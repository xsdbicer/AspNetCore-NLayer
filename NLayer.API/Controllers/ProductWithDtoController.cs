using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWithDto;

        public ProductWithDtoController(IProductServiceWithDto productServiceWithDto)
        {
            _productServiceWithDto = productServiceWithDto;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            
            return CreateActionResult(await _productServiceWithDto.GetAllAsync());
            
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _productServiceWithDto.GetProductWithCategory());
        }


        

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            return CreateActionResult(await _productServiceWithDto.GetByIdAsync(id));
        }



        [HttpPost]
        public async Task<IActionResult> Save(ProductAddDto productDTO)
        {
            
            return CreateActionResult(await _productServiceWithDto.AddAsync(productDTO));
        }



        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productDTO)
        {
            return CreateActionResult(await _productServiceWithDto.UpdateAsync(productDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            return CreateActionResult(await _productServiceWithDto.RemoveAsync(id));
        }
    }
}
