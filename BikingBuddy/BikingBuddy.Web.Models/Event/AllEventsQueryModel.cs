using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using BikingBuddy.Web.Models.Activity;
using BikingBuddy.Web.Models.Event.Enums;

using static   BikingBuddy.Common.GlobalConstants;

namespace BikingBuddy.Web.Models.Event;

public class AllEventsQueryModel
{
    public AllEventsQueryModel()
    {
        Events = new HashSet<AllEventsViewModel>();
        ActivityTypes = new HashSet<ActivityTypeViewModel>();

        CurrentPage = DefaultPage;
        EventsPerPage = EntitiesPerPage ;
    }
      
    [Display(Name = "Activity Type")]
    public string ActivityType { get; set; } = null!;
    
    [Display(Name = "Search...")]
    public string SearchTerm { get; set; } = null!;

    public EventSorting Sorting { get; init; } 
    
    public int CurrentPage { get; set; } 
    
    public int EventsPerPage { get; set; } 

    public int TotalEventsCount { get; set; }

    public ICollection<ActivityTypeViewModel> ActivityTypes { get; set; } 
    
    public ICollection<AllEventsViewModel> Events { get; set; }

}