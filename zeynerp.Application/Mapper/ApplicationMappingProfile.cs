using AutoMapper;
using zeynerp.Application.DTOs;
using zeynerp.Application.DTOs.Subscription;
using zeynerp.Application.DTOs.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Application.DTOs.Tanimlamalar.StokTanimlamalar;
using zeynerp.Domain.Entities;
using zeynerp.Domain.Entities.Subscription;
using zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Entities.Tanimlamalar.StokTanimlamalar;

namespace zeynerp.Application.Mapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<StokGrupTanimDto, StokGrupTanim>().ReverseMap();
            CreateMap<CariTurTanimDto, CariTurTanim>().ReverseMap();
            CreateMap<Plan, PlanDto>();
            CreateMap<PlanPricing, PlanPricingDto>();
            CreateMap<PlanSubscriptionDto, PlanSubscription>().ReverseMap();
            CreateMap<Invitation, InvitationDto>().ReverseMap();
        }
    }
}