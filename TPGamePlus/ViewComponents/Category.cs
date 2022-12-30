using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class Category : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public Category(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var category = await _context.Category.Where(x => x.CategoryID == id).FirstOrDefaultAsync();

            return View(category);
        }
    }
}
