using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.StokTanimlamalar;

namespace zeynerp.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IStokGrupTanimRepository StokGrupTanimRepository { get; }
        ICariTanimRepository CariTanimRepository { get; }
        ICariTurTanimRepository CariTurTanimRepository { get; }
        ICariYetkiliTanimRepository CariYetkiliTanimRepository { get; }
        ITeslimatAdresTanimRepository TeslimatAdresTanimRepository { get; }
        Task<int> SaveChangesAsync();
    }
}