using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IServiceWithDto<Entity,Dto> where Entity : BaseEntity where Dto : class
    {
        //Generic Service'den dto dönmek !! BP !!

        Task<CustomResponseDTO<Dto>> GetByIdAsync(int id);
        Task<CustomResponseDTO<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponseDTO<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDTO<Dto>> AddAsync(Dto dto);
        Task<CustomResponseDTO<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);
        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(Dto dto);
        Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id);
        Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> ids);

    }
}
