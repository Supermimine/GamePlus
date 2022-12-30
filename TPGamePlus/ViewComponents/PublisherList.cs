using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Data;

namespace TPGamePlus.ViewComponents
{
	public class PublisherList : ViewComponent
    {
            private readonly GamePlusDbContext _context;

            public PublisherList(GamePlusDbContext context)
            {
                _context = context;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var publishers = await _context.Publishers.ToListAsync();

                return View(publishers);
            }
    }
}
