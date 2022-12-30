using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
	public class ProductShop : ViewComponent
	{
        private readonly GamePlusDbContext _context;

        public ProductShop(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var product = await _context.ShoppingCartItems.ToListAsync();

            return View(product);
        }
    }
}
