using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Save(string businessId)
        {
            if(businessId != null)
            {
                var data = await _categoryService.GetAllViewModel(businessId);
                ViewBag.Categories = data;
            }

            ViewBag.businessId = businessId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(int businessId, SaveProductViewModel product)
        {
            if(ModelState.IsValid) {
                try
                {
                    var response = await _productService.Add(product);

                    if (response == "Inserted")
                    {
                        return RedirectToAction("Details", "Business", new { id = businessId });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
            return RedirectToAction("Save", new { businessId });
        }

        public async Task<ActionResult> Edit(string categoryId, string businessId, int id)
        {
            var data = await _productService.GetSaveViewModel(categoryId, id);
            SaveProductViewModel product = new()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Amount = data.Amount,
                CategoryId = data.CategoryId,
            };

            var category = await _categoryService.GetAllViewModel(businessId);
            ViewBag.Categories = category;
            ViewBag.businessId = businessId;

            return View("Save", product);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int businessId, SaveProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _productService.Update(product);

                    if (response == "Updated")
                    {
                        return RedirectToAction("Details", "Business", new { id = businessId });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return RedirectToAction("Save", new { businessId });
        }

        public async Task<ActionResult> Delete(int businessId, string categoryId, int id)
        {
            ViewBag.businessId = businessId;

            return View(await _productService.GetViewModelWithInclude(categoryId, id, collections: [], references: ["Category"]));
        }

        [HttpPost]
        public async Task<ActionResult> DeletePost(int businessId, string categoryId, int id)
        {
            string response = await _productService.Delete(id);

            if(response == "Deleted")
            {
                return RedirectToAction("Details", "Business", new { id = businessId });
            }

            return RedirectToAction("Delete", new { businessId, categoryId, id });
        }
    }
}
