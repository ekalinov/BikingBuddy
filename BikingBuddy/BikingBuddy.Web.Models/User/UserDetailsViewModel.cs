using BikingBuddy.Web.Models.Bike;

namespace BikingBuddy.Web.Models.User
{
    using Event;
    using Team;

    public class UserDetailsViewModel : UserViewModel
    {
        public UserDetailsViewModel()
        {
            UserUpcomingEvents = new HashSet<EventViewModel>();
            UserCompletedEvents = new HashSet<EventViewModel>();
            UserEvents = new HashSet<EventViewModel>();
            TeamRequests = new HashSet<AllTeamsViewModel>();
            UserBikes = new HashSet<BikeDetailsViewModel>();
        }
        
       
    
        public string? Country { get; set; } 
        public string? Town { get; set; }

        public string? Helmet { get; set; }  
        
         
        public string? Shoes { get; set; }  


        public string? TeamName { get; set; }
        public string? TeamId { get; set; }
        public int? CompletedEvents { get; set; }

        public double TotalDistance { get; set; }

        public double TotalAscent { get; set; }

        public virtual ICollection<EventViewModel> UserUpcomingEvents { get; set; } = null!;

        public virtual ICollection<EventViewModel> UserCompletedEvents { get; set; } = null!;

        public virtual ICollection<EventViewModel> UserEvents { get; set; } = null!;

        public virtual ICollection<AllTeamsViewModel> TeamRequests { get; set; } = null!;


        public virtual ICollection<BikeDetailsViewModel> UserBikes { get; set; } = null!;
    }
}