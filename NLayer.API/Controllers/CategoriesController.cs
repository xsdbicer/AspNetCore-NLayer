using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AddDto;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            
            return CreateActionResult(await _categoryService.GetAllAsync());
        }
        [ServiceFilter(typeof(NotFoundFilter<Category, CategoryDTO>))]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return CreateActionResult(await _categoryService.GetByIdAsync(id));
        }
        [ServiceFilter(typeof(NotFoundFilter<Category, CategoryDTO>))]

        //api/categories/GetSingleCategoryByIdWithProducts/2
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProducts(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryAddDto entity)
        {
            return CreateActionResult(await _categoryService.AddAsync(entity));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO dto)
        {
            return CreateActionResult(await _categoryService.UpdateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _categoryService.RemoveAsync(id));  
        }
    }
}
