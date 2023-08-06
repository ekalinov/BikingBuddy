using BikingBuddy.Web.Models.Comment;

namespace BikingBuddy.Web.Models.Team
{
    using User;

    public class TeamDetailsViewModel : AllTeamsViewModel
    {
        public TeamDetailsViewModel()
        {
            TeamMembers = new HashSet<UserViewModel>();
            MembersRequests = new HashSet<UserViewModel>();
            GalleryPhotosModels = new List<GalleryPhotoModel>();
        }
 

        public string Description { get; set; } = null!;

        public string? EstablishedOn { get; set; }   
        
        public string Town { get; set; } = null!;

        public ICollection<UserViewModel> TeamMembers { get; set; }
        
        public ICollection<UserViewModel> MembersRequests { get; set; }

        public IList<GalleryPhotoModel> GalleryPhotosModels { get; set; }

        public string TeamManager { get; set; } = null!;
    }
}