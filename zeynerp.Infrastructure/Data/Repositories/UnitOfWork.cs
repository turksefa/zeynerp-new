using zeynerp.Domain.Repositories;
using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.StokTanimlamalar;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TenantDbContext _tenantDbContext;
        private readonly IStokGrupTanimRepository _stokGrupTanimRepository;
        private readonly ICariTanimRepository _cariTanimRepository;
        private readonly ICariTurTanimRepository _cariTurTanimRepository;
        private readonly ICariYetkiliTanimRepository _cariYetkiliTanimRepository;
        private readonly ITeslimatAdresTanimRepository _teslimatAdresTanimRepository;

        public UnitOfWork(TenantDbContext tenantDbContext, IStokGrupTanimRepository stokGrupTanimRepository, ICariTanimRepository cariTanimRepository, ICariTurTanimRepository cariTurTanimRepository, ICariYetkiliTanimRepository cariYetkiliTanimRepository, ITeslimatAdresTanimRepository teslimatAdresTanimRepository)
        {
            _tenantDbContext = tenantDbContext;
            _stokGrupTanimRepository = stokGrupTanimRepository;
            _cariTanimRepository = cariTanimRepository;
            _cariTurTanimRepository = cariTurTanimRepository;
            _cariYetkiliTanimRepository = cariYetkiliTanimRepository;
            _teslimatAdresTanimRepository = teslimatAdresTanimRepository;
        }

        public IStokGrupTanimRepository StokGrupTanimRepository => _stokGrupTanimRepository;

        public ICariTanimRepository CariTanimRepository => _cariTanimRepository;

        public ICariTurTanimRepository CariTurTanimRepository => _cariTurTanimRepository;

        public ICariYetkiliTanimRepository CariYetkiliTanimRepository => _cariYetkiliTanimRepository;

        public ITeslimatAdresTanimRepository TeslimatAdresTanimRepository => _teslimatAdresTanimRepository;

        public async Task<int> SaveChangesAsync() => await _tenantDbContext.SaveChangesAsync();
    }
}