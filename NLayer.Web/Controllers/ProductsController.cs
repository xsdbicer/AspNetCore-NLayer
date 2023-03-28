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
            var categories=await _categoryService.GetAllAsync();
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
            var categories = await _categoryService.GetAllAsync();
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoryDTO, "Id", "Name");
            return View();
        }
    }
}
