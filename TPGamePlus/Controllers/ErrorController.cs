using Microsoft.AspNetCore.Mvc;

namespace TPGamePlus.Controllers
{
	public class ErrorController : Controller
	{

        public IActionResult Error404()
        {
            return View();
        }
    }
}
