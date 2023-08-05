namespace BikingBuddy.Web.Models.Event
{
    using Comment;
    using User;

    public class EventDetailsViewModel : EventMiniViewModel
    {
        public EventDetailsViewModel()
        {
            EventsParticipants = new HashSet<UserViewModel>();
            EventComments = new HashSet<CommentViewModel>();
        }

        public string OrganizerName { get; set; } = null!;

        public string Country { get; set; } = null!;


        public IList<GalleryPhotoModel> GalleryPhotosModels { get; set; }

        public ICollection<CommentViewModel>? EventComments { get; set; }

        public ICollection<UserViewModel>? EventsParticipants { get; set; }
    }
}