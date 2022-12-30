using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TPGamePlus.Domain.Entities;

namespace TPGamePlus.Models.Account
{
    public class PaidViewModel
    {
        // Card informations 

        // Nom Prénom
        [Display(Name = "Nom et prénom")]
        public string FullName { get; set; }

        // Addresse mail
        [Display(Name = "Addresse mail")]
        public string EmailAddress { get; set; }

        // Numero de téléphone
        [Display(Name = "Numéro de téléphone")]
        public string PhoneNumber { get; set; }

        // Addresse
        [Display(Name = "Addresse")]
        public Address? address { get; set; }
    }

    public class PaidValidator : AbstractValidator<PaidViewModel>
    {
        private const string REGEX_EMAIL_ADDRESS = @"^[\w\.\-]+@[\w\-]+\.\w{2,}$";
        public PaidValidator()
        {

            RuleFor(c => c.EmailAddress)
                .Matches(REGEX_EMAIL_ADDRESS)
                .When(c => !string.IsNullOrEmpty(c.EmailAddress))
                .WithMessage("Veuillez fournir un format d'adresse courriel valide.");

            RuleFor(vm => vm.PhoneNumber).NotEmpty().WithMessage("Le numéro de téléphone est requis.")
                .Matches("^([1]{0,1}[-( ]{0,1}[0-9]{3}[-) ]{0,2}[0-9]{3}[- ]{0,1}[0-9]{4})+$").WithMessage("Certains caractères ne sont pas acceptés...");


            RuleFor(vm => vm.FullName).NotEmpty().WithMessage("Le nom du titulaire de la carte est requis.")
                .Matches("^[a-zA-Z-éèêîûàâ ]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
