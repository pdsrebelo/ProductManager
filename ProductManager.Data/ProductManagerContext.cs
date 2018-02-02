using ProductManager.Data.Repositories;

namespace ProductManager.Data
{
    public class ProductManagerContext : IProductManagerContext
    {
        public ProductManagerContext(string connectionString)
        {
            ConnectionString = connectionString;
            ProductRepository = new ProductRepository(this);
            ProductCategoryRepository = new ProductCategoryRepository(this);
            ProductSubCategoryRepository = new ProductSubCategoryRepository(this);
        }

        public string ConnectionString { get; }

        public IProductRepository ProductRepository { get; }

        public IProductCategoryRepository ProductCategoryRepository { get; }

        public IProductSubCategoryRepository ProductSubCategoryRepository { get; }
    }
}
