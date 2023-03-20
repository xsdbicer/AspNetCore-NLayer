using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : Core.Repositories.IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            
        }

        public T Add(T entity)
        {
            var result = _dbSet.Add(entity);
            return entity;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public List<T> AddRange(IEnumerable<T> entities)
        {
            var result = _dbSet.Add((T)entities);
            return entities.ToList();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            // AsNoTracking dememizin amacı efcore çektiği dataları memory'e almasın. 
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            //Bu metotun ayns ı yok. Çünkü çalıştığında entity'i dbden silmez. Deleted şeklinde bir flag ekler
            //savechange metotunu çağırdığımızda ef core deleted flaglari bulup gidip dbden siliyor. 
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }



        public void SaveChange()
        {
            _context.SaveChanges();
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
