using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BikingBuddy.Common.GlobalConstants;

namespace BikingBuddy.Web.Areas.Administration.Controllers;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
public class BaseAdminController : Controller
{
}