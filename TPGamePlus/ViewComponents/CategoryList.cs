using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public CategoryList(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _context.Category.ToListAsync();

            return View(category);
        }
    }
}
