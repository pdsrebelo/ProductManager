using System;
using ProductManager.Model.Entities;

namespace ProductManager.Model
{
    public static class ProductValidator
    {
        public static bool IsValid(this Product product)
        {
            return product != null &&
                   !String.IsNullOrEmpty(product.Name) &&
                   !String.IsNullOrEmpty(product.Key) &&
                   product.Price.HasValue &&
                   product.ProductSubcategoryId.HasValue &&
                   product.StockLevel.HasValue;
        }
    }
}
