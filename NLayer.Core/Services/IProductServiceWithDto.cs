using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto:IServiceWithDto<Product,ProductDTO>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory();

        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto);

        Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductAddDto dto);


    }
}
