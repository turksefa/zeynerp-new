using AutoMapper;
using Microsoft.AspNetCore.Identity;
using zeynerp.Application.Common.Interfaces;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs;
using zeynerp.Domain.Entities;
using zeynerp.Domain.Entities.Identity;
using zeynerp.Domain.Enums;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInvitationRepository _invitationRepository;
        private readonly ITenantService _tenantService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public InvitationService(ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager,
            IInvitationRepository invitationRepository,
            ITenantService tenantService,
            IEmailService emailService,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _invitationRepository = invitationRepository;
            _tenantService = tenantService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<InvitationDto> GetInvitationByIdAsync(Guid invitationId) =>
            _mapper.Map<InvitationDto>(await _invitationRepository.GetInvitationByIdAsync(invitationId));

        public async Task<Result<bool>> SendInvitationAsync(InvitationDto invitationDto)
        {
            var email = await _userManager.FindByEmailAsync(invitationDto.Email);
            if (email != null)
                return Result<bool>.Failure("Bu e-posta adresi zaten kayıtlı.");

            var tenantId = await _tenantService.GetTenantIdAsync();

            var user = await _userManager.FindByIdAsync(await _tenantService.GetCurrentUserIdAsync());
            if (user == null)
                return Result<bool>.Failure("Kullanıcı bulunamadı.");

            var applicationUser = new ApplicationUser
            {
                TenantId = tenantId,
                UserName = invitationDto.Email,
                Email = invitationDto.Email,
                CompanyName = user.CompanyName
            };

            var result = await _userManager.CreateAsync(applicationUser);
            if (!result.Succeeded)
                return Result<bool>.Failure(result.Errors.Select(e => e.Description).ToList());

            var invitation = new Invitation
            {
                UserId = applicationUser.Id,
                Email = invitationDto.Email,
                Token = tenantId.ToString(),
                ExpiresAt = DateTime.Now.AddDays(7),
                CreatedDate = DateTime.Now,
                Status = InvitationStatus.Pending
            };
            await _invitationRepository.AddAsync(invitation);
            await _applicationDbContext.SaveChangesAsync();

            var emailSent = await _emailService.SendInvitationEmailAsync(applicationUser.Email, applicationUser.UserName, $"https://zeynerp.com/davet-kabul?invitationId={invitation.Id}");
            if (!emailSent)
            {
                await _userManager.DeleteAsync(applicationUser);
                return Result<bool>.Failure("Davet e-postası gönderilemedi. Lütfen tekrar deneyin.");
            }

            return Result<bool>.Success();
        }

        public async Task<Result<bool>> AcceptInvitationAsync(InvitationDto invitationDto)
        {
            if (invitationDto.Id.HasValue)
            {
                var invitationId = invitationDto.Id.Value;
                var invitation = await _invitationRepository.GetInvitationByIdAsync(invitationId);
                if (invitation == null)
                    return Result<bool>.Failure("Davet bulunamadı veya geçersiz.");

                if (invitation.Status != InvitationStatus.Pending)
                    return Result<bool>.Failure("Davet zaten kabul edilmiş veya reddedilmiş.");

                if(!string.IsNullOrEmpty(invitationDto.FullName))
                    invitation.User.FullName = invitationDto.FullName;
                invitation.User.EmailConfirmed = true;
                await _userManager.UpdateAsync(invitation.User);

                if(!string.IsNullOrEmpty(invitationDto.Password))
                    await _userManager.AddPasswordAsync(invitation.User, invitationDto.Password);

                invitation.Status = InvitationStatus.Accepted;
                _applicationDbContext.Invitations.Update(invitation);
                await _applicationDbContext.SaveChangesAsync();
            }


            return Result<bool>.Success();
        }
    }
}