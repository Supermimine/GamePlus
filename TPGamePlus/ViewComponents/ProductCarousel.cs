using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class ProductCarousel : ViewComponent
    {

        private readonly GamePlusDbContext _context;

        public ProductCarousel(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var product = await _context.Products.Where(x => x.ProductID == id).FirstOrDefaultAsync();

            return View(product);
        }
    }
}
