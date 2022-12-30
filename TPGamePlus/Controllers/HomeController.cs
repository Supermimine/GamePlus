using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TPGamePlus.Data;
using TPGamePlus.Domain.Entities;
using TPGamePlus.Models;
using X.PagedList;

namespace TPGamePlus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GamePlusDbContext _context;

        public HomeController(ILogger<HomeController> logger, GamePlusDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Random rnd = new Random();

            //Choice featured products
            var lstVed = new List<Product>();
            lstVed = _context.Products.OrderByDescending(x => x.Quantity).ToList();
            lstVed = lstVed.Take(8).ToList();
            ViewBag.ved = lstVed;

            //Choice products Nintendo
            var lstNin = new List<Product>();
            lstNin = _context.Products.Where(x => x.PlateformeID == 1 && x.CategoryID == 2).ToList();
            lstNin = lstNin.OrderBy(x => rnd.Next()).Take(6).ToList();
            ViewBag.nin = lstNin;

			//Choice products Microsoft
			var lstMic = new List<Product>();
            lstMic = _context.Products.Where(x => x.PlateformeID == 2 && x.CategoryID == 2).ToList();
            lstMic = lstMic.OrderBy(x => rnd.Next()).Take(6).ToList();
            ViewBag.mic = lstMic;

			//Choice products PS4
			var lstPs4 = new List<Product>();
            lstPs4 = _context.Products.Where(x => x.PlateformeID == 3 && x.CategoryID == 2).ToList();
            lstPs4 = lstPs4.OrderBy(x => rnd.Next()).Take(3).ToList();
            ViewBag.ps4 = lstPs4;

			//Choice products PS5
			var lstPs5 = new List<Product>();
            lstPs5 = _context.Products.Where(x => x.PlateformeID == 4 && x.CategoryID == 2).ToList();
            lstPs5 = lstPs5.OrderBy(x => rnd.Next()).Take(3).ToList();
            ViewBag.ps5 = lstPs5;

            //Choice Accessory
            var lstAcc = new List<Product>();
            lstAcc = _context.Products.Where(x => x.CategoryID == 3).ToList();
            lstAcc = lstAcc.OrderBy(x => rnd.Next()).Take(4).ToList();
            ViewBag.acc = lstAcc;

            //Choice Console
            var lstCon = new List<Product>();
            lstCon = _context.Products.Where(x => x.CategoryID == 1).ToList();
            lstCon = lstCon.OrderBy(x => rnd.Next()).Take(4).ToList();
            ViewBag.con = lstCon;



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public async Task<IActionResult> Store(string sortOrder, int? page)
        {
            var products = _context.Products.ToPagedList();

            sortOrder = HttpContext.Request.Query.Take(1).First().Value.ToString().Replace(" ", "").ToUpper();
            ResetFilter();

            ViewBag.sort = sortOrder;
            List<Product> lstProduct = new List<Product>();

            if (sortOrder.Contains("SEARCH:"))
            {
                sortOrder = sortOrder.Remove(0, 7);

                foreach (var product in _context.Products)
                {
                    if (product.Name.ToUpper().Contains(sortOrder))
                    {
                        lstProduct.Add(products.Where(p => p.ProductID == product.ProductID).FirstOrDefault());
                    }
                }

                products = lstProduct.ToPagedList();
            }
            else
            {
                foreach (var publisher in _context.Publishers)
                {
                    if (sortOrder == publisher.PublisherName.Replace(" ", "").ToUpper())
                    {
                        products = products.Where(p => p.PublisherID == publisher.PublisherID).ToPagedList();
                        var com = _context.Publishers.Where(x => x.PublisherID == publisher.PublisherID).FirstOrDefault();
                        com.isMainShopFilter = true;
                    }
                }

                foreach (var category in _context.Category)
                {
                    if (sortOrder == category.CategoryName.ToUpper())
                    {
                        products = products.Where(p => p.CategoryID == category.CategoryID).ToPagedList();
                        var ca = _context.Category.Where(x => x.CategoryID == category.CategoryID).FirstOrDefault();
                        ca.isMainShopFilter = true;
                    }
                }

                foreach (var status in _context.Status)
                {
                    if (sortOrder == status.StatusName.ToUpper())
                    {
                        products = products.Where(p => p.StatusID == status.StatusID).ToPagedList();
                        var s = _context.Status.Where(x => x.StatusID == status.StatusID).FirstOrDefault();
                        s.isMainShopFilter = true;
                    }
                }

                switch (sortOrder)
                {
                    case "NAMEA":
                        products = products.OrderBy(p => p.Name).ToPagedList();
                        break;
                    case "NAMEB":
                        products = products.OrderByDescending(p => p.Name).ToPagedList();
                        break;
                    case "PRICEA":
                        products = products.OrderBy(p => p.ActualPrice).ToPagedList();
                        break;
                    case "PRICEB":
                        products = products.OrderByDescending(p => p.ActualPrice).ToPagedList();
                        break;
                    case "ALLPRICE":
                        products = products.ToPagedList();
                        break;
                    case "SECTIONONEPRICE":
                        products = products.Where(p => p.ActualPrice <= 10).ToPagedList();
                        break;
                    case "SECTIONTWOPRICE":
                        products = products.Where(p => p.ActualPrice >= 10 && p.ActualPrice <= 25).ToPagedList();
                        break;
                    case "SECTIONTHREEPRICE":
                        products = products.Where(p => p.ActualPrice >= 25 && p.ActualPrice <= 50).ToPagedList();
                        break;
                    case "SECTIONFOURPRICE":
                        products = products.Where(p => p.ActualPrice >= 50).ToPagedList();
                        break;
                    case "SALE":
                        products = products.Where(p => p.ActualPrice < p.Price).ToPagedList();
                        break;
                    case "AUCUN":
                        products = products.OrderBy(p => p.Name).ToPagedList();
                        break;
                }
            }

            _context.SaveChanges();

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        private void ResetFilter()
        {
            var a = _context.Publishers.Where(x => x.PublisherID == 1).FirstOrDefault();
            a.isMainShopFilter = false;

            var b = _context.Publishers.Where(x => x.PublisherID == 2).FirstOrDefault();
            b.isMainShopFilter = false;

            var c = _context.Publishers.Where(x => x.PublisherID == 3).FirstOrDefault();
            c.isMainShopFilter = false;

            var d = _context.Publishers.Where(x => x.PublisherID == 4).FirstOrDefault();
            d.isMainShopFilter = false;

            var e = _context.Publishers.Where(x => x.PublisherID == 5).FirstOrDefault();
            e.isMainShopFilter = false;

            var f = _context.Publishers.Where(x => x.PublisherID == 6).FirstOrDefault();
            f.isMainShopFilter = false;

            var g = _context.Publishers.Where(x => x.PublisherID == 7).FirstOrDefault();
            g.isMainShopFilter = false;

            var h = _context.Publishers.Where(x => x.PublisherID == 8).FirstOrDefault();
            h.isMainShopFilter = false;

            var i = _context.Publishers.Where(x => x.PublisherID == 9).FirstOrDefault();
            i.isMainShopFilter = false;

            var j = _context.Publishers.Where(x => x.PublisherID == 10).FirstOrDefault();
            j.isMainShopFilter = false;

            var k = _context.Publishers.Where(x => x.PublisherID == 11).FirstOrDefault();
            k.isMainShopFilter = false;

            var l = _context.Category.Where(x => x.CategoryID == 1).FirstOrDefault();
            l.isMainShopFilter = false;

            var m = _context.Category.Where(x => x.CategoryID == 2).FirstOrDefault();
            m.isMainShopFilter = false;

            var n = _context.Category.Where(x => x.CategoryID == 3).FirstOrDefault();
            n.isMainShopFilter = false;

            var o = _context.Category.Where(x => x.CategoryID == 4).FirstOrDefault();
            o.isMainShopFilter = false;

            var p = _context.Status.Where(x => x.StatusID == 1).FirstOrDefault();
            p.isMainShopFilter = false;

            var q = _context.Status.Where(x => x.StatusID == 2).FirstOrDefault();
            q.isMainShopFilter = false;

            var r = _context.Status.Where(x => x.StatusID == 3).FirstOrDefault();
            r.isMainShopFilter = false;

            _context.SaveChanges();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}