using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Components;

public class TopEventsViewComponent : ViewComponent
{
    private readonly IEventService eventService;

    public TopEventsViewComponent(IEventService _eventService)
    {
        eventService = _eventService;
    }


    public async Task<IViewComponentResult> InvokeAsync(string? eventId)
    {
        IList<EventMiniViewModel> model;
        if (eventId != null)
        {
            model = await eventService.GetNewestEventsAsync(eventId);
        }
        else
        {
            model = await eventService.GetNewestEventsAsync();
        }


        return View(model);
    }
}