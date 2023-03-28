using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IServices<Product>
    {
        Task<List<ProductWithCategoryDTO>> GetProductWithCategory();
    }
}
