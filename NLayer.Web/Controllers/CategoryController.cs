using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryApiService _categoryApiService;

        public CategoryController(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryApiService.GetAllAsync());
        }

        public async Task<IActionResult> Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.SaveCategory(dto);
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }
        [ServiceFilter(typeof(NotFoundFilter<Category, CategoryDTO>))]
        public async Task<IActionResult> Update(int id)
        {
            var category= await _categoryApiService.GetByIdAsync(id);
            
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _categoryApiService.Update(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);

        }


        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
