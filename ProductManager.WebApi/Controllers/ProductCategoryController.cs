using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ProductManager.Model.Entities;
using ProductManager.Service.Interfaces;
using ProductManager.WebApi.Models;

namespace ProductManager.WebApi.Controllers
{
    [Authorization]
    public class ProductCategoryController : ApiController
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET api/ProductCategory
        public async Task<IEnumerable<ProductCategoryModel>> Get()
        {
            IEnumerable<ProductCategory> productCategories = await _productCategoryService.GetAllProductCategoriesAsync();
            IEnumerable<ProductCategoryModel> modelProductCategories 
                = AutoMapper.Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryModel>>(productCategories);

            return modelProductCategories;
        }

        // GET api/ProductCategory/{id}
        public async Task<ProductCategoryModel> Get(int id)
        {
            ProductCategory productCategory = await _productCategoryService.GetProductCategory(id);
            ProductCategoryModel modelProductCategory = 
                AutoMapper.Mapper.Map<ProductCategory, ProductCategoryModel>(productCategory);

            if (modelProductCategory == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return modelProductCategory;
        }
    }
}