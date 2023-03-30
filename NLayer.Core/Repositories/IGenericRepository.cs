using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        T Add(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        List<T> AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void SaveChange();
        Task SaveChangeAsync();

    }
}
