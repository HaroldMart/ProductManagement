using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels;

namespace ProductManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }
        public IActionResult Details(int id)
        {
            return View(_service.Get(id));
        }
        public async Task<IActionResult> Save()
        {
            var data = await _categoryService.GetAll();
            ViewBag.Categories = data;
            return View();
        }

        [HttpPost]
        public IActionResult SaveProduct(SaveProductViewModel product)
        {
            if(ModelState.IsValid) {
                try
                {
                    var response = _service.Save(product);

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
        public async Task<ActionResult> Edit(int id)
        {
            var data = _service.Get(id);
            SaveProductViewModel product = new()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Amount = data.Amount,
                CategoryId = data.Category.Id,
            };

            var category = await _categoryService.GetAll();
            ViewBag.Categories = category;

            return View("Save", product);
        }

        [HttpPost]
        public ActionResult Edit(SaveProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = _service.Update(product);

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
