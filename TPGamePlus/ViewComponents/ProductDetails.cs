using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TPGamePlus.Data;


namespace TPGamePlus.ViewComponents
{
	public class ProductDetails : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public ProductDetails(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var product = await _context.Products.Where(x => x.ProductID == id).FirstOrDefaultAsync();

            ViewBag.QteCart = _context.ShoppingCartItems.Where(x => x.ProductId == product.ProductID).Select(x => x.Quantity).FirstOrDefault();

            return View(product);
        }
    }
}
