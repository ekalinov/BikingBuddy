
namespace BikingBuddy.Web.Models.User
{
    using System.ComponentModel.DataAnnotations;

    using BikingBuddy.Data.Models;
    using Event;
    using Team;

    using static Common.EntityValidationsConstants.User;

    public class UserDetailsViewModel
    {


        public UserDetailsViewModel()
        {
            this.UserEvents = new HashSet<EventViewModel>();
            this.TeamRequests = new HashSet<TeamRequestViewModel>();
        }


        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? BikeId { get; set; } = null!;

        public string? Helmet { get; set; } = null!;

        public string? Shoes { get; set; } = null!;

        public string? TeamId { get; set; } = null!;

        [Url]
        public string? ProfileImageUrl { get; set; }

        public int CompletedEvents { get; set; }
      
        public double TotalDistance { get; set; }

        public double TotalAscent { get; set; }

        public virtual ICollection<EventViewModel> UserEvents { get; set; }

        public virtual ICollection<TeamRequestViewModel> TeamRequests { get; set; }





    }
}
