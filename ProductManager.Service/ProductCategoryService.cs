using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Data.Repositories;
using ProductManager.Model.Entities;
using ProductManager.Service.Interfaces;

namespace ProductManager.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
        {
            var returnedList = await _productCategoryRepository.GetProductCategories();

            return AutoMapper.Mapper.Map<IEnumerable<ProductCategory>>(returnedList);
        }

        public async Task<ProductCategory> GetProductCategory(int productId)
        {
            var category = await _productCategoryRepository.GetProductCategoryById(productId);

            return AutoMapper.Mapper.Map<ProductCategory>(category);
        }
    }
}