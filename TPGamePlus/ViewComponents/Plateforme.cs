using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
	public class Plateforme : ViewComponent
	{
        private readonly GamePlusDbContext _context;

        public Plateforme(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var plateforme = await _context.Plateformes.Where(x => x.PlateformeID == id).FirstOrDefaultAsync();

            return View(plateforme);
        }
    }
}
