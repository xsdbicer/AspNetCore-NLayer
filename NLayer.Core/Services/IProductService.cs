using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AddDto;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IServices<Product,ProductDTO>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory();
        Task<CustomResponseDTO<ProductDTO>> UpdateAsync(ProductUpdateDTO dto);
        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductAddDto dto);

    }
}
