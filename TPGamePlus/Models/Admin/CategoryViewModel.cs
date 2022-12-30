using FluentValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Admin
{
    public class CategoryViewModel
    {
        [Display(Name = "ID")]
        public int CategoryID { get; set; }
        [Display(Name = "Catégorie")]
        public string Category { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator()
        {
            //RuleFor(vm => vm.Category)
            //     .NotEmpty().WithMessage("La catégorie est requise.")
            //     .Length(2, 30).WithMessage("La catégorie doit contenir entre 2 et 30 lettres.");
            //     //.Matches("^[a-zA-Z0-9-éÉèÈêÊàÀùÙçÇ]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
