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
        private readonly IInvitationRepository _invitationRepository;
        private readonly TenantDbContext _tenantDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITenantService _tenantService;
        private readonly IEmailService _emailService;

        public InvitationService(IInvitationRepository invitationRepository, TenantDbContext tenantDbContext, UserManager<ApplicationUser> userManager, ITenantService tenantService, IEmailService emailService)
        {
            _invitationRepository = invitationRepository;
            _tenantDbContext = tenantDbContext;
            _userManager = userManager;
            _tenantService = tenantService;
            _emailService = emailService;
        }

        public async Task<Result<bool>> SendInvitationAsync(InvitationDto invitationDto)
        {
            var email = await _userManager.FindByEmailAsync(invitationDto.Email);
            if (email != null)
                return Result<bool>.Failure("Bu e-posta adresi zaten kayıtlı.");

            var tenantId = await _tenantService.GetTenantIdAsync();

            var applicationUser = new ApplicationUser
            {
                UserName = invitationDto.Email,
                Email = invitationDto.Email,
                TenantId = tenantId
            };

            var result = await _userManager.CreateAsync(applicationUser);
            if (!result.Succeeded)
                return Result<bool>.Failure(result.Errors.Select(e => e.Description).ToList()); 

            var emailSent = await _emailService.SendInvitationEmailAsync(applicationUser.Email, applicationUser.UserName, $"https://localhost:7240/Authentication/AcceptInvitation?token={tenantId}");           
            if (!emailSent)
            {
                await _userManager.DeleteAsync(applicationUser);
                return Result<bool>.Failure("Davet e-postası gönderilemedi. Lütfen tekrar deneyin.");
            }

            var invitation = new Invitation
            {
                Email = invitationDto.Email,
                Token = tenantId.ToString(),
                ExpiresAt = DateTime.Now.AddDays(7),
                CreatedDate = DateTime.Now,
                Status = InvitationStatus.Pending
            };
            await _invitationRepository.AddAsync(invitation);
            await _tenantDbContext.SaveChangesAsync();

            return Result<bool>.Success();
        }
    }
}