using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDTO>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory();

        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto);

        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductAddDto dto);

        Task<CustomResponseDTO<IEnumerable<ProductDTO>>> AddRangeAsync(IEnumerable<ProductAddDto> dtos);



    }
}
