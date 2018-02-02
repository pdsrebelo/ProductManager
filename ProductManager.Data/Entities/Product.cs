using System;

namespace ProductManager.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public int? ProductSubcategoryId { get; set; }

        public string Name { get; set; }

        public short? StockLevel { get; set; }

        public Decimal? Price { get; set; }
    }
}
