using System.ComponentModel.DataAnnotations;

namespace ProductManager.WebApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public int? ProductSubcategoryId { get; set; }

        [Required]
        public short? StockLevel { get; set; }
    }
}