using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TPGamePlus.Domain.Entities;

namespace TPGamePlus.Domain
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            this.Addresses=new HashSet<Address>();
        }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]

        public string? Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }   
    }
}
