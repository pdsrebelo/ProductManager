using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Model.Entities;

namespace ProductManager.Service.Interfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();

        Task<ProductCategory> GetProductCategory(int productId);
    }
}