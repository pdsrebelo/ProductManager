using ProductManager.Data.Entities;

namespace ProductManager.Tests
{
    public static class TestsHelper
    {
        public static Product CreateDbProduct(int id)
        {
            return new Product
            {
                Id = id,
                StockLevel = (short)id,
                Price = id * 10,
                ProductSubcategoryId = id,
                Name = "testname" + id,
                Key = "testkey" + id
            };
        }

        public static ProductManager.Model.Entities.Product CreateProduct(int id)
        {
            return new ProductManager.Model.Entities.Product
            {
                Id = id,
                StockLevel = (short)id,
                Price = id * 10,
                ProductSubcategoryId = id,
                Name = "testname" + id,
                Key = "testkey" + id
            };
        }

        public static bool ProductComparer(Product product1, ProductManager.Model.Entities.Product product2)
        {
            return
                product1.Id == product2.Id &&
                product1.Key == product2.Key &&
                product1.Name == product2.Name &&
                product1.Price == product2.Price &&
                product1.ProductSubcategoryId == product2.ProductSubcategoryId &&
                product1.StockLevel == product2.StockLevel;
        }
    }
}
