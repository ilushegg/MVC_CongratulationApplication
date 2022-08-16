using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_CongratulationApplication.Models;
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


        public async Task<IActionResult> Index(int page = 1)
        {
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
            return View(viewModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewData["Title"] = "Друзья";
                return View("~/Views/Shared/Index.cshtml", viewModel);
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }




    }
}