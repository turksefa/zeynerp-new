using AutoMapper;
using zeynerp.Application.Common.Models;
using zeynerp.Application.DTOs.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Enums;
using zeynerp.Domain.Repositories;

namespace zeynerp.Application.Services.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTurTanimService : ICariTurTanimService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CariTurTanimService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CariTurTanimDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<CariTurTanimDto>>(await _unitOfWork.CariTurTanimRepository.GetAllAsync());

        public async Task<Result<string>> AddRangeAsync(List<CariTurTanimDto> cariTurTanimDtos)
        {
            if (cariTurTanimDtos == null)
                return Result<string>.Failure("dto not found");

            cariTurTanimDtos.ForEach(s => s.Durum = Status.Aktif);

            await _unitOfWork.CariTurTanimRepository.AddRangeAsync(_mapper.Map<IEnumerable<CariTurTanim>>(cariTurTanimDtos));

            return Result<string>.Success("Cari türler oluşturuldu.");
        }        

        public async Task<Result<string>> UpdateAsync(CariTurTanimDto cariTurTanimDto)
        {
            if (cariTurTanimDto == null)
                return Result<string>.Failure("dto not found");

            await _unitOfWork.CariTurTanimRepository.UpdateAsync(_mapper.Map<CariTurTanim>(cariTurTanimDto));
            
            return Result<string>.Success($"{cariTurTanimDto.CariTurTanimAdi} cari tür güncelleştirildi.");
        }
    }
}