using zeynerp.Domain.Entities.Products;
using zeynerp.Domain.Repositories;
using zeynerp.Infrastructure.Data.Contexts;

namespace zeynerp.Infrastructure.Data.Repositories.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TenantDbContext context) : base(context)
        {
        }
    }
}