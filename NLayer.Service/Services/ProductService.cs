using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AddDto;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Services<Product, ProductDTO>, IProductService
    {
        // product repositorye erişmek için
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IProductRepository productRepository, IMapper mapper) : base(unitOfWork,mapper, repository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductAddDto dto)
        {
            var product=_mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            var ProductDto = _mapper.Map<ProductDTO>(product);
            return CustomResponseDTO<ProductDTO>.Success(ProductDto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory()
        {
            var products = await _productRepository.GetProductWithCategory();
            var productDTO = _mapper.Map<List<ProductWithCategoryDTO>>(products);
            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(productDTO,200);
        }

        public async Task<CustomResponseDTO<ProductDTO>> UpdateAsync(ProductUpdateDTO dto)
        {
            var entity=_mapper.Map<Product>(dto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            var dtoS = _mapper.Map<ProductDTO>(entity);
            return CustomResponseDTO<ProductDTO>.Success(dtoS, StatusCodes.Status200OK);
        }
    }
}
