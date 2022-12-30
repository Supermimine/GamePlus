using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using TPGamePlus.Data;
using TPGamePlus.Domain.Entities;

namespace TPGamePlus.ViewComponents
{
	public class OrderItemList : ViewComponent
    {
        private readonly GamePlusDbContext _context;

        public OrderItemList(GamePlusDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string sName, int id)
        {
            var orderItem = _context.OrderItems.Where(p => p.OrderItemName == sName && p.OrderId == id);

            var p = new List<string>();
            foreach (var product in orderItem)
            {
                p.Add(_context.Products.Where(x => x.ProductID == product.ProductId).Select(x => x.Name).FirstOrDefault());

            }

            ViewBag.OrderItem = p;

            return View(orderItem);
        }
    }
}