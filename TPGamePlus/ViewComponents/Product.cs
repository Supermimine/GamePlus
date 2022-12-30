using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class Product : ViewComponent
    {

        private readonly GamePlusDbContext _context;

        public Product(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, bool isQuickBuy)
        {
            var product = await _context.Products.Where(x => x.ProductID == id).FirstOrDefaultAsync();
            ViewBag.quickBuy = isQuickBuy;

            return View(product);
        }
    }
}
