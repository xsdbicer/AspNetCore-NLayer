using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IServices<InT,OutT> where InT : BaseEntity where OutT : class
    {
        Task<CustomResponseDTO<OutT>> GetByIdAsync(int id);
        Task<CustomResponseDTO<IEnumerable<OutT>>> GetAllAsync();
        CustomResponseDTO<IQueryable<InT>> Where(Expression<Func<InT, bool>> expression);
        Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<InT, bool>> expression);
        Task<CustomResponseDTO<OutT>> AddAsync(OutT entity);
        Task<CustomResponseDTO<IEnumerable<OutT>>> AddRangeAsync(IEnumerable<OutT> entities);
        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(OutT entity);
        Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id);
        Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}
