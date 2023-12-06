using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService service, ICategoryService categoryService)
        {
            _productService = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllViewModel());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _productService.GetViewModel(id));
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
            var data = await _productService.GetViewModel(id);
            SaveProductViewModel product = new()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Amount = data.Amount,
                CategoryId 
            };

            var category = await _categoryService.GetAll();
            ViewBag.Categories = category;

            return View("Save", product);
        }

        [HttpPost]
        public ActionResult Edit(SaveCategoryViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = _productService.Update(product);

                    if (response == true)
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
            return View();
        }
    }
}
