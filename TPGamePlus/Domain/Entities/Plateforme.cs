using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class Plateforme
    {
        [Key]
        [Display(Name = "ID")]
        public int PlateformeID { get; set; }
        [Display(Name = "Plateforme")]
        public string PlateformeName { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
}
