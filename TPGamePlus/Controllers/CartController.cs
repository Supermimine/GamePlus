using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPGamePlus.Domain.Entities;
using TPGamePlus.Data;
using Microsoft.AspNetCore.Identity;
using TPGamePlus.Domain;
using Microsoft.AspNetCore.Authorization;
using TPGamePlus.Models.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using TPGamePlus.Models;
using Stripe;
using Address = TPGamePlus.Domain.Entities.Address;
using Order = TPGamePlus.Domain.Entities.Order;
using OrderItem = TPGamePlus.Domain.Entities.OrderItem;
using Product = TPGamePlus.Domain.Entities.Product;

namespace TPGamePlus.Controllers
{
	[Authorize]
	public class CartController : Controller
	{
		private readonly IOptions<StripeOptions> _stripeOptions;
		private readonly GamePlusDbContext _context;
		private readonly UserManager<ApplicationUser> _user;
		public CartController(GamePlusDbContext context, UserManager<ApplicationUser> user, IOptions<StripeOptions> stripeOptions)
		{
			_context = context;
			_user = user;
			_stripeOptions = stripeOptions;
		}



		public IActionResult Index()
		{
			return RedirectToAction(nameof(Shop));
		}



		public async Task<IActionResult> Shop()
		{
			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var userid = _user.GetUserId(User);
			var productCart = await _context.ShoppingCartItems.Where(x => x.CartName == _user.GetUserName(User)).ToListAsync();

			foreach (var p in productCart)
			{
				p.Product = await _context.Products.Where(x => x.ProductID == p.ProductId).FirstOrDefaultAsync();
				p.QuantityMax = p.Product.Quantity;
			}

			int nb = 0;
			float subTotal = 0.00f;
			foreach (var item in productCart)
			{
				nb++;
				subTotal += item.Product.ActualPrice * item.Quantity;
			}

			float shipping = 0;
			if (subTotal > 0.00f && subTotal < 150.00f)
				shipping = 30;

			float taxe = 0.14975f * subTotal;
			float total = shipping + subTotal + taxe;

			ViewBag.subTotal = subTotal;
			ViewBag.total = total;
			ViewBag.shipping = shipping;
			ViewBag.nb = nb;
			ViewBag.productName = "";
			ViewBag.addresses = new SelectList((_context.Address
				.Where(u => u.Users.Any(us => us.Id == userid)).ToList()), "AddressID", "StreetAddress");

			ViewBag.addressNb = _context.Address.Where(u => u.Users.Any(us => us.Id == userid)).Count();

			ShopViewModel model = new() { };

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Shop(ShopViewModel model)
		{

			System.Security.Claims.ClaimsPrincipal currentUser = this.User;
			var userid = _user.GetUserId(User);
			var productCart = await _context.ShoppingCartItems.Where(x => x.CartName == _user.GetUserName(User)).ToListAsync();

			int nb = 0;
			float subTotal = 0.00f;

			foreach (var p in productCart)
			{
				p.Product = await _context.Products.Where(x => x.ProductID == p.ProductId).FirstOrDefaultAsync();
				p.QuantityMax = p.Product.Quantity;
				
				nb++;
				subTotal += p.Product.ActualPrice * p.Quantity;
			}

			float shipping = 0;
			if (subTotal > 0.00f && subTotal < 150.00f)
				shipping = 30;

			float taxe = 0.14975f * subTotal;
			float total = shipping + subTotal + taxe;

			ViewBag.subTotal = subTotal;
			ViewBag.total = total;
			ViewBag.shipping = shipping;
			ViewBag.nb = nb;
			ViewBag.productName = "";
			ViewBag.addresses = new SelectList((_context.Address
				.Where(u => u.Users.Any(us => us.Id == userid)).ToList()), "AddressID", "StreetAddress");

			ViewBag.addressNb = _context.Address.Where(u => u.Users.Any(us => us.Id == userid)).Count();

			if (ModelState.IsValid &&
				(model.City != null && model.Country != null && model.State != null && model.StreetAddress != null && model.PostalCode != null ||
				model.Address != null))
			{
				model.JavascriptToRun = "ShowPayment()";

				string sName = "";
				foreach (var item in productCart)
					sName += $"{item.Product.Name} x{item.Quantity} \n";
				ViewBag.productName = sName;

				Address BDaddress;
				if (model.Address == null)
				{
					BDaddress = new()
					{
						StreetAddress = model.StreetAddress,
						City = model.City,
						Country = model.Country,
						PostalCode = model.PostalCode,
						Users = _context.Users.Where(x => x.Id == userid).ToList()
					};
				}
				else
					BDaddress = (Address)_context.Address.Where(x => x.AddressID == model.Address.Value).FirstOrDefault();

				var order = new Order
				{
					Taxe = 0,
					PriceNoTaxe = 0,
					PriceWithTaxe = 0,
					IsConfirmed = false,
					UserName = User.Identity.Name,
					Status = (int)OrderStatus.Incomplete,
					AddressID = BDaddress.AddressID,
					Address = BDaddress,
					EmailAddress = User.Identity.Name
				};
				_context.Orders.Add(order);
				_context.SaveChanges();
			}

			return View(model);
		}


		public async Task<IActionResult> Confirmation(ConfirmationViewModel model)
		{
			var order = _context.Orders.Where(x => x.UserName == User.Identity.Name && x.Status == (int)OrderStatus.Incomplete).FirstOrDefault();
			order.Address = _context.Address.Where(x => x.AddressID == order.AddressID).FirstOrDefault();

			model.StreetAddress = order.Address.StreetAddress;
			model.PostalCode = order.Address.PostalCode;
			model.Country = order.Address.Country;
			model.State = order.Address.Province;
			model.City = order.Address.City;

			model.FullName = ViewBag.fullname;
			model.PhoneNumber = ViewBag.phone;
			model.EmailAddress = ViewBag.email;

			var productCart = await _context.ShoppingCartItems.Where(x => x.CartName == _user.GetUserName(User)).ToListAsync();

			foreach (var p in productCart)
			{
				p.Product = await _context.Products.Where(x => x.ProductID == p.ProductId).FirstOrDefaultAsync();
			}

			int nb = 0;
			float subTotal = 0.00f;
			foreach (var item in productCart)
			{
				nb++;
				subTotal += item.Product.ActualPrice * item.Quantity;
			}

			float shipping = 0;
			if (subTotal > 0.00f && subTotal < 150.00f)
				shipping = 30;

			float taxe = 0.14975f * subTotal;
			float total = shipping + subTotal + taxe;

			ViewBag.subTotal = subTotal;
			ViewBag.total = total;
			ViewBag.shipping = shipping;
			ViewBag.nb = nb;

			return View(model);
		}


		
		public async Task<IActionResult> AddToCart(int id)
		{
			var product = await _context.Products.Where(x => x.ProductID == id).FirstOrDefaultAsync();
			var user = _user.GetUserName(User);

			bool isHaveAnimation = false;
			bool isExist = false;
			var nb = Request.Query["nbItemCart"];
			if (nb.Count == 0)
			{
				nb = new Microsoft.Extensions.Primitives.StringValues(new string[] {"1"});
				isHaveAnimation = true;
			}

			int nbMax = product.Quantity;

			foreach (var item in _context.ShoppingCartItems)
			{
				if (item.ProductId == product.ProductID)
				{
					if (item.Quantity + int.Parse(nb[0]) < item.Quantity)
					{
						item.Quantity += int.Parse(nb[0]);
					}
					isExist = true;
				}
			}

			if (isExist == false)
			{
				_context.ShoppingCartItems.Add(new CartItem
				{
					ProductId = id,
					CartName = user,
					Product = product,
					Quantity = int.Parse(nb[0]),
					QuantityMax = nbMax
				});
			}
			await _context.SaveChangesAsync();

			if (!isHaveAnimation)
				return RedirectToAction("Store", "Home", new { SortOrder = "Aucun", page = 1 });
			else
			{
				Thread.Sleep(1300);
				return RedirectToAction("Store", "Home", new { SortOrder = "Aucun", page = 1 });
			}
		}

		public async Task<IActionResult> Remove(int id)
		{
			var product = await _context.ShoppingCartItems.Where(x => x.CartId == id).FirstOrDefaultAsync();
			_context.ShoppingCartItems.Remove(product);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Shop));
		}

