using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Product;
using ProductManagement.Core.Domain.Entities;

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
        public async Task<IActionResult> Index()
        {
           /* if (products.Count > 10)
            {

                return View(products.TakeLast(10).ToList());
            }
           */
            return View(await _productService.GetAllViewModel());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _productService.GetViewModelWithInclude(id, collections: [], references: ["Category"]));
        }
        public async Task<IActionResult> Save()
        {
            var data = await _categoryService.GetAllViewModel();
            ViewBag.Categories = data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(SaveProductViewModel product)
        {
            if(ModelState.IsValid) {
                try
                {
                    var response = await _productService.Add(product);

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
            var data = await _productService.GetSaveViewModel(id);
            SaveProductViewModel product = new()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Amount = data.Amount,
                CategoryId = data.CategoryId,
            };

            var category = await _categoryService.GetAllViewModel();
            ViewBag.Categories = category;

            return View("Save", product);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SaveProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _productService.Update(product);

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
            return View(await _productService.GetViewModel(id));
        }
        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
            string response = await _productService.Delete(id);

            if(response == "Deleted")
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete");
        }
    }
}
