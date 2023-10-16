using BikingBuddy.Data.Models;

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
            GalleryPhotosModels = new List<GalleryPhotoModel>();
            EventTracks = new List<EventTrack>();
        }

        public string OrganizerName { get; set; } = null!;
        
        public bool IsDeleted { get; set; }


        public IList<GalleryPhotoModel> GalleryPhotosModels { get; set; }

        public ICollection<CommentViewModel> EventComments { get; set; }

        public ICollection<UserViewModel> EventsParticipants { get; set; }
        
        
        public ICollection<EventTrack> EventTracks { get; set; }
    }
}