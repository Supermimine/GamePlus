using System.ComponentModel.DataAnnotations;

namespace TPGamePlus.Domain.Entities
{
    public class GameCategory
    {
        [Key]
        public int CategoryGameID { get; set; }
        public string CategoryGameName { get; set; }
        public bool isMainShopFilter { get; set; }
    }
}
