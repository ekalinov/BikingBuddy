using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Services.Data.Models.Events;

public class AdminAllTeamsFilteredAndPagedServiceModel
{
    public AdminAllTeamsFilteredAndPagedServiceModel()
    {
        AllTeams = new HashSet<TeamDetailsViewModel>();
    }

    public int TotalTeamsCount { get; set; }

    public ICollection<TeamDetailsViewModel> AllTeams { get; set; }
}