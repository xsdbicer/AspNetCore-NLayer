using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetProductsWithCategory());
        }
        public async Task<IActionResult> Save()
        {
            var categoryDTO = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDTO, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDto)
        {


            if (ModelState.IsValid)
            {
                await _productApiService.Save(productDto);
                return RedirectToAction(nameof(Index));
            }
            // TODO: Neden product kaydedip Category listesini view'e gönderiyorum? 
            var categoryDTO = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDTO, "Id", "Name");
            return View();
        }
        // TODO: her metottan iki tane yazıyoruz birisi http tagli bunların farkı ve amacı ne?
        /* TODO: ilk metot update butonuna tıkladığım zaman verileri getiriyor,
       ikincisi de submitlemek istediğim zaman işlemi dbye yansıtıyor ve route işlemini yapıyor
         peki bu ikisini tek metotta yapamaz mıydık? [http_] yi tek metota versek ne olur? */
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product=await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO productdto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productdto);
                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productdto.CategoryId);

            return View(productdto);

        }

        public async Task<IActionResult> Delete(int id)
        {
    
            await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));

        }

        
    }
}
