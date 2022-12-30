using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vous devez entrer un nom d'utilisateur ou email")]
        [Display(Name = "Utilisateur ou courriel")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vous devez entrer un mot de passe")]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
        [Display(Name = "Se souvenir de moi")]

        public bool RememberMe { get; set; } = false;
    }
}
