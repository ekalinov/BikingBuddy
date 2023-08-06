using BikingBuddy.Services.Contracts;
using BikingBuddy.Services.Data.Models.Events;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Mvc;


using static BikingBuddy.Common.ErrorMessages.EventErrorMessages;
using static  BikingBuddy.Common.NotificationMessagesConstants;
using static  BikingBuddy.Common.GlobalConstants;

namespace BikingBuddy.Web.Areas.Administration.Controllers;

public class EventController : BaseAdminController
{
    
    private readonly IEventService service;
    private readonly ICommentService commentService;
    private readonly string envWebRooth;

    public EventController(IEventService _service,
        ICommentService _commentService,
        IWebHostEnvironment _environment)
    {
        service = _service;
        commentService = _commentService;
        envWebRooth = _environment.WebRootPath;
    }
    
    
    
    
    public async Task<IActionResult> All([FromQuery] AdminAllEventsQueryModel queryModel)
    {
        var serviceModel = await service.AdminAllEventAsync(queryModel);

        queryModel.Events = serviceModel.AllEvents;
        queryModel.TotalEventsCount = serviceModel.TotalEventsCount;
        queryModel.ActivityTypes = await service.GetActivityTypesAsync();


        return View(queryModel);
    }
    
    
    //Delete

    [HttpPost]
    public async Task<IActionResult> Delete(string eventId)
    { 
        if (!await service.IsOrganiser(eventId, User.GetId()) && !User.IsAdmin())
        {
            TempData[ErrorMessage] = UnauthorizedForError;
            return Unauthorized();
        }

        if (await service.IsDeleteAsync(eventId))
        {
            TempData[ErrorMessage] = EventAlreadyDeleted;
            return RedirectToAction("All", "Team");
        }
        try
        {
            await service.DeleteEventAsync(eventId);

            TempData[SuccessMessage] = EventDeletedSuccessfully;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = DeleteEventError;
            return RedirectToAction("Details", "Event", new { eventId });
        }

        return RedirectToAction("All", "Event");
    }

}