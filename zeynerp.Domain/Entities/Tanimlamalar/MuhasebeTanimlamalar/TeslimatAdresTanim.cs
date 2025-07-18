using zeynerp.Domain.Entities.Common;

namespace zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar
{
    public class TeslimatAdresTanim : BaseEntity
    {
        public string Baslik { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public Guid CariTanimId { get; set; }

        // Navigation properties
        public CariTanim CariTanim { get; set; } = null!;
    }
}