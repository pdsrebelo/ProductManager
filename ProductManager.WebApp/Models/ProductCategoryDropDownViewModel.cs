using System.Collections.Generic;

namespace ProductManager.WebApp.Models
{
    public class ProductCategoryDropDownViewModel
    {
        public int ProductCategoryId { get; set; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}