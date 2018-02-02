using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProductManager.Model.Entities;
using ProductManager.Service.Interfaces;
using ProductManager.WebApi.Models;

namespace ProductManager.WebApi.Controllers
{
    [Authorization]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/Product
        public async Task<IEnumerable<ProductModel>> Get()
        {
            IEnumerable<Product> products = await _productService.GetAllProductsAsync();
            IEnumerable<ProductModel> modelProducts = AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

            return modelProducts;
        }

        // GET api/Product?productId={productId}
        public async Task<ProductModel> GetByProductId(int productId)
        {
            Product product = await _productService.GetProductByIdAsync(productId);
            ProductModel modelProduct = AutoMapper.Mapper.Map<Product, ProductModel>(product);

            if (modelProduct == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return modelProduct;
        }

        // GET api/Product?subCategoryId={subCategoryId}
        public async Task<IEnumerable<ProductModel>> GetBySubCategoryId(int subCategoryId)
        {
            IEnumerable<Product> products = await _productService.GetAllProductsAsync(subCategoryId);
            IEnumerable<ProductModel> modelProducts =
                AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

            if (modelProducts == null || !modelProducts.Any())
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return modelProducts;
        }

        // POST api/Product
        public async Task<HttpResponseMessage> Post([FromBody]ProductModel value)
        {
            Product modelProduct = AutoMapper.Mapper.Map<ProductModel, Product>(value);

            Reason reason = await _productService.CreateProductAsync(modelProduct);

            if (reason == Reason.InvalidProduct || reason == Reason.Unknown)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (reason == Reason.InvalidKey)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        // PUT api/Product/5
        public async Task<HttpResponseMessage> Put([FromBody]ProductModel value)
        {
            Product modelProduct = AutoMapper.Mapper.Map<ProductModel, Product>(value);

            Reason reason = await _productService.UpdateProductAsync(modelProduct);

            if (reason == Reason.InvalidProduct || reason == Reason.Unknown)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if(reason == Reason.InvalidKey)
                throw new HttpResponseException(HttpStatusCode.Conflict);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        // DELETE api/Product/5
        public async Task<HttpResponseMessage> Delete(int id)
        {
            if (await _productService.DeleteProductAsync(id) == Reason.Unknown)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}