using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Data.Entities;

namespace ProductManager.Data.Repositories
{
    public interface IProductSubCategoryRepository
    {
        Task<ProductSubcategory> GetProductSubcategoryById(int productSubcategoryId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<ProductSubcategory>> GetProductSubcategoriesByProductCategoryId(int productCategoryId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<ProductSubcategory>> GetProductSubcategories(CancellationToken cancellationToken = default(CancellationToken));
    }
}