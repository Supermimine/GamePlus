using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGamePlus.Domain.Entities
{
    public enum OrderStatus
    {
        Incomplete,
        EnAttente,
        Accepter,
        Refuser,
        Livraison,
        Arriver
    };

    // Commande
    public class Order
    {
        [Key]
        [Display(Name = "ID")]
        public int OrderID { get; set; }
        [Display(Name = "Confirmation")]
        public bool IsConfirmed { get; set; }
        public int Status { get; set; }



        [Display(Name = "Prix sans taxe")]
        public decimal PriceNoTaxe { get; set; }
        [Display(Name = "Prix avec taxe")]
        public decimal PriceWithTaxe { get; set; }
        public decimal Taxe { get; set; }



        [Display(Name = "Date de commande")]
        public DateTime? OrderDate { get; set; }
        [Display(Name = "Date de vérification")]
        public DateTime? VerifiedDate { get; set; }
        [Display(Name = "Nom du client")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }


        public virtual Address? Address { get; set; }
        public int? AddressID { get; set; }



        public List<OrderItem> Items { get; set; }

    }
}
