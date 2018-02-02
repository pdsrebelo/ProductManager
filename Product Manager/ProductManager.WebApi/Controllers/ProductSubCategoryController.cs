using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ProductManager.Model.Entities;
using ProductManager.Service.Interfaces;
using ProductManager.WebApi.Models;

namespace ProductManager.WebApi.Controllers
{
    [Authorization]
    public class ProductSubCategoryController : ApiController
    {
        private readonly IProductSubCategoryService _productSubCategoryService;

        public ProductSubCategoryController(IProductSubCategoryService productSubCategoryService)
        {
            _productSubCategoryService = productSubCategoryService;
        }

        // GET api/ProductSubCategory
        public async Task<IEnumerable<ProductSubCategoryModel>> Get()
        {
            IEnumerable<ProductSubCategory> productSubCategories = await _productSubCategoryService.GetAllProductSubCategoriesAsync();
            IEnumerable<ProductSubCategoryModel> modelProducts = AutoMapper.Mapper.Map<IEnumerable<ProductSubCategory>, IEnumerable<ProductSubCategoryModel>>(productSubCategories);

            return modelProducts;
        }

        // GET api/ProductSubCategory?categoryId={categoryId}
        public async Task<IEnumerable<ProductSubCategoryModel>> GetByCategoryId(int categoryId)
        {
            IEnumerable<ProductSubCategory> productSubCategories = await _productSubCategoryService.GetAllProductSubCategoriesAsync(categoryId);
            IEnumerable<ProductSubCategoryModel> modelProductSubCategory 
                = AutoMapper.Mapper.Map<IEnumerable<ProductSubCategory>, IEnumerable<ProductSubCategoryModel>>(productSubCategories);

            if (modelProductSubCategory == null || !modelProductSubCategory.Any())
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return modelProductSubCategory;
        }

        // GET api/ProductSubCategory?subCategoryId={subCategoryId}
        public async Task<ProductSubCategoryModel> GetBySubCategoryId(int subCategoryId)
        {
            ProductSubCategory productSubCategory = await _productSubCategoryService.GetProductSubCategoryAsync(subCategoryId);
            ProductSubCategoryModel modelProductSubCategory = AutoMapper.Mapper.Map<ProductSubCategory, ProductSubCategoryModel>(productSubCategory);

            if (modelProductSubCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return modelProductSubCategory;
        }
    }
}