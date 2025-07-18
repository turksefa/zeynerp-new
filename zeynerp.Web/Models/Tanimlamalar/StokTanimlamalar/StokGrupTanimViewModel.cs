using zeynerp.Domain.Enums;

namespace zeynerp.Web.Models.Tanimlamalar.StokTanimlamalar
{
    public class StokGrupTanimViewModel
    {
        public Guid Id { get; set; }
        public string StokTanimAdi { get; set; } = string.Empty;
        public int Sira { get; set; }
        public Status Durum { get; set; }
    }
}