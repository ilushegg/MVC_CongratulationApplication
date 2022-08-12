using Microsoft.AspNetCore.Mvc;
using MVC_CongratulationApplication.Domain.Entity;
using MVC_CongratulationApplication.Domain.Response;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Service.Interface;
using System;

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

        public async Task<IActionResult> Index()
        {
            var response = await _personService.GetPeople();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel pvm, IFormFile File)
        {
            Console.WriteLine("ENTERING");
            var response = await _personService.CreatePerson(pvm, File, _webHostEnvironment.WebRootPath);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }
      

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _personService.GetPerson(id);
            
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewData["Birthday"] = response.Data.Birthday;
                return View(response.Data);
            }
            return RedirectToAction("Error");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonViewModel pvm, IFormFile file)
        {
            var response = await _personService.EditPerson(id, pvm, file);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _personService.DeletePerson(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


    }
}
