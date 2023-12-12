using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Business;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly string idUser;
        public BusinessController(IBusinessService businessService, ICategoryService categoryService, IProductService productService)
        {
            _businessService = businessService;
            _categoryService = categoryService;
            _productService = productService;
            idUser = "1";
        }

        public async Task<IActionResult> Index()
        {
            return View(await _businessService.GetAllViewModel(idUser));
        }

        public async Task<IActionResult> Details(int id)
        {
            var business = await _businessService.GetViewModelWithInclude(idUser, id, collections: ["Categories"], references: []);
            var categories = await _categoryService.GetAllViewModelWithInclude(id.ToString(), ["Products"]);
            List<ProductViewModel> productList = [];

            if (categories != null && categories.Count > 0)
            {
                foreach(var category in categories)
                {
                    var products = await _productService.GetAllViewModelWithInclude(category.Id.ToString(), ["Category"]);

                    if(products != null)
                    {
                        foreach(ProductViewModel product in products)
                        {
                            productList.Add(product);
                        }
                    }
                }
                
                business.Categories = categories;
                ViewBag.Products = productList;
            }
            
            return View(business);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBusiness(SaveBusinessViewModel business)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _businessService.Add(business);

                    if (response == "Inserted")
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("Save");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var data = await _businessService.GetSaveViewModel(idUser, id);
            SaveBusinessViewModel business = new()
            {
                Id = data.Id,
                Name = data.Name,
                Image = data.Image,
                IdUser = data.IdUser
            };

            return View("Save", business);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SaveBusinessViewModel business)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _businessService.Update(business);

                    if (response == "Updated")
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("Save");
        }
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _businessService.GetViewModel(idUser, id));
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
            string response = await _businessService.Delete(id);

            if (response == "Deleted")
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete");
        }
    }
}
