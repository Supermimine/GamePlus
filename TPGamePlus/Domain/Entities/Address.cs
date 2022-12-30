namespace TPGamePlus.Domain.Entities
{
    public class Address
    {
        public Address()
        {
            this.Users=new HashSet<ApplicationUser>();
        }

        public int AddressID { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAddress { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
