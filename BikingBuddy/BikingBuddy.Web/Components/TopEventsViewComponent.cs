using BikingBuddy.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Components;

public class TopEventsViewComponent : ViewComponent
{
    
    private readonly IEventService eventService;
    
    public TopEventsViewComponent(IEventService _eventService)
    {

        eventService = _eventService;
    }
    
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var topEvents = await eventService.GetNewestEventsAsync();


        return View(topEvents);
    }
    
}