using System.Collections.Generic;

namespace ProductManager.WebApp.Models
{
    public class ProductSubCategoryDropDownViewModel
    {
        public int ProductSubCategoryId { get; set; }

        public IEnumerable<ProductSubCategoryViewModel> SubCategories { get; set; }
    }
}