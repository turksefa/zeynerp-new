using zeynerp.Domain.Entities.Products;

namespace zeynerp.Application.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}