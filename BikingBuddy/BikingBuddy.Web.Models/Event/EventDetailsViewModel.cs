using System.Diagnostics.Contracts;
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
        }

        public string OrganizerName { get; set; } = null!;
        
        public bool IsDeleted { get; set; }
 
        public double MeetingPointLatitude { get; set; }  
        
        public double MeetingPointLongitude { get; set; }  
        public IList<GalleryPhotoModel> GalleryPhotosModels { get; set; }

        public ICollection<CommentViewModel> EventComments { get; set; }

        public ICollection<UserViewModel> EventsParticipants { get; set; }


        public EventTrack? EventTrack { get; set; } 
    }
}