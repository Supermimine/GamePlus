using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Account
{
    public class ConfirmationViewModel
    {
        // Informations de l'adresse 

        [Display(Name = "Rue")]
        public string StreetAddress { get; set; }

        [Display(Name = "Ville")]
        public string City { get; set; }

        [Display(Name = "État ou Province")]
        public string State { get; set; }

        [Display(Name = "Code Postal")]
        public string PostalCode { get; set; }

        [Display(Name = "Pays")]
        public string Country { get; set; }

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
    }
}
