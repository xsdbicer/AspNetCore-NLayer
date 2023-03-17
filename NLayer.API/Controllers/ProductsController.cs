﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductsController : CustomBaseController
    {

        // mapping -> normalde service kapmanında gerçekleştirmemiz gerekiyor fakat repoda olmadığı için şuan burada yapıyoruz. v1
        //Burada mümkün olduğunda bussiness kod olmayacak !! BP !!
        private readonly IMapper _mapper;
        private readonly IServices<Product> _service;
        private readonly IProductService _productservice;

        public ProductsController(IMapper mapper, IServices<Product> service, IProductService productservice)
        {
            _mapper = mapper;
            _service = service;
            _productservice = productservice;
        }

        // get
        // www.mysite.com/api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            // bu entity
            var products = await _service.GetAllAsync();
            // mapping işlemi
            var productDTOs=_mapper.Map<List<ProductDTO>>(products.ToList());
            //CustomBaseController yazmadan önce return tipimiz böyleydi. Eğer CustomBase olmasaydı Created, Badrequest gibi gibi sonuçların hepsi için metotlar belirtmem gerekecekti. !! BP !!
            //return Ok(CustomResponseDTO<List<ProductDTO>>.Success(productDTOs, 200));
            return CreateActionResult(CustomResponseDTO<List<ProductDTO>>.Success(productDTOs, 200));
        }


        // get
        // www.mysite.com/api/products/GetProductWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _productservice.GetProductWithCategory());
        }


        // get
        // www.mysite.com/api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDTO= _mapper.Map<ProductDTO>(product);
            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(productDTO, 200));
        }


        // post
        // www.mysite.com/api/products/5
        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDTO));
            var productsDTO = _mapper.Map<ProductDTO>(product);
            // 201 -> Created
            return CreateActionResult(CustomResponseDTO<ProductDTO>.Success(productsDTO, 201));
        }



        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productDTO)
        {

            await _service.UpdateAsync(_mapper.Map<Product>(productDTO));
            // 204 -> NOContent
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success( 204));
        }


        // Delete
        // www.mysite.com/api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            // tabi burada belirtilen product var mı yok mu diye bir kontrol yapmam gerekiyor. İleride bunu revize edeceğiz. v1
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success( 200));
        }
    }
}