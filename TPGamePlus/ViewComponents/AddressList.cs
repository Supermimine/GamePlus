using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TPGamePlus.Data;
using TPGamePlus.Domain;

namespace TPGamePlus.ViewComponents
{
	public class AddressList: ViewComponent
	{
		private readonly GamePlusDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public AddressList(GamePlusDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var currentUser = (System.Security.Claims.ClaimsPrincipal)User;
			var userid = _userManager.GetUserId(currentUser);
			var address = _context.Address.Where(u => u.Users.Any(us => us.Id == userid)).ToList();

			if (address != null)
				return View(address);
			else
				return View();
		}
	}
}
