using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Application.Interfaces.Service;
using ProductManagement.Core.Application.ViewModels.Business;

namespace ProductManagement.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;
        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _businessService.GetAllViewModel());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _businessService.GetViewModelWithInclude(id, collections: ["Categories"], references: []));
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
            var data = await _businessService.GetSaveViewModel(id);
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
            return View(await _businessService.GetViewModel(id));
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
