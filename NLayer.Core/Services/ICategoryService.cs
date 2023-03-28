using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface ICategoryService : IServices<Category>
    {
        Task<CustomResponseDTO<CategoryWithProductsDTO>> GetSingleCategoryByIdWithProducts(int id);
    }
}
