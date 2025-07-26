using zeynerp.Domain.Enums;

namespace zeynerp.Web.Models.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTurTanimViewModel
    {
        public Guid Id { get; set; }
        public string CariTurTanimAdi { get; set; } = string.Empty;
        public int Sira { get; set; }
        public Status Durum { get; set; }
    }
}