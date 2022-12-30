using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class ProductInfo : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public ProductInfo(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var description = await _context.ProductsInfos.Where(x => x.ProductInfoID == id).FirstOrDefaultAsync();

            return View(description);
        }
    }
}
