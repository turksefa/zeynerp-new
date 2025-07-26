using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTanim : BaseEntity
    {
        public string Adi { get; set; } = string.Empty;
        public string KisaAdi { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string EPosta { get; set; } = string.Empty;
        public string VergiDairesi { get; set; } = string.Empty;
        public string VergiNumarasi { get; set; } = string.Empty;
        public string FaturaAdresi { get; set; } = string.Empty;
        public CariStatus Durum { get; set; } = CariStatus.Beklemede;

        // Navigation properties
        public ICollection<CariYetkiliTanim> CariYetkiliTanimlar { get; set; } = null!;
        public ICollection<TeslimatAdresTanim> TeslimatAdresTanimlar { get; set; } = null!;
        public ICollection<CariTurTanim> CariTurTanimlar { get; set; } = null!;
    }
}