using AutoMapper;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Subscription;
using zeynerp.Domain.Repositories;

namespace zeynerp.Application.Services.Subscription
{
    public class PlanService : IPlanService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlanDto>> GetPlansAsync() => _mapper.Map<IEnumerable<PlanDto>>(await _applicationUnitOfWork.PlanRepository.GetAllAsync());

        public async Task<Result<bool>> PlanExistsAsync(int planId)
        {
            var exists = await _applicationUnitOfWork.PlanRepository.FirstOrDefaultAsync(p => p.Id == planId) != null;
            return Result<bool>.Success(exists);
        }
    }
}