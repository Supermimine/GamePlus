using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net.Mail;
using TPGamePlus.Data;
using TPGamePlus.Domain;
using TPGamePlus.Domain.Entities;
using TPGamePlus.Models.Account;
using System.Configuration;
using static TPGamePlus.Models.Account.RegistrationViewModel;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace TPGamePlus.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly GamePlusDbContext _context;

		public AccountController(SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			GamePlusDbContext context)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_roleManager = roleManager;
			_context = context;
		}



		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			ViewBag.UserOrder = _context.Orders.Where(x => x.UserName == User.Identity.Name).ToList();

			string userRole = "";
			if (User.IsInRole("Client"))
				userRole = "Client";
			else if (User.IsInRole("Administrateur"))
				userRole = "Administrateur";

			user.Role = userRole;

			return View(user);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult LogIn()
		{
			return View();
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Registration(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ViewBag.returnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]

		public async Task<IActionResult> Registration(RegistrationViewModel p_modèle,
			string returnUrl = null)
		{
			try
			{
				returnUrl ??= Url.Content("~/");

				var existingAddress = await _context.Address
				  .SingleOrDefaultAsync(ad => ad.PostalCode == p_modèle.PostalCode &&
					  ad.Province == p_modèle.State &&
					  ad.City == p_modèle.City &&
					  ad.Country == p_modèle.Country &&
					  ad.StreetAddress == p_modèle.StreetAddress);

				Address newAddress = new Address();
				if (existingAddress == null)
				{
					newAddress.Country = p_modèle.Country;
					newAddress.PostalCode = p_modèle.PostalCode;
					newAddress.Province = p_modèle.State;
					newAddress.City = p_modèle.City;
					newAddress.StreetAddress = p_modèle.StreetAddress;
					_context.Address.Add(newAddress);
				}

				if (ModelState.IsValid)
				{

					ApplicationUser utilisateur = new ApplicationUser
					{
						UserName = p_modèle.Email,
						Email = p_modèle.Email,
						FirstName = p_modèle.FirstName,
						LastName = p_modèle.LastName,
						PhoneNumber = p_modèle.PhoneNumber,

					};
					IdentityResult résultat =
						await _userManager.CreateAsync(utilisateur, p_modèle.Password);

					if (résultat.Succeeded)
					{

						if (!await _roleManager.RoleExistsAsync(RoleUtilisateurs.Administrateur.ToString()))
						{
							await _roleManager.CreateAsync(new IdentityRole(RoleUtilisateurs.Administrateur.ToString()));
						}
						if (!await _roleManager.RoleExistsAsync(RoleUtilisateurs.Gérant.ToString()))
						{
							await _roleManager.CreateAsync(new IdentityRole(RoleUtilisateurs.Gérant.ToString()));
						}
						if (!await _roleManager.RoleExistsAsync(RoleUtilisateurs.Client.ToString()))
						{
							await _roleManager.CreateAsync(new IdentityRole(RoleUtilisateurs.Client.ToString()));
						}
						var userRole = await _userManager.GetUsersInRoleAsync(RoleUtilisateurs.Administrateur.ToString());


						await _userManager.AddToRoleAsync(utilisateur, RoleUtilisateurs.Client.ToString());

						utilisateur.Role = RoleUtilisateurs.Client.ToString();

						if (_userManager.Options.SignIn.RequireConfirmedAccount)
						{
							// return RedirectToPage("RegisterConfirmation", new { email =p_modèle.Email, returnUrl = returnUrl });
						}
						else
						{
							await _signInManager.SignInAsync(utilisateur, isPersistent: false);

							if (existingAddress != null)
							{
								_context.Address.Attach(existingAddress);
								utilisateur.Addresses.Add(existingAddress);
							}
							else
								utilisateur.Addresses.Add(newAddress);
							await _context.SaveChangesAsync();

							return LocalRedirect(returnUrl);
						}
					}
					else
					{
						foreach (IdentityError erreur in résultat.Errors)
						{
							ModelState.AddModelError("", erreur.Description);
						}
					}
				}
				ViewBag.returnUrl = returnUrl;
				return View(p_modèle);

			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> LogIn(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var result = await _signInManager.PasswordSignInAsync(
					model.UserName, model.Password, model.RememberMe, false);

				if (!result.Succeeded)
				{
					ModelState.AddModelError(string.Empty, "LogIn Failed!");
					return View(model);
				}

				return RedirectToAction("Index", "Home");

			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}

		[Authorize]
		public async Task<IActionResult> LogOut()
		{
			try
			{
				await _signInManager.SignOutAsync();

				return RedirectToAction("Index", "Home");
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}

		[AllowAnonymous]
		public IActionResult AccesRefuse()
		{
			return View();
		}



		public IActionResult AddAddress()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddAddress(AddressViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				System.Security.Claims.ClaimsPrincipal currentUser = this.User;
				var userid = _userManager.GetUserId(User);
				var userAU = _context.ApplicationUsers.Find(userid);

				var existingAddress = await _context.Address
				.SingleOrDefaultAsync(ad => ad.PostalCode == model.PostalCode &&
				ad.Province == model.State &&
				ad.City == model.City &&
				ad.Country == model.Country &&
				ad.StreetAddress == model.StreetAddress);

				Address newAddress = new Address();
				if (existingAddress == null)
				{
					newAddress.Country = model.Country;
					newAddress.PostalCode = model.PostalCode;
					newAddress.Province = model.State;
					newAddress.City = model.City;
					newAddress.StreetAddress = model.StreetAddress;
					_context.Address.Add(newAddress);
					userAU.Addresses.Add(newAddress);
					await _context.SaveChangesAsync();

				}
				else
				{
					_context.Address.Attach(existingAddress);
					userAU.Addresses.Add(existingAddress);
					await _context.SaveChangesAsync();
				}

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", "Home");
			}
		}

		public IActionResult EditAddress(int id)
		{
			var ad = _context.Address.Find(id);
			var address = new AddressViewModel
			{

			};
			address.City = ad.City;
			address.Country = ad.Country;
			address.PostalCode = ad.PostalCode;
			address.State = ad.Province;
			address.StreetAddress = ad.StreetAddress;


			return View(address);
		}

		public IActionResult EditSaveAddress(AddressViewModel model, int id)
		{
			var ad = _context.Address.Find(id);
			ad.City = model.City;
			ad.Country = model.Country;
			ad.PostalCode = model.PostalCode;
			ad.Province = model.State;
			ad.StreetAddress = model.StreetAddress;

			_context.Address.Update(ad);
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		public IActionResult DeleteAddress(int id)
		{
			try
			{
				var address = _context.Address.Find(id);
				if (address != null)
					_context.Address.Remove(address);
			}
			catch
			{
				ModelState.AddModelError(string.Empty, "Erreur !!!");
				return View();
			}
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		[AllowAnonymous]
		void SendMail(string to, string subject, string message)
		{
			var cm = ConfigurationManager.AppSettings["ChangeMdpEmail"] ?? String.Empty;
			if (cm != null)
			{
				var fromAddress = cm;
				using (var email = new MailMessage(fromAddress, to, subject, message))
				{
					SendMail(email);
				}
			}
		}

		[AllowAnonymous]
		void SendMail(MailMessage mailMessage)
		{
			try
			{
				if (mailMessage != null)
				{
					using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
					{
						smtp.Credentials = new NetworkCredential("NoReply@GamePlus.com", "password");
						smtp.EnableSsl = true;
						smtp.Send(mailMessage);
					}
				}
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e.Message);
			}
		}

		[AllowAnonymous]
		public IActionResult ChangeMDP(string email)
		{
			var subject = "Gameplus - Reset Password";
			var message = "<div style=\"padding: 25px; text-align: center;\">\r\n  <h1 style=\"margin-bottom: 50px;\">GamePlus</h1>\r\n  <div style=\"background-color:#F6F6F6; height:300px; padding: 25px;\">\r\n    <h2> Reset Password</h5>\r\n    <br/><br/>\r\n    <p>If you're lost password or wish to reset it, use the link below to get started.</p>\r\n    <br/>\r\n     <a href=\"#\" style=\"text-decoration: none; cursor: pointer; background-color: #c64646; padding: 15px;\">Reset Your Password</a>\r\n    <br/><br/><br/>\r\n        <p style=\"opacity: 0.7;\">If you did not request reset, you can safely ignore this email. Only a person with acess to your email can reset your account password.</p>\r\n  </div>\r\n</div>";

			SendMail(email, subject, message);

			return RedirectToAction(nameof(LogIn));
		}
	}
}