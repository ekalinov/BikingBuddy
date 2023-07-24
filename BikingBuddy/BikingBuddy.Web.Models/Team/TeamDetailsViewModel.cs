namespace BikingBuddy.Web.Models.Team
{

    using User;

    public class TeamDetailsViewModel
    {


        public TeamDetailsViewModel()
        {

            TeamMembers = new HashSet<UserViewModel>();
            MembersRequests = new HashSet<UserViewModel>();
        }


        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? EstablishedOn { get; set; } = null!;

        public string TeamImageUrl { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Town { get; set; } = null!;

        public ICollection<UserViewModel> TeamMembers { get; set; }

        public ICollection<UserViewModel> MembersRequests { get; set; }

        public string TeamManager { get; set; } = null!;


    }
}
