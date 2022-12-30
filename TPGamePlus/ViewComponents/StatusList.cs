using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
    public class StatusList : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public StatusList(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var status = await _context.Status.ToListAsync();

            return View(status);
        }
    }
}
