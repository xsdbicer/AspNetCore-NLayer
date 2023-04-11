using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AddDto;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IServices<Category,CategoryDTO>
    {
        Task<CustomResponseDTO<CategoryWithProductsDTO>> GetSingleCategoryByIdWithProducts(int id);
        Task<CustomResponseDTO<CategoryDTO>> AddAsync(CategoryAddDto dto);
    }
}
