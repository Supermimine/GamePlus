using Microsoft.AspNetCore.Mvc;

namespace TPGamePlus.ViewComponents
{
    public class FormContactUs : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }

    }
}
