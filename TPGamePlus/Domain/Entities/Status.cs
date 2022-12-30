using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class Status
    {
        [Key]
        [Display(Name = "ID")]
        public int StatusID { get; set; }
        [Display(Name = "Status")]
        public string StatusName { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
}
