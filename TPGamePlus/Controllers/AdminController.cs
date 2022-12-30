using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using TPGamePlus.Data;
using TPGamePlus.Domain.Entities;
using TPGamePlus.Models.Admin;

namespace TPGamePlus.Controllers
{
    [Authorize(Roles = "Administrateur")]
    public class AdminController : Controller
    {
        private readonly GamePlusDbContext _context;
        private readonly IHostEnvironment _hostEnvironment;

        public AdminController(GamePlusDbContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult IndexAdmin()
        {
            return View();
        }

        public IActionResult EditProduct(int id)
        {
            ViewBag.Plateformes = new SelectList(_context.Plateformes, "PlateformeID", "PlateformeName", "isMainShopFilter");
            ViewBag.Status = new SelectList(_context.Status, "StatusID", "StatusName", "isMainShopFilter");
            ViewBag.Publishers = new SelectList(_context.Publishers, "PublisherID", "PublisherName", "isMainShopFilter");
            ViewBag.Compagny = new SelectList(_context.Compagnies, "CompagnyID", "CompagnyName", "isMainShopFilter");
            ViewBag.Categories = new SelectList(_context.Category, "CategoryID", "CategoryName", "isMainShopFilter");
            var p = _context.Products.Find(id);
            ProductInfo pi = _context.ProductsInfos.Find(p.ProductInfoID);
            var product = new ProductViewModel
            {

            };
            product.Name = p.Name;
            product.Price = p.Price;
            product.ActualPrice = p.ActualPrice;
            product.Quantity = p.Quantity;

            product.Images = p.PathImage;

            if (p.StatusID != null)
                product.Available = (int)p.StatusID;
            if (p.PublisherID != null)
                product.Publisher = (int)p.PublisherID;
            if (p.CategoryID != null)
                product.Category = (int)p.CategoryID;
            if (p.CompagnyID != null)
                product.Compagny = (int)p.CompagnyID;
            if (p.PlateformeID != null)
                product.Plateforme = (int)p.PlateformeID;
            product.ProductID = p.ProductID;
            if (pi != null)
                product.Description = pi.Description;
            else
                product.Description = "";

            return View(product);
        }

        [HttpPost]
        public IActionResult EditSaveProduct(ProductViewModel model, IFormFile? file, int id)
        {
            try
            {
                Product p = _context.Products.Find(id);
                ProductInfo pi = _context.ProductsInfos.Find(p.ProductInfoID);

                if (file != null)
                {
                    string wwwRootPath = "wwwroot";
                    var uploads = Path.Combine(wwwRootPath, @"img\Produit\");


                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, model.Name + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    model.Images = @"/img/Produit/" + model.Name + extension;
                }

                if (p.PathImage != null && model.Images == null)
                    p.PathImage = p.PathImage;
                else
                    p.PathImage = model.Images;

                p.Name = model.Name;
                p.Price = model.Price;
                p.ActualPrice = model.ActualPrice;
                p.Quantity = model.Quantity;
                pi.Description = model.Description;

                if (model.Plateforme != null)
                    p.PlateformeID = model.Plateforme;
                else
                    p.PlateformeID = 0;

                if (model.Publisher != null)
                    p.PublisherID = model.Publisher;

                if (model.Compagny != null)
                    p.CompagnyID = model.Compagny;
                else
                    p.PublisherID = 0;

                if (model.Available != null)
                    p.StatusID = model.Available;
                else
                    p.StatusID = 0;

                if (model.Category != null)
                    p.CategoryID = model.Category;
                else
                    p.CategoryID = 0;

                _context.Products.Update(p);
                _context.ProductsInfos.Update(pi);
                _context.SaveChanges();
            }
            catch
            {

            }

            return RedirectToAction(nameof(ListProduct));
        }

        public IActionResult ManageProduct()
        {

            ViewBag.Plateformes = new SelectList(_context.Plateformes, "PlateformeID", "PlateformeName", "isMainShopFilter");
            ViewBag.Status = new SelectList(_context.Status, "StatusID", "StatusName", "isMainShopFilter");
            ViewBag.Publishers = new SelectList(_context.Publishers, "PublisherID", "PublisherName", "isMainShopFilter");
            ViewBag.Compagny = new SelectList(_context.Compagnies, "CompagnyID", "CompagnyName", "isMainShopFilter");
            ViewBag.Categories = new SelectList(_context.Category, "CategoryID", "CategoryName", "isMainShopFilter");
            return View();
        }
        [HttpPost]
        public IActionResult ManageProduct(ProductViewModel model, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Plateformes = new SelectList(_context.Plateformes, "PlateformeID", "PlateformeName", "isMainShopFilter");
                ViewBag.Status = new SelectList(_context.Status, "StatusID", "StatusName", "isMainShopFilter");
                ViewBag.Publishers = new SelectList(_context.Publishers, "PublisherID", "PublisherName", "isMainShopFilter");
                ViewBag.Compagny = new SelectList(_context.Compagnies, "CompagnyID", "CompagnyName", "isMainShopFilter");
                ViewBag.Categories = new SelectList(_context.Category, "CategoryID", "CategoryName", "isMainShopFilter");
                return View(model);
            }

            string wwwRootPath = "wwwroot";
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"img\Produit\");

                var extension = Path.GetExtension(file.FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, model.Name + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                model.Images = @"/img/Produit/" + model.Name + extension;
            }
            try
            {
                var productInfo = new ProductInfo
                {
                    Description = model.Description
                };

                _context.ProductsInfos.Add(productInfo);
                _context.SaveChanges();

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    ActualPrice = model.ActualPrice,
                    Quantity = model.Quantity,
                    PathImage = model.Images,
                    StatusID = model.Available,
                    PlateformeID = model.Plateforme,
                    CategoryID = model.Category,
                    CompagnyID = model.Compagny,
                    ProductInfoID = productInfo.ProductInfoID
                };

                if (model.Publisher != null)
                {
                    product.PublisherID = model.Publisher;
                }

                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(ListProduct));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult ListProduct()
        {
            try
            {
                var product = _context.Products.Select(p => new Product
                {
                    Name = p.Name,
                    ProductID = p.ProductID,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    ActualPrice = p.ActualPrice,
                    Plateforme = p.Plateforme,
                    Publisher = p.Publisher,
                    Compagny = p.Compagny,
                    Status = p.Status,
                    PathImage = p.PathImage,
                    ProductInfo = p.ProductInfo
                });

                if (product != null)
                    return View(product);
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                var info = _context.ProductsInfos.Find(id);
                if (product != null && info != null)
                {
                    _context.ProductsInfos.Remove(info);
                    _context.Products.Remove(product);


                    int pos = product.PathImage.LastIndexOf(".");
                    var extension = product.PathImage.Substring(pos, product.PathImage.Length - pos);

                    System.IO.File.Delete("wwwroot\\img\\Produit\\" + product.Name + extension);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListProduct));
        }



        public IActionResult ManageStatus()
        {
            return View();
        }

        public IActionResult EditStatus(int id)
        {
            var svm = _context.Status.Find(id);
            var status = new StatusViewModel
            {

            };
            status.Status = svm.StatusName;
            status.isMainShopFilter = svm.isMainShopFilter;
            status.StatusID = svm.StatusID;

            return View(status);
        }

        public IActionResult EditSaveStatus(StatusViewModel model, int id)
        {

            var ss = _context.Status.Find(id);
            ss.StatusName = model.Status;
            ss.isMainShopFilter = model.isMainShopFilter;
            _context.Status.Update(ss);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListStatus));
        }

