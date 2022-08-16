using Microsoft.AspNetCore.Mvc;
using MVC_CongratulationApplication.Models;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService _personService;

        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewData["Title"] = "Главная";
            ViewBag.Text = "У ваших друзей скоро день рождения";
            var response = await _personService.GetBirthdayPeople();
            int elementCount = 5;
            var count = response.Data.Count();
            var items = response.Data.Skip((page - 1) * elementCount).Take(elementCount);

            PageViewModel pageViewModel = new PageViewModel(count, page, elementCount);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                People = items
            };
            if (response.StatusCode == Domain.Enum.StatusCode.OK || response.StatusCode == Domain.Enum.StatusCode.PeopleNotFound)
            {
                return View("~/Views/Shared/Index.cshtml", viewModel);
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }

        public IActionResult Edit(int id)
        {
            return RedirectToAction("Edit", "People", new { id });
        }

        public IActionResult Delete(int id)
        {
            return RedirectToAction("Delete", "People", new { id });
        }
    }
}