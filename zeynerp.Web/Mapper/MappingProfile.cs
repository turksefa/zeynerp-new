using AutoMapper;
using zeynerp.Application.DTOs.Authentication;
using zeynerp.Web.Models.Identity;

namespace zeynerp.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<LoginViewModel, LoginDto>();
        }
    }
}