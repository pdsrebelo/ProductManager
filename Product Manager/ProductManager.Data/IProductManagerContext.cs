using ProductManager.Data.Repositories;

namespace ProductManager.Data
{
    public interface IProductManagerContext
    {
        IProductRepository ProductRepository { get; }

        IProductCategoryRepository ProductCategoryRepository { get; }

        IProductSubCategoryRepository ProductSubCategoryRepository { get; }
    }
}
