using zeynerp.Domain.Entities.Common;

namespace zeynerp.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}