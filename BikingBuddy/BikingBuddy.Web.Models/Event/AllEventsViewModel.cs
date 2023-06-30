using BikingBuddy.Web.Models.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Event
{
    public class AllEventsViewModel
    {

        public AllEventsViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }

        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Distance { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string OrganizerId { get; set; } = null!;

        public string OrganizerUsername { get; set; } = null!;
            

        public string EventImageUrl { get; set; } = null!;

        public string ActivityType { get; set; } = null!;

        public string Town { get; set; } = null!;

        public ICollection<CommentViewModel> Comments { get; set; } 

    }
}
