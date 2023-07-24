  using BikingBuddy.Web.Models.Event;

namespace BikingBuddy.Services.Data.Models.Events;

public class AllEventsFilteredAndPagedServiceModel
{
    public AllEventsFilteredAndPagedServiceModel()
    {
        AllEvents = new HashSet<AllEventsViewModel>();
    }

    public int TotalEventsCount { get; set; }

    public ICollection<AllEventsViewModel> AllEvents { get; set; }
}