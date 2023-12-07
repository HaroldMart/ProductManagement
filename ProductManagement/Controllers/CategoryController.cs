using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Category;

namespace ProductManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBusinessService _businessService;
        public CategoryController(ICategoryService categoryService, IBusinessService businessService)
        {
            _categoryService = categoryService;
            _businessService = businessService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllViewModel());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _categoryService.GetViewModelWithInclude(id, collections: ["Products"], references: ["Business"]));
        }
        public async Task<IActionResult> Save()
        {
            var data = await _businessService.GetAllViewModel();
            ViewBag.Business = data;
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
        public async Task<ActionResult> Edit(int id)
        {
            var data = await _categoryService.GetSaveViewModel(id);
            SaveCategoryViewModel category = new()
            {
                Id = data.Id,
                Name = data.Name,
                Image = data.Image,
                BusinessId = data.BusinessId
            };

            var business = await _businessService.GetAllViewModel();
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
            return View(await _categoryService.GetViewModel(id));
        }
        [HttpPost]
        public async Task<ActionResult> DeletePost(int id)
        {
            string response = await _categoryService.Delete(id);

            if (response == "Deleted")
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Delete");
        }
    }
}
