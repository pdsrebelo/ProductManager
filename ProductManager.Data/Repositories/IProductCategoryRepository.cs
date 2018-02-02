using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Data.Entities;

namespace ProductManager.Data.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> GetProductCategoryById(int productId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<ProductCategory>> GetProductCategories(CancellationToken cancellationToken = default(CancellationToken));
    }
}