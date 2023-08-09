using BikingBuddy.Web.Models.Bike;

namespace BikingBuddy.Web.Models.User
{
    using Event;
    using Team;

    public class UserDetailsViewModel : UserViewModel
    {
       
        public virtual ICollection<EventViewModel> UserUpcomingEvents { get; set; } = null!;

        public virtual ICollection<EventViewModel> UserCompletedEvents { get; set; } = null!;

        public virtual ICollection<EventViewModel> UserEvents { get; set; } = null!;

        public virtual ICollection<AllTeamsViewModel> TeamRequests { get; set; } = null!;


        public virtual ICollection<BikeDetailsViewModel> UserBikes { get; set; } = null!;
    }
}