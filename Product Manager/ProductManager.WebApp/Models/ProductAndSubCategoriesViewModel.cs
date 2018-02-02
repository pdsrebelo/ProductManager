using System.ComponentModel.DataAnnotations;

namespace ProductManager.WebApp.Models
{
    public class ProductAndSubCategoriesViewModel
    {
        [Required]
        public ProductViewModel ProductViewModel { get; set; }

        public ProductSubCategoryDropDownViewModel ProductSubCategoryDropDownViewModel { get; set; }
    }
}