		public int getCount()
		{
			return _context.ShoppingCartItems.Count();
		}



		public IActionResult Plus(int cartId)
		{
			var cart = _context.ShoppingCartItems.Find(cartId);
			if (_context.Products.Where(x => x.ProductID == cart.ProductId).Select(y => y.Quantity).FirstOrDefault() >= (cart.Quantity + 1))
				cart.Quantity += 1;
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Minus(int cartId)
		{
			try
			{
				var cart = _context.ShoppingCartItems.Find(cartId);
				if (cart.Quantity > 1)
				{
					cart.Quantity -= 1;
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{

			}
			return RedirectToAction(nameof(Index));
		}



		public IActionResult ConfirmCart()
		{
			if (_context.ShoppingCartItems.Count() > 0)
			{
				try
				{
					int nb = 0;
					float subTotal = 0.00f;

					var order = _context.Orders.Where(x => x.UserName == User.Identity.Name && x.IsConfirmed == false).OrderBy(x => x.OrderID).LastOrDefault();

					foreach (var item in _context.ShoppingCartItems.Where(x => x.CartName == User.Identity.Name).ToList())
					{
						var product = _context.Products.Where(x => x.ProductID == item.ProductId).FirstOrDefault();
						_context.OrderItems.Add(new OrderItem
						{
							ProductId = item.ProductId,
							OrderItemName = item.CartName,
							Product = product,
							Quantity = item.Quantity,
							OrderId = order.OrderID
						});

						subTotal += product.ActualPrice * product.Quantity;
						nb++;
					}
					_context.SaveChanges();


					float shipping = 0;
					if (subTotal > 0.00f && subTotal < 150.00f)
						shipping = 30;
					float taxe = 0.14975f * subTotal;
					float total = shipping + subTotal + taxe;

					order.IsConfirmed = true;
					order.PriceNoTaxe = (decimal)subTotal;
					order.PriceWithTaxe = (decimal)total;
					order.UserName = User.Identity.Name;
					order.Items = _context.OrderItems.Where(x => x.OrderItemName == User.Identity.Name && x.OrderId == order.OrderID).ToList();
					order.Taxe = (decimal)taxe;
					order.OrderDate = DateTime.Now;

					_context.Orders.Update(order);
					_context.SaveChanges();

					var cart = _context.ShoppingCartItems.ToList();
					foreach (var item in cart)
					{
						Product product = _context.Products.Where(x => x.ProductID == item.ProductId).First();
						product.Quantity -= item.Quantity;
						_context.Products.Update(product);
						_context.SaveChanges();

						_context.ShoppingCartItems.Remove(item);
						_context.SaveChanges();
					}
				}
				catch (Exception e)
				{
					System.Console.WriteLine(e.Message);
				}

				return RedirectToAction("Store", "Home", new { SortOrder = "Aucun", page = 1 });
			}
			else
				return RedirectToAction(nameof(Shop));
		}



		[HttpPost]
		public JsonResult Charges([FromBody] ChargesModel model)
		{
			StripeConfiguration.SetApiKey(_stripeOptions.Value.SecretKey);

			var options = new ChargeCreateOptions
			{
				Amount = model.AmountInCents,
				Description = model.Description,
				SourceId = model.Token,
				Currency = model.CurrencyCode
			};
			var service = new ChargeService();
			Charge charge = service.Create(options);

			return Json(charge.ToJson());
		}
	}
}
