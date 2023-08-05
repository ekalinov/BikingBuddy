using BikingBuddy.Web.Models.Event;

namespace BikingBuddy.Services.Data.Models.Events;

public class AdminAllEventsFilteredAndPagedServiceModel
{
    public AdminAllEventsFilteredAndPagedServiceModel()
    {
        AllEvents = new HashSet<EventDetailsViewModel>();
    }

    public int TotalEventsCount { get; set; }

    public ICollection<EventDetailsViewModel> AllEvents { get; set; }
}