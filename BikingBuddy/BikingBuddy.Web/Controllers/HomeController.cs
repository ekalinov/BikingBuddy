using BikingBuddy.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using BikingBuddy.Services.Data.Models.Home;
using BikingBuddy.Services.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace BikingBuddy.Web.Controllers
{
    using static Common.GlobalConstants;

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
            if (User.IsInRole(AdminRoleName))
            {
                RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }


            var model = new IndexViewModel
            {
                UsersCount = await userService.GetUserSCountAsync(),
                TeamsCount = await teamService.GetTeamsCountAsync(),
                ActiveEventsCount = await eventService.GetActiveEventsCountAsync(),
                AllEventsCount = await eventService.GetAllEventsCountAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            string envWebRooth = String.Empty;
            string destination = String.Empty;
            string url = await UploadPhotosHepler.UploadPhotoToLocalStorageAsync(destination, photo, envWebRooth);


            return Ok();
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