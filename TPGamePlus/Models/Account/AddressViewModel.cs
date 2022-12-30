using FluentValidation;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using TPGamePlus.Domain.Entities;

namespace TPGamePlus.Models.Account
{
    public class AddressViewModel
    {
        [Display(Name = "Rue de l'adresse")]
        public string StreetAddress { get; set; }

        [Display(Name = "Ville")]
        public string City { get; set; }

        [Display(Name = "État ou Province")]
        public string State { get; set; }

        [Display(Name = "Code Postal")]
        public string PostalCode { get; set; }

        [Display(Name = "Pays")]
        public string Country { get; set; }

        [Display(Name = "Addresse")]
        public int? Address { get; set; }
    }

    public class AddressValidator : AbstractValidator<AddressViewModel>
    {
        public AddressValidator()
        {
                RuleFor(vm => vm.StreetAddress).NotEmpty().WithMessage("Vous devez entrez votre adresse")
                    .MinimumLength(5).WithMessage("Le nom de la rue et l'adresse doivent contenir au moins 5 caractères.")
                    .Matches("^[a-zA-Z0-9-éèêîûàâ ]+$").WithMessage("Certains caractères ne sont pas acceptés...");

                RuleFor(vm => vm.City).NotEmpty().WithMessage("Vous devez entrer votre ville")
                    .Matches("^[a-zA-Z0-9-éèêîûàâ ]+$").WithMessage("Certains caractères ne sont pas acceptés...");

                RuleFor(vm => vm.Country).NotEmpty().WithMessage("Vous devez entrez votre pays")
                    .Matches("^[a-zA-Z-éèêîûàâ ]+$").WithMessage("Certains caractères ne sont pas acceptés...");

                RuleFor(vm => vm.State).NotEmpty().WithMessage("Vous devez entrer votre état ou votre province")
                    .Matches("^[a-zA-Z-éèêîûàâ ]+$").WithMessage("Certains caractères ne sont pas acceptés...");

                RuleFor(vm => vm.PostalCode).NotEmpty().WithMessage("Vous devez entrez votre code postal")
                    .Matches("^[a-zA-Z0-9-]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
