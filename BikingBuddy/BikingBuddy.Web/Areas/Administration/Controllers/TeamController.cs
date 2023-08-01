using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Areas.Administration.Controllers
{
	public class TeamController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
