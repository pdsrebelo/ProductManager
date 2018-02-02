namespace ProductManager.WebApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public int? ProductSubcategoryId { get; set; }

        public short? StockLevel { get; set; }
    }
}