namespace BikingBuddy.Web.Models.Event
{
    using Comment;
    using User;

    public class EventDetailsViewModel : AllEventsViewModel
    {
        public EventDetailsViewModel()
        {
            EventsParticipants = new HashSet<UserViewModel>();
        }

        public string Ascent { get; set; } = null!;

        public string OrganizerName { get; set; } = null!;

        public string Country { get; set; } = null!;


        public ICollection<CommentViewModel> EventComments { get; set; } = null!;

        public ICollection<UserViewModel> EventsParticipants { get; set; }
    }
}