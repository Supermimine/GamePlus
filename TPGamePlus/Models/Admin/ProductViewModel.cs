using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Admin
{
    //public enum Status { Stock, Preorder, Nostock }
    //public enum Category { Console, Game, Controller, Accessories }
    //public enum Company { Microsoft, Nintendo, Sony, Others }


    public class ProductViewModel
    {
        [Display(Name = "ID")]
        public int ProductID { get; set; }

        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Prix")]
        public float Price { get; set; }

        [Display(Name = "Prix actuel")]
        public float ActualPrice { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Disponibilité")]
        public int Available { get; set; }

        [Display(Name = "Quantité")]
        public int Quantity { get; set; }

        //[Display(Name = "Notation")]
        //public CategoryViewModel Stars { get; set; }

        [Display(Name = "Images")]
        [ValidateNever]
        public string Images { get; set; }

        [Display(Name = "Plateforme")]
        public int Plateforme { get; set; }

        [Display(Name = "Éditeur")]
        public int? Publisher { get; set; }

        [Display(Name = "Compagnie")]
        public int? Compagny { get; set; }


        [Display(Name = "Catégorie")]
        public int Category { get; set; }


    }

    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator()
        {
            RuleFor(vm => vm.Name)
                 .NotEmpty().WithMessage("Le nom du produit est requis.")
                 .Length(3, 50).WithMessage("Le nom du produit doit contenir entre 3 et 50 lettres.");
                 //.Matches("^[a-zA-Z0-9-éÉèÈêÊàÀùÙçÇ]+$").WithMessage("Certains caractères ne sont pas acceptés...");

            RuleFor(vm => vm.Price).NotEmpty().WithMessage("Le prix est requis.")
                .GreaterThan(0).WithMessage("Le prix doit être plus grand que zéro.");

            RuleFor(vm => vm.ActualPrice).NotEmpty().WithMessage("Le prix actuelle est requis.")
                .GreaterThan(0).WithMessage("Le prix actuelle doit être plus grand que zéro.");

            RuleFor(vm => vm.Description).NotEmpty().WithMessage("La description est requis.");

            RuleFor(vm => vm.Quantity).NotEmpty().WithMessage("La quantité est requis.")
                .GreaterThan(0).WithMessage("La quantité doit être grand que zéro.");

            RuleFor(vm => vm.Description)
                 .NotEmpty().WithMessage("La description du produit est requis.");

        }
    }
}