using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IServices<Product,ProductDTO>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory();
        Task<CustomResponseDTO<ProductDTO>> Update(ProductUpdateDTO dto);

    }
}
