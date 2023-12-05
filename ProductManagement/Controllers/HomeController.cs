using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBusinessService _businessService;

        public HomeController(IProductService productService, ICategoryService categoryService, IBusinessService businessService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _businessService = businessService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAll();
            var categories = await _categoryService.GetAll();
            var business = await _businessService.GetAll();
            ViewBag.categoriesCount = categories.Count;
            ViewBag.productsCount = products.Count;
            ViewBag.businessCount = business.Count;

            if (products.Count > 10) {

                return View(products.TakeLast(10).ToList());
            }
            return View(products);
        }
    }
}