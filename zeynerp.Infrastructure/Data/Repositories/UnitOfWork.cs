using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IProductRepository _productRepository;
        private readonly TenantDbContext _tenantDbContext;

        public UnitOfWork(IProductRepository productRepository, TenantDbContext tenantDbContext)
        {
            _productRepository = productRepository;
            _tenantDbContext = tenantDbContext;
        }

        public IProductRepository Products => _productRepository;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync() => await _tenantDbContext.SaveChangesAsync();
    }
}