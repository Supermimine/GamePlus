using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Admin
{
    public class PlateformeViewModel
    {
        [Display(Name = "ID")]
        public int PlateformeID { get; set; }
        [Display(Name = "Plateforme")]
        public string PlateformeName { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }

    public class PlateformeValidator : AbstractValidator<PlateformeViewModel>
    {
        public PlateformeValidator()
        {
            //RuleFor(vm => vm.PlateformeName)
            //     .NotEmpty().WithMessage("Le nom de la compagnie est requis.")
            //     .Length(2, 50).WithMessage("Le nom de la compagnie doit contenir entre 2 et 50 lettres.");
            //     //.Matches("^[a-zA-Z0-9-éÉèÈêÊàÀùÙçÇ]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
