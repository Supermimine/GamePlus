using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TPGamePlus.Domain;

namespace TPGamePlus.Models.Account
{
    public partial class IndexViewModel:PageModel
    {  
        [Display(Name = "Nom utilisateur")]
        public string Username { get; set; }

    }
}
