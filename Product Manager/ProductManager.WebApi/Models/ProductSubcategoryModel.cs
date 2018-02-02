namespace ProductManager.WebApi.Models
{
    public class ProductSubCategoryModel
    {
        public int Id { get; set; }

        public int? Key { get; set; }

        public string Name { get; set; }

        public int? ProductCategoryId { get; set; }
    }
}