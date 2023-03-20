using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProducts(int Categoryid)
        {
            return await _context.Categories.Include(c => c.Products).Where(c=>c.Id==Categoryid).SingleOrDefaultAsync();
        }
    }
}
