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
    public class CategoryService : Services<Category,CategoryDTO>, ICategoryService
    {
        //mapper
        //repository
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IGenericRepository<Category> repository, ICategoryRepository ProductRepository, IMapper mapper) : base(unitOfWork, mapper, repository)
        {
            _categoryRepository = ProductRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<CategoryDTO>> AddAsync(CategoryAddDto dto)
        {
            var category=_mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return CustomResponseDTO<CategoryDTO>.Success(categoryDto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<CategoryWithProductsDTO>> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCategoryByIdWithProducts(categoryId);
            var categoryDTO = _mapper.Map<CategoryWithProductsDTO>(category);
            return CustomResponseDTO<CategoryWithProductsDTO>.Success(categoryDTO, 200);
        }
    }
}
