using BikingBuddy.Web.Models.Bike;

namespace BikingBuddy.Web.Models.User
{
    using Event;
    using Team;

    public class AdminUserDetailsViewModel : UserViewModel
    {

        public string Email { get; set; } = null!;
        
        public string Username { get; set; }= null!; 
        
        public bool  IsDeleted  { get; set; } 
        public bool  IsAdmin { get; set; } 

        

    }
}