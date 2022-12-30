using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Admin
{
    public class PublisherViewModel
    {
        [Display(Name = "ID")]
        public int PublisherID { get; set; }
        [Display(Name = "Éditeur")]
        public string Publisher { get; set; }
        [Display(Name = "Option de filtre")]
        public bool isMainShopFilter { get; set; }
    }
    public class PublisherValidator : AbstractValidator<PublisherViewModel>
    {
        public PublisherValidator()
        {
            //RuleFor(vm => vm.Publisher).NotEmpty().WithMessage("Le nom de l'éditeur est requis.")
                 //.Length(2, 30).WithMessage("Le nom de l'éditeur doit contenir entre 2 et 30 lettres.");
                 //.Matches("^[a-zA-Z0-9-éÉèÈêÊàÀùÙçÇ]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
