namespace BikingBuddy.Web.Models.Team
{
    public class EditTeamViewModel : AddTeamViewModel

    {
        public string Id { get; set; } = null!;
        public string TeamManagerId { get; set; } = null!;
    }
}