using zeynerp.Domain.Enums;

namespace zeynerp.Application.DTOs.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTurTanimDto
    {
        public Guid Id { get; set; }
        public string CariTurTanimAdi { get; set; } = string.Empty;
        public int Sira { get; set; }
        public Status Durum { get; set; }
    }
}