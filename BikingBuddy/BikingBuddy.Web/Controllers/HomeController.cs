using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BikingBuddy.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IEventService eventService;

        public HomeController(ILogger<HomeController> _logger, IEventService _eventService)
        {
            this.logger = _logger;
            this.eventService = _eventService;
        }

        public async Task<IActionResult> Index()
        {

	        var topEvents = await eventService.GetTopEventsAsync();

            return View(topEvents);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}