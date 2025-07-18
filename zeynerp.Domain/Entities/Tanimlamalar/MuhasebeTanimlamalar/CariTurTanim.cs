using zeynerp.Domain.Entities.Common;
using zeynerp.Domain.Enums;

namespace zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTurTanim : BaseEntity
    {
        public string CariTurTanimAdi { get; set; } = string.Empty;
        public int Sira { get; set; }
        public Status Durum { get; set; }

        // Navigation properties
        public ICollection<CariTanim> CariTanimlar { get; set; } = null!;
    }
}