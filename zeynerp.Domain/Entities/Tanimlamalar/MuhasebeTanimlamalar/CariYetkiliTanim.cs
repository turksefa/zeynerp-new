using zeynerp.Domain.Entities.Common;

namespace zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariYetkiliTanim : BaseEntity
    {
        public string AdiSoyadi { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string? Fax { get; set; } = string.Empty;
        public string EPosta { get; set; } = string.Empty;
        public Guid CariTanimId { get; set; }

        // Navigation properties
        public CariTanim CariTanim { get; set; } = null!;        
    }
}