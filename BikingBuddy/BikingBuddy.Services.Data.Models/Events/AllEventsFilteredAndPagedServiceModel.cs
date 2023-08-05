using BikingBuddy.Web.Models.Event;

namespace BikingBuddy.Services.Data.Models.Events;

public class AllEventsFilteredAndPagedServiceModel
{
    public AllEventsFilteredAndPagedServiceModel()
    {
        AllEvents = new HashSet<EventMiniViewModel>();
    }

    public int TotalEventsCount { get; set; }

    public ICollection<EventMiniViewModel> AllEvents { get; set; }
}