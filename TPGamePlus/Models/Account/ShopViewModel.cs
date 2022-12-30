using FluentValidation;

namespace TPGamePlus.Models.Account
{
    public class ShopViewModel : AddressViewModel
    {
        public string JavascriptToRun { get; set; }
    }

    public class ShopValidator : AbstractValidator<ShopViewModel>
    {
        public ShopValidator()
        {
            When(x => x.Address == null, () =>
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
            });
        }
    }
}
