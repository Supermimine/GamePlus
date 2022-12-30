using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGamePlus.Domain.Entities
{
    public class Product
    {
        [Key]
        [Display(Name = "ID")]
        public int ProductID { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; } = "";
        [Display(Name = "Prix")]
        public float Price { get; set; }
        [Display(Name = "Prix actuel")]
        public float ActualPrice { get; set; }
        [Display(Name = "Image")]
        public string PathImage { get; set; }
        [Display(Name = "Quantité")]
        public int Quantity { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
        public int? ProductInfoID { get; set; }

        public virtual Status Status { get; set; }
        public int? StatusID { get; set; }

        public int? PlateformeID { get; set; }
        public Plateforme? Plateforme { get; set; }

        public virtual Publisher? Publisher { get; set; }
        public int? PublisherID { get; set; }

        [Display(Name = "Compagnie")]
        public virtual Compagny? Compagny { get; set; }
        public int? CompagnyID { get; set; }

        [Display(Name = "Catégorie")]
        public virtual Category? Category { get; set; }
        public int? CategoryID { get; set; }
    }
}
