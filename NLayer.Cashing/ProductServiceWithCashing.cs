using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Cashing
{
    // In-memory cashe
    public class ProductServiceWithCashing : IProductService
    {
        private readonly string CasheProductKey="productsCache";

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork   _unitOfWork;
        private readonly IMemoryCache _cache;

        public ProductServiceWithCashing(IProductRepository repository, IMapper mapper, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cache = cache;
            if(!_cache.TryGetValue(CasheProductKey,out _))
            {
                _cache.Set(CasheProductKey, _repository.GetProductWithCategory().Result);
            }
        }

        public Product Add(Product entity)
        {
            _repository.Add(entity);
            _unitOfWork.Commit();
            CasheAllProducts();
            return entity;
        }

        
        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CasheAllProductsAsync();
            return entity;
        }

        public  IEnumerable<Product> AddRange(IEnumerable<Product> entities)
        {
            _repository.AddRange(entities);
             _unitOfWork.Commit();
             CasheAllProducts();
            return entities;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CasheAllProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_cache.Get<IEnumerable<Product>>(CasheProductKey));
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _cache.Get<List<Product>>(CasheProductKey).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not Found.");
            }
            return Task.FromResult(product);
        }

        public  Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory()
        {
            var products = _cache.Get<IEnumerable<Product>>(CasheProductKey);
            var ProductsWithCategoryDTO=_mapper.Map<List<ProductWithCategoryDTO>>(products);
            return Task.FromResult(CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(ProductsWithCategoryDTO, 200));
        }

        public async Task RemoveAsync(Product entity)
        {
             _repository.Remove(entity); 
            await _unitOfWork.CommitAsync();
            await CasheAllProductsAsync();

        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();    
            await CasheAllProductsAsync();

        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CasheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _cache.Get<List<Product>>(CasheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CasheAllProductsAsync()
        {
           await _cache.Set(CasheProductKey,_repository.GetAll().ToListAsync());
        }
        public void CasheAllProducts()
        {
             _cache.Set(CasheProductKey, _repository.GetAll().ToList());
        }
    }
}
