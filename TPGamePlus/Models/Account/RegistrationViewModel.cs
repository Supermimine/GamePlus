using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPGamePlus.Models.Account
{
    public class RegistrationViewModel
    {

        public enum RoleUtilisateurs
        {
            Gérant,
            Client,
            Administrateur
        }

        [Required(ErrorMessage = "Vous devez entrer un prénom")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vous devez entrer un nom")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [StringLength(20, ErrorMessage = "La longueur du mot de passe doît se situer entre {2} et {1}", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]

        [Required(ErrorMessage = "Entrez un mot de passe!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmer le mot de passe")]

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et  le mot de passe confirmé sont différents.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Entrez un courriel")]
        //[EmailAddress(ErrorMessage = "Il ne s'agit pas d'un adresse courriel valide!")]
        [Display(Name = "Adresse courriel")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vous devez entrer un numéro de télephone")]
        [Phone(ErrorMessage = "Le format du téléphone n'est pas valide!")]
        [StringLength(20, ErrorMessage = "La longueur ne correspond pas!", MinimumLength = 10)]
        [RegularExpression("^([1]{0,1}[-( ]{0,1}[0-9]{3}[-) ]{0,2}[0-9]{3}[- ]{0,1}[0-9]{4})+$", ErrorMessage="Le numéro de télephone invalide")]
        [Display(Name = "Numéro de téléphone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vous devez entrer une rue")]
        [Display(Name = "Rue")]

        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Vous devez entrer une ville")]
        [Display(Name = "Ville")]

        public string City { get; set; }
        [Required(ErrorMessage = "Vous devez entrer une État ou Province")]
        [Display(Name = "État ou Province")]
        public string State { get; set; }
        [Display(Name = "Code Postal")]
        [StringLength(10, ErrorMessage = "La longueur du code postal doît se situer entre {2} et {1}", MinimumLength = 2)]

        [Required(ErrorMessage = "Vous devez entrer un État ou Code Postal")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Vous devez entrer un pays")]
        [Display(Name = "Pays")]

        public string Country { get; set; }

        public RoleUtilisateurs? Role { get; set; }
        [NotMapped]
        public string ReturnUrl { get; set; }

    }

    public class RegistrationValidator : AbstractValidator<RegistrationViewModel>
    {
        private const string REGEX_EMAIL_ADDRESS = @"^[\w\.\-]+@[\w\-]+\.\w{2,}$";

        public RegistrationValidator()
        {
            RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("Le prénom d'utilisateur est requis.")
                .Length(5, 20).WithMessage("Le prénom doit contenir entre 5 et 20 lettres.")
                .Matches("^[a-zA-Z0-9-_.]+$").WithMessage("Certains caractères ne sont pas acceptés...");

            RuleFor(vm => vm.LastName).NotEmpty().WithMessage("Le nom d'utilisateur est requis.")
                .Length(5, 20).WithMessage("Le nom d'utilisateur doit contenir entre 5 et 20 lettres.")
                .Matches("^[a-zA-Z0-9-_.]+$").WithMessage("Certains caractères ne sont pas acceptés...");

            RuleFor(c => c.Email)
                .Matches(REGEX_EMAIL_ADDRESS)
                .When(c => !string.IsNullOrEmpty(c.Email))
                .WithMessage("Veuillez fournir un format d'adresse courriel valide.");
           // RuleFor(vm => vm.Email).NotEmpty().WithMessage("Le courriel est requis.").EmailAddress().WithMessage("Veuillez fournir un format d'adresse courriel valide.")
                //.Matches("^[a-zA-Z0-9-_.@!$%?&*éÉèÈêÊàÀùÙçÇ]+$").WithMessage("Certains caractères ne sont pas acceptés...");

            RuleFor(vm => vm.PhoneNumber).NotEmpty().WithMessage("Le numéro de téléphone est requis.")
                .Matches("^([1]{0,1}[-( ]{0,1}[0-9]{3}[-) ]{0,2}[0-9]{3}[- ]{0,1}[0-9]{4})+$").WithMessage("Certains caractères ne sont pas acceptés...");

            //RuleFor(vm => vm.Password).NotEmpty().WithMessage("Le mot de passe est requis.")
            //    .Matches("^[a-zA-Z0-9-_.@!$%?&*éÉèÈêÊàÀùÙçÇ]+#()=^,;{}:<>$]+$").WithMessage("Certains caractères ne sont pas acceptés...");

            //RuleFor(vm => vm.ConfirmPassword).NotEmpty().WithMessage("Veuillez confirmer votre mot de passe.").Equal(vm => vm.Password).WithMessage("Les mots de passe sont différents...")
            //    .Matches("^[a-zA-Z0-9-_.@!$%?&*éÉèÈêÊàÀùÙçÇ]+#()=^,;{}:<>$]+$").WithMessage("Certains caractères ne sont pas acceptés...");
        }
    }
}
