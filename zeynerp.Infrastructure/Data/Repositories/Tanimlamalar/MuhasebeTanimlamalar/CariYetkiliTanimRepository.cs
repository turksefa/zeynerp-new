using zeynerp.Domain.Entities.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Domain.Repositories.Tanimlamalar.MuhasebeTanimlamalar;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Tanimlamalar.MuhasebeTanimlamalar
{
    public class CariYetkiliTanimRepository : Repository<CariYetkiliTanim>, ICariYetkiliTanimRepository
    {
        public CariYetkiliTanimRepository(TenantDbContext tenantDbContext) : base(tenantDbContext)
        {
        }
    }
}