﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDTO>, IProductServiceWithDto
    {
        private readonly IProductRepository _productRepository;
        public ProductServiceWithDto(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IProductRepository productRepository) : base(mapper, unitOfWork, repository)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDTO<ProductDTO>> AddAsync(ProductAddDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var ProductDto = _mapper.Map<ProductDTO>(entity);
            return CustomResponseDTO<ProductDTO>.Success(ProductDto, StatusCodes.Status200OK);


        }

        public async Task<CustomResponseDTO<List<ProductWithCategoryDTO>>> GetProductWithCategory()
        {
            var products = await _productRepository.GetProductWithCategory();
            var productDTO = _mapper.Map<List<ProductWithCategoryDTO>>(products);
            return CustomResponseDTO<List<ProductWithCategoryDTO>>.Success(productDTO, 200);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(ProductUpdateDTO dto)
        {
            var newEntity = _mapper.Map<Product>(dto);
            _productRepository.Update(newEntity);
            await _unitOfWork.CommitAsync();
            var productDto = _mapper.Map<ProductDTO>(newEntity);
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<CustomResponseDTO<IEnumerable<ProductDTO>>> AddRangeAsync(IEnumerable<ProductAddDto> dtos)
        {
            var newEntities = _mapper.Map<IEnumerable<Product>>(dtos);
            await _productRepository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitAsync();
            var ProductDtos = _mapper.Map<List<ProductDTO>>(newEntities);
            return CustomResponseDTO<IEnumerable<ProductDTO>>.Success(ProductDtos, StatusCodes.Status200OK);
        }

    }
}
