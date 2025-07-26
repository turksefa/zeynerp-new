using Microsoft.EntityFrameworkCore;
using zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTurTanimRepository : Repository<CariTurTanim>, ICariTurTanimRepository
    {
        public CariTurTanimRepository(TenantDbContext tenantDbContext) : base(tenantDbContext)
        {

        }

        public override async Task<IEnumerable<CariTurTanim>> GetAllAsync() =>
            await _dbSet.OrderBy(c => c.Sira).ToListAsync();
    }
}