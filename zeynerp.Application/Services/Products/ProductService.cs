using zeynerp.Domain.Entities.Products;
using zeynerp.Domain.Repositories;

namespace zeynerp.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _unitOfWork.Products.GetAllAsync();

    }
}