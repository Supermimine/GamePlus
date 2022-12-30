using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGamePlus.Domain.Entities
{
    public class ProductInfo
    {
        [Key]
        public int ProductInfoID { get; set; }
        public string Description { get; set; } = "";
        // Livraison ... 
    }
}
