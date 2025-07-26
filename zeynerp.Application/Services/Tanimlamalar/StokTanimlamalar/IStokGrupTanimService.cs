using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Tanimlamalar.StokTanimlamalar;

namespace zeynerp.Application.Services.Tanimlamalar.StokTanimlamalar
{
    public interface IStokGrupTanimService
    {
        Task<IEnumerable<StokGrupTanimDto>> GetAllAsync();
        Task<Result<string>> AddRangeAsync(List<StokGrupTanimDto> stokGrupTanimDtos);
        Task<Result<string>> UpdateAsync(StokGrupTanimDto stokGrupTanimDto);
    }
}