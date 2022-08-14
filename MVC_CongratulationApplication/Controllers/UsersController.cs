using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CongratulationApplication.Domain.ViewModel;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Controllers
{
    public class UsersController : Controller
    {

        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Settings()
        {
            var response = await _userService.GetUser();
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Settings(UserViewModel uvm)
        {
            var response = await _userService.EditUser(uvm);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "People");
            }
            return View("Error");
        }

        public async Task<IActionResult> Activate(string code)
        {
            var response = await _userService.DeleteActivationCode();
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "People");
            }
            return View("Error");
        }

        public async Task<IActionResult> SendCode()
        {
            var response = await _userService.SendActivationCode();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "People");
            }
            return View("Error");
        }
    }
}
