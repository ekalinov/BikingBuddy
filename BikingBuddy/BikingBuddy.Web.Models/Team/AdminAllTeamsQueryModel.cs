using System.ComponentModel.DataAnnotations;
using BikingBuddy.Common.Enums;
using BikingBuddy.Web.Models.Activity;
using BikingBuddy.Web.Models.Event.Enums;
using BikingBuddy.Web.Models.Team;
using static BikingBuddy.Common.GlobalConstants;

namespace BikingBuddy.Web.Models.Event;

public class AdminAllTeamsQueryModel
{
    public AdminAllTeamsQueryModel()
    {
        Teams = new HashSet<TeamDetailsViewModel>();

        CurrentPage = DefaultPage;
        TeamsPerPage = EntitiesPerPage;
    }

    [Display(Name = "Activity Type")] public string ActivityType { get; set; } = null!;

    [Display(Name = "Search...")] public string SearchTerm { get; set; } = null!;
    
    [Display(Name = "Availability Status")]
    public DeleteStatus IsDeleted { get; init; }
    public TeamSorting Sorting { get; init; }

    public int CurrentPage { get; set; }

    public int TeamsPerPage { get; set; }

    public int TotalTeamsCount { get; set; }


    public ICollection<TeamDetailsViewModel> Teams { get; set; }
}