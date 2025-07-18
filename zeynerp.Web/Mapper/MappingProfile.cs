using AutoMapper;
using zeynerp.Application.DTOs.Authentication;
using zeynerp.Application.DTOs.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Application.DTOs.Tanimlamalar.StokTanimlamalar;
using zeynerp.Web.Models.Authentication;
using zeynerp.Web.Models.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Web.Models.Tanimlamalar.StokTanimlamalar;

namespace zeynerp.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<StokGrupTanimViewModel, StokGrupTanimDto>().ReverseMap();
            CreateMap<CariTurTanimViewModel, CariTurTanimDto>().ReverseMap();
        }
    }
}