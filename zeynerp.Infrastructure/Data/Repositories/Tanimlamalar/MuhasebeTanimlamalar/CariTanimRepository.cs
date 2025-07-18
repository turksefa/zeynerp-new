using zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariTanimRepository : Repository<CariTanim>, ICariTanimRepository
    {
        public CariTanimRepository(TenantDbContext tenantDbContext) : base(tenantDbContext)
        {
        }
    }
}