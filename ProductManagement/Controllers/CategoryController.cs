using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.Services;
using ProductManagement.Core.Application.ViewModels.Category;

namespace ProductManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBusinessService _businessService;
        private readonly IProductService _productService;
        public CategoryController(ICategoryService categoryService, IBusinessService businessService, IProductService productService)
        {
            _categoryService = categoryService;
            _businessService = businessService;
            _productService = productService;
        }

        public async Task<IActionResult> Details(int businessId, int id)
        {
            var categories = await _categoryService.GetViewModelWithInclude(businessId.ToString(), id, collections: ["Products"], references: ["Business"]);
            
            if(categories != null && categories.ProductsCount > 0)
            {
                var products = await _productService.GetAllViewModel(id.ToString());

                if (products != null) categories.Products = products;
            }

            ViewBag.businessId = businessId;
           
            return View(categories);
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(SaveCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _categoryService.Add(category);

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
        public async Task<ActionResult> Edit(int businessId, int id)
        {
            var data = await _categoryService.GetSaveViewModel(businessId.ToString(), id);
            SaveCategoryViewModel category = new()
            {
                Id = data.Id,
                Name = data.Name,
                Image = data.Image,
                BusinessId = data.BusinessId
            };

            string userId = "1";
            var business = await _businessService.GetAllViewModel(userId);
            ViewBag.Business = business;

            return View("Save", category);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SaveCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _categoryService.Update(category);

                    if (response == "Updated")
                    {
                        return RedirectToAction("Details", new { businessId = category.BusinessId, id = category.Id });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("Save");
        }

        public async Task<ActionResult> Delete(int businessId, int id)
        {
            ViewBag.businessId = businessId;
            return View(await _categoryService.GetViewModel(businessId.ToString(), id));
        }
        [HttpPost]
        public async Task<ActionResult> DeletePost(int businessId, int id)
        {
            string response = await _categoryService.Delete(id);

            if (response == "Deleted")
            {
                return RedirectToAction("Index", "Business", businessId);
            }

            return RedirectToAction("Delete", new { businessId, id });
        }
    }
}
