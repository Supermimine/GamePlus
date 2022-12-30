using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class Compagny : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public Compagny(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var compagny = await _context.Compagnies.Where(x => x.CompagnyID == id).FirstOrDefaultAsync();

            return View(compagny);
        }
    }
}
