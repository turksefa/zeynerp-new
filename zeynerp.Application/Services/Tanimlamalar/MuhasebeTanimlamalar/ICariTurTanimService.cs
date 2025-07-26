using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Tanimlamalar.MuhasebeTanimlamalar;

namespace zeynerp.Application.Services.Tanimlamalar.MuhasebeTanimlamalar
{
    public interface ICariTurTanimService
    {
        Task<IEnumerable<CariTurTanimDto>> GetAllAsync();
        Task<Result<string>> AddRangeAsync(List<CariTurTanimDto> cariTurTanimDtos);
        Task<Result<string>> UpdateAsync(CariTurTanimDto cariTurTanimDto);
    }
}