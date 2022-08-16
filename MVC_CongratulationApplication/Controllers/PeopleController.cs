using Microsoft.AspNetCore.Mvc;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Models;
using MVC_CongratulationApplication.Service.Interface;


namespace MVC_CongratulationApplication.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewData["Title"] = "Друзья";
            var response = await _personService.GetPeople();
            int elementCount = 5;
            var count = response.Data.Count();
            var items = response.Data.Skip((page - 1) * elementCount).Take(elementCount);

            PageViewModel pageViewModel = new PageViewModel(count, page, elementCount);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                People = items
            };
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("~/Views/Shared/Index.cshtml", viewModel);
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                var response = await _personService.CreatePerson(pvm);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                return View("~/Views/Shared/Error.cshtml", response.Description);
            }
            return View("~/Views/Shared/Error.cshtml", "Неккоректные данные");
        }

        public async Task<IActionResult> Edit(int id)
        {

            var response = await _personService.GetPerson(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                var response = await _personService.EditPerson(id, pvm);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
                return View("~/Views/Shared/Error.cshtml", response.Description);
            }
            return View("~/Views/Shared/Error.cshtml", "Неккоректные данные");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _personService.DeletePerson(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }


    }
}
