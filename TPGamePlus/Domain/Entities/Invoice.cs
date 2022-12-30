using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    // Facture
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        public decimal PriceNoTaxe { get; set; }
        public decimal PriceWithTaxe { get; set; }

        public decimal Taxe { get; set; }

        public string UserName { get; set; }
        public bool IsPaid { get; set; }
    }
}
