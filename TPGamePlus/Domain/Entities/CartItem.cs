using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGamePlus.Domain.Entities
{
    public class CartItem
    {
        [Key]
        public int CartId { get; set; }
        public string CartName { get; set; }

        public int Quantity { get; set; }
        public int QuantityMax { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
