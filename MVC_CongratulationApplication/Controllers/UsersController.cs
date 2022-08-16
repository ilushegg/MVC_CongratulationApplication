using Microsoft.AspNetCore.Mvc;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Settings()
        {
            var response = await _userService.GetUser();
            if (response.StatusCode == Domain.Enum.StatusCode.OK || response.StatusCode != Domain.Enum.StatusCode.InternalServerError)
            {
                return View(response.Data);
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(UserViewModel uvm)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.EditUser(uvm);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Index", "People");
                }
                return View("~/Views/Shared/Error.cshtml", response.Description);
            }
            return View("~/Views/Shared/Error.cshtml", "Неккоректные данные");
        }

        public async Task<IActionResult> Activate(string code)
        {
            var response = await _userService.DeleteActivationCode();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View();
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }

        public async Task<IActionResult> SendCode()
        {
            var response = await _userService.SendActivationCode();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "People");
            }
            return View("~/Views/Shared/Error.cshtml", response.Description);
        }
    }
}
