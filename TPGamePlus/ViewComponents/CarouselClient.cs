using Microsoft.AspNetCore.Mvc;

namespace TPGamePlus.ViewComponents
{
    public class CarouselClient :ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
