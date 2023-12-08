using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Business;

namespace ProductManagement.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;
        private readonly ICategoryService _categoryService;
        private readonly string idUser;
        public BusinessController(IBusinessService businessService, ICategoryService categoryService)
        {
            _businessService = businessService;
            _categoryService = categoryService;
            idUser = "1";
        }
        public async Task<IActionResult> Index()
        {
            return View(await _businessService.GetAllViewModel(idUser));
        }
        public async Task<IActionResult> Details(int id)
        {
            var business = await _businessService.GetViewModelWithInclude(idUser, id, collections: ["Categories"], references: []);
            var categories = await _categoryService.GetAllViewModel(id.ToString());
            if (categories != null)
            {
                business.Categories = categories;
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
