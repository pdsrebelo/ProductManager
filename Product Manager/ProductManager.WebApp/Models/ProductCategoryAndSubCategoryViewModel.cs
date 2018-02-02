using System.Collections.Generic;

namespace ProductManager.WebApp.Models
{
    public class ProductCategoryAndSubCategoryViewModel
    {
        public IEnumerable<ProductViewModel> ProductsModel { get; set; }

        public ProductCategoryDropDownViewModel ProductCategoryDropDownViewModel { get; set; }

        public ProductSubCategoryDropDownViewModel ProductSubCategoryDropDownViewModel { get; set; }
    }
}