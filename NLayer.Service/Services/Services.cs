using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Services(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        
        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
