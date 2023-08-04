namespace BikingBuddy.Web.Models.Team
{
    public class TeamRequestViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? TeamImageUrl { get; set; } = null!;

        public string Country { get; set; } = null!;

        public int MembersCount { get; set; }
    }
}