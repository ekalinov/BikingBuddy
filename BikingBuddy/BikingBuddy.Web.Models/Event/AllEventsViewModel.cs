using BikingBuddy.Web.Models.Comment;
 

namespace BikingBuddy.Web.Models.Event
{
    public class AllEventsViewModel : EventMiniViewModel
    {

        public AllEventsViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }

        public string Distance { get; set; } = null!;

        public string OrganizerUsername { get; set; } = null!;
        
        public string Town { get; set; } = null!;

        public ICollection<CommentViewModel> Comments { get; set; } 

    }
}
