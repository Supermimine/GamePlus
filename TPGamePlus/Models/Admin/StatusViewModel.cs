using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Admin
{
    public class StatusViewModel
    {
        [Display(Name = "ID")]
        public int StatusID { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; } = false;
    }
    public class StatusValidator : AbstractValidator<StatusViewModel>
    {
        public StatusValidator()
        {
            //RuleFor(vm => vm.Status)
            //     .NotEmpty().WithMessage("Le status est requis.")
            //     .Length(2, 30).WithMessage("Le status doit contenir entre 2 et 30 lettres.");
            //     //.Matches("^[a-zA-Z0-9-éÉèÈêÊàÀùÙçÇ]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
