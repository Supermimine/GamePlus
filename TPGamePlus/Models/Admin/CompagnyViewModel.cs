using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Admin
{
    public class CompagnyViewModel
    {
        [Display(Name = "ID")]
        public int CompagnyID { get; set; }
        [Display(Name = "Compagnie")]
        public string Compagny { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
    public class CompagnyValidator : AbstractValidator<CompagnyViewModel>
    {
        public CompagnyValidator()
        {

        }
    }
}
