using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class Publisher
    {
        [Key]
        [Display(Name = "ID")]
        public int PublisherID { get; set; }
        [Display(Name = "Éditeur")]
        public string PublisherName { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
}
