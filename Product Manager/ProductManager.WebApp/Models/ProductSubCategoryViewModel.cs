namespace ProductManager.WebApp.Models
{
    public class ProductSubCategoryViewModel
    {
        public int Id { get; set; }

        public int? Key { get; set; }

        public string Name { get; set; }

        public int? ProductCategoryId { get; set; }
    }
}