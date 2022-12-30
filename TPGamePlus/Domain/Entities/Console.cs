using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class Console
    {
        [Key]
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; }
        public bool isMainShopFilter { get; set; }
    }
}
