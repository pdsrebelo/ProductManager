using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.Data.Repositories;
using ProductManager.Model;
using ProductManager.Model.Entities;
using ProductManager.Service.Interfaces;
using DataAcess = ProductManager.Data.Entities;
using Product = ProductManager.Model.Entities.Product;

namespace ProductManager.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var returnedList = await _productRepository.GetProducts();

            return AutoMapper.Mapper.Map<IEnumerable<Product>>(returnedList);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int subCategoryId)
        {
            var returnedList = await _productRepository.GetProductsByProductSubCategoryId(subCategoryId);

            return AutoMapper.Mapper.Map<IEnumerable<Product>>(returnedList);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var returnedProduct = await _productRepository.GetProductById(id);

            return AutoMapper.Mapper.Map<Product>(returnedProduct);
        }

        public async Task<Reason> CreateProductAsync(Product product)
        {
            if (product.IsValid())
            {
                DataAcess.Product dataProduct = AutoMapper.Mapper.Map<DataAcess.Product>(product);

                if (await _productRepository.IsValidProductKey(dataProduct.Key))
                {
                    try
                    {
                        await _productRepository.CreateProduct(dataProduct);

                        return Reason.Ok;
                    }
                    catch (Exception)
                    {
                        return Reason.Unknown;
                    }
                }

                return Reason.InvalidKey;
            }

            return Reason.InvalidProduct;
        }

        public async Task<Reason> UpdateProductAsync(Product product)
        {
            if (product.IsValid())
            {
                DataAcess.Product dataProduct = AutoMapper.Mapper.Map<DataAcess.Product>(product);

                if (!await _productRepository.IsValidProductKey(dataProduct.Key))
                {
                    try
                    {
                        await _productRepository.UpdateProduct(dataProduct);

                        return Reason.Ok;
                    }
                    catch (Exception)
                    {
                        return Reason.Unknown;
                    }
                }

                return Reason.InvalidKey;
            }

            return Reason.InvalidProduct;
        }

        public async Task<Reason> DeleteProductAsync(int productId)
        {
            try
            {
                await _productRepository.DeleteProduct(productId);

                return Reason.Ok;
            }
            catch (Exception)
            {
                return Reason.Unknown;
            }
        }
    }
}