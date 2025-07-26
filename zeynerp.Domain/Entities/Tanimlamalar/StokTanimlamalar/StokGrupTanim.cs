using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities.Tanimlamalar.StokTanimlamalar
{
    public class StokGrupTanim : BaseEntity
    {
        public string StokTanimAdi { get; set; } = string.Empty;
        public int Sira { get; set; }
        public Status Durum { get; set; }
    }
}