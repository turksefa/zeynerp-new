using AutoMapper;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Tanimlamalar.StokTanimlamalar;
using zeynerp.Domain.Entities.Tanimlamalar.StokTanimlamalar;
using zeynerp.Domain.Enums;
using zeynerp.Domain.Repositories;

namespace zeynerp.Application.Services.Tanimlamalar.StokTanimlamalar
{
    public class StokGrupTanimService : IStokGrupTanimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StokGrupTanimService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StokGrupTanimDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<StokGrupTanimDto>>(await _unitOfWork.StokGrupTanimRepository.GetAllAsync());

        public async Task<Result<string>> AddRangeAsync(List<StokGrupTanimDto> stokGrupTanimDtos)
        {
            if (stokGrupTanimDtos == null)
                return Result<string>.Failure("dto not found");

            stokGrupTanimDtos.ForEach(s => s.Durum = Status.Aktif);

            await _unitOfWork.StokGrupTanimRepository.AddRangeAsync(_mapper.Map<IEnumerable<StokGrupTanim>>(stokGrupTanimDtos));

            return Result<string>.Success("Stok gruplar oluşturuldu.");
        }

        public async Task<Result<string>> UpdateAsync(StokGrupTanimDto stokGrupTanimDto)
        {
            if (stokGrupTanimDto == null)
                return Result<string>.Failure("dto not found");

            await _unitOfWork.StokGrupTanimRepository.UpdateAsync(_mapper.Map<StokGrupTanim>(stokGrupTanimDto));
            
            return Result<string>.Success($"{stokGrupTanimDto.StokTanimAdi} stok grup güncelleştirildi.");
        }
    }
}