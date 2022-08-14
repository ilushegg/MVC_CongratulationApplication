using Microsoft.AspNetCore.Mvc;
using MVC_CongratulationApplication.Models;
using MVC_CongratulationApplication.Service.Implementation;
using MVC_CongratulationApplication.Service.Interface;
using System.Diagnostics;

namespace MVC_CongratulationApplication.Controllers
{
    public class HomeController : Controller
    {
        IPersonService _personService;

        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }


        public async Task<IActionResult> Index()
        {
            var response = await _personService.GetBirthdayPeople();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }


    }
}