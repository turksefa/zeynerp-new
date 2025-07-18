using zeynerp.Domain.Enums;

namespace zeynerp.Application.DTOs.Tanimlamalar.StokTanimlamalar
{
    public class StokGrupTanimDto
    {
        public Guid Id { get; set; }
        public string StokTanimAdi { get; set; } = string.Empty;
        public int Sira { get; set; }
        public Status Durum { get; set; }
    }
}