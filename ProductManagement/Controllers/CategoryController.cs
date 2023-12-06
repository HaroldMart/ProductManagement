using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
