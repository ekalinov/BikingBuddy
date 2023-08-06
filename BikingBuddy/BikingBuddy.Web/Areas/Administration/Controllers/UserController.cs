using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Areas.Administration.Controllers;

public class UserController:  BaseAdminController
{
    
    public async Task<IActionResult> All()
    {
        // var serviceModel = await service.AdminAllEventAsync(queryModel);
        //
        // queryModel.Events = serviceModel.AllEvents;
        // queryModel.TotalEventsCount = serviceModel.TotalEventsCount;
        // queryModel.ActivityTypes = await service.GetActivityTypesAsync();


        return View();
    }
}