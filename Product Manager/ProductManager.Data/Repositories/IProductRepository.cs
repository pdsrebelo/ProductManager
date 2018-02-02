using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Data.Entities;

namespace ProductManager.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<Product>> GetProducts(CancellationToken cancellationToken = default(CancellationToken));

        Task<List<Product>> GetProductsByProductSubCategoryId(int productSubCategoryId, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> CreateProduct(Product product, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> UpdateProduct(Product product, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> DeleteProduct(int productId, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("Use DeleteProduct instead")]
        int DeleteProductSync(int productId);

        Task<bool> IsValidProductKey(string productKey, CancellationToken cancellationToken = default(CancellationToken));
    }
}