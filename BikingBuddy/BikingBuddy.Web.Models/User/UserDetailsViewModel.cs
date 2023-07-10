﻿using BikingBuddy.Web.Models.Bike;

namespace BikingBuddy.Web.Models.User
{
    using System.ComponentModel.DataAnnotations;
    
    using Event;
    using Team;

    public class UserDetailsViewModel
    {


        public UserDetailsViewModel()
        {
            this.UserEvents = new HashSet<EventViewModel>();
            this.TeamRequests = new HashSet<TeamRequestViewModel>();
            this.UserBikes = new HashSet<BikeDetailsViewModel>();
        }


        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;

        public string Town { get; set; } = null!;
        
        public string? Helmet { get; set; } = null!;

        public string? Shoes { get; set; } = null!;

        public string? TeamId { get; set; } = null!;


        [Url]
        public string? ProfileImageUrl { get; set; }

        public int CompletedEvents { get; set; }
      
        public double TotalDistance { get; set; }

        public double TotalAscent { get; set; }

        public virtual ICollection<EventViewModel> UserEvents { get; set; } = null!;

        public virtual ICollection<TeamRequestViewModel> TeamRequests { get; set; } = null!;


        public virtual ICollection<BikeDetailsViewModel> UserBikes { get; set; } = null!;



    }
}
