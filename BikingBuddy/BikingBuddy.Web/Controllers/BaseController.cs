using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}