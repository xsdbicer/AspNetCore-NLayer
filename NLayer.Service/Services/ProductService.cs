using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Services<Product>, IProductService
    {
        // product repositorye erişmek için
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IProductRepository productRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductWithCategory()
        {
            var products = await _productRepository.GetProductWithCategory();
            var productDTO = _mapper.Map<List<ProductWithCategoryDTO>>(products);
            return productDTO;
        }
    }
}
