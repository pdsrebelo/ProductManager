namespace ProductManager.Data.Entities
{
    public class ProductSubcategory
    {
        public int Id { get; set; }

        public int? Key { get; set; }

        public int? ProductCategoryId { get; set; }

        public string Name { get; set; }
    }
}
