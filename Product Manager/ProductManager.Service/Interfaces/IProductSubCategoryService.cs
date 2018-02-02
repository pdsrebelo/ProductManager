using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Model.Entities;

namespace ProductManager.Service.Interfaces
{
    public interface IProductSubCategoryService
    {
        Task<IEnumerable<ProductSubCategory>> GetAllProductSubCategoriesAsync();

        Task<IEnumerable<ProductSubCategory>> GetAllProductSubCategoriesAsync(int categoryId);

        Task<ProductSubCategory> GetProductSubCategoryAsync(int subCategoryId);
    }
}