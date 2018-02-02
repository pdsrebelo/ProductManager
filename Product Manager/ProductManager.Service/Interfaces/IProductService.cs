using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Model.Entities;

namespace ProductManager.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetAllProductsAsync(int subCategoryId);

        Task<Product> GetProductByIdAsync(int id);

        Task<Reason> CreateProductAsync(Product product);

        Task<Reason> UpdateProductAsync(Product product);

        Task<Reason> DeleteProductAsync(int id);
    }
}