using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Tanimlamalar.StokTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.StokTanimlamalar;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.StokTanimlamalar
{
    public class StokGrupTanimRepository : Repository<StokGrupTanim>, IStokGrupTanimRepository
    {
        public StokGrupTanimRepository(TenantDbContext tenantDbContext) : base(tenantDbContext)
        {
        }

        public override async Task<IEnumerable<StokGrupTanim>> GetAllAsync() =>
            await _dbSet.OrderBy(s => s.Sira).ToListAsync();
    }
}