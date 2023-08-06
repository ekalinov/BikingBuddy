using BikingBuddy.Web.Models.Bike;

namespace BikingBuddy.Web.Models.User
{
    using Event;
    using Team;

    public class UserDetailsViewModel : EditUserViewModel
    {
        public UserDetailsViewModel()
        {
            UserEvents = new HashSet<EventViewModel>();
            TeamRequests = new HashSet<AllTeamsViewModel>();
            UserBikes = new HashSet<BikeDetailsViewModel>();
        }


        public int? CompletedEvents { get; set; }

        public double TotalDistance { get; set; }

        public double TotalAscent { get; set; }

        public virtual ICollection<EventViewModel> UserEvents { get; set; } = null!;

        public virtual ICollection<AllTeamsViewModel> TeamRequests { get; set; } = null!;


        public virtual ICollection<BikeDetailsViewModel> UserBikes { get; set; } = null!;
    }
}