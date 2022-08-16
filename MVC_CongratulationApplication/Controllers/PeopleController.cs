using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Models;
using MVC_CongratulationApplication.Service.Interface;


namespace MVC_CongratulationApplication.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PeopleController(IPersonService personService, IWebHostEnvironment webHostEnvironment)
        {
            _personService = personService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
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
            return View(viewModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewData["Title"] = "Друзья";
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
        public async Task<IActionResult> Create(PersonViewModel pvm, IFormFile File)
        {
            var response = await _personService.CreatePerson(pvm, File);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
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
        public async Task<IActionResult> Edit(int id, PersonViewModel pvm, IFormFile File)
        {
            var response = await _personService.EditPerson(id, pvm, File);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);

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
