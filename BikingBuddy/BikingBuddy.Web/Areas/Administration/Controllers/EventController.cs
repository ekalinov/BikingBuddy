using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Areas.Administration.Controllers;

public class EventController : Controller
{
    // GET
    public IActionResult Index()
    {
            return View();
    }
}