        [HttpPost]
        public IActionResult ManageStatus(StatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var status = new Status
                {
                    StatusName = model.Status,
                    isMainShopFilter = model.isMainShopFilter
                };

                _context.Status.Add(status);
                _context.SaveChanges();

                return RedirectToAction(nameof(ListStatus));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(ListStatus));
            }
        }

        public IActionResult ListStatus()
        {
            try
            {
                var status = _context.Status.Select(s => new Status
                {
                    StatusName = s.StatusName,
                    StatusID = s.StatusID,
                    isMainShopFilter = s.isMainShopFilter
                });

                if (status != null)
                    return View(status);
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeleteStatus(int id)
        {
            try
            {
                var status = _context.Status.Find(id);
                if (status != null)
                    _context.Status.Remove(status);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListStatus));
        }



        public IActionResult ManagePlateforme()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManagePlateforme(PlateformeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var plateforme = new Plateforme
                {
                    PlateformeName = model.PlateformeName,
                    isMainShopFilter = model.isMainShopFilter
                };

                _context.Plateformes.Add(plateforme);
                _context.SaveChanges();

                return RedirectToAction(nameof(ListPlateforme));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult EditPlateforme(int id)
        {
            var cpvm = _context.Plateformes.Find(id);
            var plateforme = new PlateformeViewModel
            {

            };
            plateforme.PlateformeName = cpvm.PlateformeName;
            plateforme.isMainShopFilter = cpvm.isMainShopFilter;
            plateforme.PlateformeID = cpvm.PlateformeID;

            return View(plateforme);
        }

        public IActionResult EditSavePlateforme(PlateformeViewModel model, int id)
        {

            var cp = _context.Plateformes.Find(id);
            cp.PlateformeName = model.PlateformeName;
            cp.isMainShopFilter = model.isMainShopFilter;
            _context.Plateformes.Update(cp);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListPlateforme));
        }

        public IActionResult ListPlateforme()
        {
            try
            {
                var plateforme = _context.Plateformes.Select(c => new Plateforme
                {
                    PlateformeName = c.PlateformeName,
                    PlateformeID = c.PlateformeID,
                    isMainShopFilter = c.isMainShopFilter
                });

                if (plateforme != null)
                    return View(plateforme);
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeletePlateforme(int id)
        {
            try
            {
                var plateforme = _context.Plateformes.Find(id);
                if (plateforme != null)
                    _context.Plateformes.Remove(plateforme);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListPlateforme));
        }



        public IActionResult ManageCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManageCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var category = new Category
                {
                    CategoryName = model.Category,
                    isMainShopFilter = model.isMainShopFilter
                };

                _context.Category.Add(category);
                _context.SaveChanges();

                return RedirectToAction(nameof(ListCategory));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult EditCategory(int id)
        {
            var cvm = _context.Category.Find(id);
            var category = new CategoryViewModel
            {

            };
            category.Category = cvm.CategoryName;
            category.isMainShopFilter = cvm.isMainShopFilter;
            category.CategoryID = cvm.CategoryID;

            return View(category);
        }

        public IActionResult EditSaveCategory(CategoryViewModel model, int id)
        {

            var c = _context.Category.Find(id);
            c.CategoryName = model.Category;
            c.isMainShopFilter = model.isMainShopFilter;
            _context.Category.Update(c);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListCategory));
        }

        public IActionResult ListCategory()
        {
            try
            {
                var categories = _context.Category.Select(c => new Category
                {
                    CategoryName = c.CategoryName,
                    CategoryID = c.CategoryID,
                    isMainShopFilter = c.isMainShopFilter
                });

                if (categories != null)
                    return View(categories);
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _context.Category.Find(id);
                if (category != null)
                    _context.Category.Remove(category);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListCategory));
        }



        public IActionResult ManagePublisher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManagePublisher(PublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var publisher = new Publisher
                {
                    PublisherName = model.Publisher,
                    isMainShopFilter = model.isMainShopFilter
                };

                _context.Attach(publisher);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la création du publisher");
                return View(model);
            }
            return RedirectToAction(nameof(ListPublisher));
        }

        public IActionResult EditPublisher(int id)
        {
            var p = _context.Publishers.Find(id);
            var publisher = new PublisherViewModel
            {

            };
            publisher.Publisher = p.PublisherName;
            publisher.isMainShopFilter = p.isMainShopFilter;
            publisher.PublisherID = p.PublisherID;

            return View(publisher);
        }

        public IActionResult EditSavePublisher(PublisherViewModel model, int id)
        {

            var p = _context.Publishers.Find(id);
            p.PublisherName = model.Publisher;
            p.isMainShopFilter = model.isMainShopFilter;
            _context.Publishers.Update(p);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListPublisher));
        }

        public IActionResult ListPublisher()
        {
            try
            {
                var publishers = _context.Publishers.Select(p => new Publisher
                {
                    PublisherName = p.PublisherName,
                    PublisherID = p.PublisherID,
                    isMainShopFilter = p.isMainShopFilter
                });

                if (publishers != null)
                    return View(publishers);
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeletePublisher(int id)
        {
            try
            {
                var publisher = _context.Publishers.Find(id);
                if (publisher != null)
                    _context.Publishers.Remove(publisher);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListPublisher));
        }



        public IActionResult ManageCompagny()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManageCompagny(CompagnyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var compagny = new Compagny
                {
                    CompagnyName = model.Compagny,
                    isMainShopFilter = model.isMainShopFilter
                };

                _context.Attach(compagny);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erreur lors de la création du publisher");
                return View(model);
            }
            return RedirectToAction(nameof(ListCompagny));
        }

        public IActionResult EditCompagny(int id)
        {
            var p = _context.Compagnies.Find(id);
            var compagny = new CompagnyViewModel
            {

            };
            compagny.Compagny = p.CompagnyName;
            compagny.isMainShopFilter = p.isMainShopFilter;
            compagny.CompagnyID = p.CompagnyID;

            return View(compagny);
        }

        public IActionResult EditSaveCompagny(CompagnyViewModel model, int id)
        {

            var p = _context.Compagnies.Find(id);
            p.CompagnyName = model.Compagny;
            p.isMainShopFilter = model.isMainShopFilter;
            _context.Compagnies.Update(p);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListCompagny));
        }

        public IActionResult ListCompagny()
        {
            try
            {
                var compagnies = _context.Compagnies.Select(p => new Compagny
                {
                    CompagnyName = p.CompagnyName,
                    CompagnyID = p.CompagnyID,
                    isMainShopFilter = p.isMainShopFilter
                });

                if (compagnies != null)
                    return View(compagnies);
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeleteCompagny(int id)
        {
            try
            {
                var compagny = _context.Compagnies.Find(id);
                if (compagny != null)
                    _context.Compagnies.Remove(compagny);
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListCompagny));
        }



        public IActionResult ListOrders()
        {
            try
            {
                var orders = _context.Orders.Select(p => new Order
                {
                    OrderID = p.OrderID,
                    UserName = p.UserName,
                    IsConfirmed = p.IsConfirmed,
                    Status = p.Status,
                    PriceNoTaxe =p.PriceNoTaxe,
                    Taxe = p.Taxe,
                    PriceWithTaxe = p.PriceWithTaxe,
                    Items = p.Items
                });

                if (orders != null)
                {


                    return View(orders);
                }
                else
                    return View();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
        }

        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);

                foreach (var product in _context.OrderItems)
                {
                    if (product.OrderId == id)
                        _context.OrderItems.Remove(product);
                }

                if (order != null)
                    _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOrders));
        }

        public IActionResult ConfirmOrder(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order != null)
                {
                    order.Status = (int)OrderStatus.Accepter - 1;
                    order.VerifiedDate = DateTime.Now;

                    var invoice = new Invoice
                    {
                        IsPaid = false,
                        PriceNoTaxe = order.PriceNoTaxe,
                        PriceWithTaxe = order.PriceWithTaxe,
                        Taxe = order.Taxe,
                        UserName = order.UserName
                    };
                    _context.Invoices.Add(invoice);
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOrders));
        }
        public IActionResult DeclineOrder(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order != null)
                {
                    order.Status = (int)OrderStatus.Refuser - 1;
                    order.VerifiedDate = DateTime.Now;
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOrders));
        }
        public IActionResult SetShippingOrder(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order != null)
                {
                    order.Status = (int)OrderStatus.Livraison - 1;
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOrders));
        }
        public IActionResult completOrder(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order != null)
                {
                    order.Status = (int)OrderStatus.Arriver - 1;
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Erreur !!!");
                return View();
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(ListOrders));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _context.Products.ToList();
            return Json(new { data = productList });
        }
        #endregion
    }

}
