using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikingBuddy.Common;
using BikingBuddy.Web.Models.Comment;
using Microsoft.VisualBasic;
using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Web.Models.Event
{
    public class EventDetailsViewModel
    {
        public EventDetailsViewModel()
        {
            this.EventsParticipants = new HashSet<EventParticipantViewModel>();
        }

        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public DateTime Date { get; set; } 

        public string Description { get; set; } = null!;

        public string? EventImageUrl { get; set; } = null!;

        public string ActivityType { get; set; } = null!;

        public string Distance { get; set; } = null!;

        public string Ascent { get; set; } = null!;

        public string OrganizerName { get; set; } = null!;

        public string OrganizerUsername { get; set; } = null!;
        
        public string Country { get; set; } = null!;

        public string? Municipality { get; set; }

        public string Town { get; set; } = null!;


        public ICollection<CommentViewModel> EventComments { get; set; } = null!;

        public ICollection<EventParticipantViewModel> EventsParticipants { get; set; }

    }
}
