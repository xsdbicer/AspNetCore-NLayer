using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService service, ICategoryService categoryService = null, IMapper mapper = null)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetProductWithCategory());
        }
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoryDTO, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDto)
        {


            if (ModelState.IsValid)
            {
                await _service.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            // TODO: Neden product kaydedip Category listesini view'e gönderiyorum? 
            var categories = await _categoryService.GetAllAsync();
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(categories.ToList());
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
            var product=await _service.GetByIdAsync(id);
            var categories= await _categoryService.GetAllAsync();
            var categoriesDto=_mapper.Map<List<CategoryDTO>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);
            return View(_mapper.Map<ProductDTO>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO productdto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Product>(productdto));
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productdto.CategoryId);

            return View(productdto);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var product= await _service.GetByIdAsync(id);
    
            await _service.RemoveAsync(_mapper.Map<Product>(product));

            return RedirectToAction(nameof(Index));

        }

        
    }
}
