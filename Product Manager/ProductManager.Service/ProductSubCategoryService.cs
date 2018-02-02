using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Data.Repositories;
using ProductManager.Model.Entities;
using ProductManager.Service.Interfaces;

namespace ProductManager.Service
{
    public class ProductSubCategoryService : IProductSubCategoryService
    {
        private readonly IProductSubCategoryRepository _productSubCategoryRepository;

        public ProductSubCategoryService(IProductSubCategoryRepository productSubCategoryRepository)
        {
            _productSubCategoryRepository = productSubCategoryRepository;
        }

        public async Task<IEnumerable<ProductSubCategory>> GetAllProductSubCategoriesAsync()
        {
            var returnedList = await _productSubCategoryRepository.GetProductSubcategories();

            return AutoMapper.Mapper.Map<IEnumerable<ProductSubCategory>>(returnedList);
        }

        public async Task<IEnumerable<ProductSubCategory>> GetAllProductSubCategoriesAsync(int categoryId)
        {
            var returnedList = await _productSubCategoryRepository.GetProductSubcategoriesByProductCategoryId(categoryId);

            return AutoMapper.Mapper.Map<IEnumerable<ProductSubCategory>>(returnedList);
        }

        public async Task<ProductSubCategory> GetProductSubCategoryAsync(int subCategoryId)
        {
            var returnedList = await _productSubCategoryRepository.GetProductSubcategoryById(subCategoryId);

            return AutoMapper.Mapper.Map<ProductSubCategory>(returnedList);
        }
    }
}