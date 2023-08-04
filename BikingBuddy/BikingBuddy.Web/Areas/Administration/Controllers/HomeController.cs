using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Areas.Administration.Controllers;

public class HomeController : BaseAdminController
{
    public IActionResult Index()
    {
        return View();
    }
}