using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BikingBuddy.Web.Controllers
{
    public class UserController : BaseController
    {
        public readonly IEventService eventService;

        public readonly IUserService userService;

        public UserController(IEventService _eventService, IUserService _userService)
        {
            this.eventService = _eventService;
            this.userService = _userService;
        }


        public async Task<IActionResult> Details(string userId)
        {

            return View();


        }


        public async Task<IActionResult> MyProfile()
        {

            var userDetails = await userService.GetUserDetails(this.User.GetId());


            return View(userDetails);


        }
    }
}
