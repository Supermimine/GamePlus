using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    // Recus
    public class Received
	{
		[Key]
        [Display(Name = "ID")]
        public int ReceivedID { get; set;}
        [Display(Name = "ID de la commande")]
        public int OrderId { get; set; }
        [Display(Name = "Commande")]
        public Order Order { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
