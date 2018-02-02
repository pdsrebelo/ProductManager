using System.Collections.Generic;
using System.Web.Mvc;
using ProductManager.WebApp.Models;

namespace ProductManager.WebApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        #region Partial Views
        [HttpPost]
        public ActionResult LoadCategories(IEnumerable<ProductCategoryViewModel> productCategories)
        {
            return PartialView("ProductCategoryDropDown", new ProductCategoryDropDownViewModel
            {
                Categories = productCategories
            });
        }

        [HttpPost]
        public ActionResult LoadSubCategories(IEnumerable<ProductSubCategoryViewModel> productSubCategories, int productSubCategoryId)
        {
            return PartialView("ProductSubCategoryDropDown", new ProductSubCategoryDropDownViewModel
            {
                ProductSubCategoryId = productSubCategoryId,
                SubCategories = productSubCategories
            });
        }

        [HttpPost]
        public ActionResult LoadProductList(IEnumerable<ProductViewModel> products)
        {
            return PartialView("ProductList", products);
        }

        [HttpPost]
        public ActionResult LoadProduct(ProductViewModel product)
        {
            return PartialView("Product", product);
        }
        #endregion
    }
}