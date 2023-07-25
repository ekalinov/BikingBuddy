using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BikingBuddy.Services.Data.Models.Home;
using Microsoft.AspNetCore.Authorization;

namespace BikingBuddy.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IEventService eventService;
        private readonly IUserService userService;
        private readonly ITeamService teamService;

        public HomeController(
            ILogger<HomeController> _logger,
            IEventService _eventService,
            IUserService _userService,
            ITeamService _teamService)
        {
            logger = _logger;
            eventService = _eventService;
            userService = _userService;
            teamService = _teamService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var model = new IndexViewModel
            {
                TopEvents = await eventService.GetNewestEventsAsync(),
                UsersCount = await userService.GetUserSCountAsync(),
                TeamsCount = await teamService.GetTeamsCountAsync(),
                ActiveEventsCount = await eventService.GetActiveEventsCountAsync(),
                AllEventsCount =  await eventService.GetAllEventsCountAsync()
            };
 
            return View(model);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }
            
            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
             
        }
    }
}