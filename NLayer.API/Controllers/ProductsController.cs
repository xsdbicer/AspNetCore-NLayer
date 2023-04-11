using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AddDto;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductsController : CustomBaseController
    {

        // mapping -> normalde service kapmanında gerçekleştirmemiz gerekiyor fakat repoda olmadığı için şuan burada yapıyoruz. v1
        //Burada mümkün olduğunda bussiness kod olmayacak !! BP !!
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService productservice)
        {
            _mapper = mapper;
            _service = productservice;
            
        }


        //GET www.mysite.com/api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            
            return CreateActionResult(await _service.GetAllAsync());
            #region Return type
            //CustomBaseController yazmadan önce return tipimiz böyleydi. Eğer CustomBase olmasaydı Created, Badrequest gibi gibi sonuçların hepsi için metotlar belirtmem gerekecekti. !! BP !!
            //return Ok(CustomResponseDTO<List<ProductDTO>>.Success(productDTOs, 200)); 
            #endregion
        }


        //GET www.mysite.com/api/products/GetProductWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _service.GetProductWithCategory());
        }



        //Bir filter constructorda parametre alıyorsa direkt olarak veremeyiz serviceFilter üzerinden belirtmem gerekiyor.
        [ServiceFilter(typeof(NotFoundFilter<Product,ProductDTO>))]
        //GET www.mysite.com/api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           
            return CreateActionResult(await _service.GetByIdAsync(id));
        }



        //POST www.mysite.com/api/products/5
        [HttpPost]
        public async Task<IActionResult> Save(ProductAddDto productDTO)
        {
            
            return CreateActionResult(await _service.AddAsync(productDTO));
        }



        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productDTO)
        {

            //TODO productUpdateDto
            return CreateActionResult(await _service.UpdateAsync(productDTO));
        }

        //DELETE www.mysite.com/api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {           
            return CreateActionResult(await _service.RemoveAsync(id));
        }
    }
}
