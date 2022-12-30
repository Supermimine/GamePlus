using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
	public class ProductConfirmation : ViewComponent
	{
        private readonly GamePlusDbContext _context;

        public ProductConfirmation(GamePlusDbContext context)
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
