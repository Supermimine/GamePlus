using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        public int CategoryID { get; set; }
        [Display(Name = "Catégorie")]
        public string CategoryName { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
}
