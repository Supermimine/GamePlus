using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class Compagny
    {
        [Key]
        [Display(Name = "ID")]
        public int CompagnyID { get; set; }
        [Display(Name = "Compagnie")]
        public string CompagnyName { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
}
