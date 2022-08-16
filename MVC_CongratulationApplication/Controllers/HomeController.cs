using Microsoft.AspNetCore.Mvc;
using MVC_CongratulationApplication.Service.Interface;

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
                ViewData["Title"] = "Главная";
                ViewBag.Text = "У  ваших друзей скоро день рождения";
                return View("~/Views/Shared/Index.cshtml", response.Data);
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }




    }
}