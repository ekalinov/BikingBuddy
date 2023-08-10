namespace BikingBuddy.Services.Data.Models.Users;

using BikingBuddy.Web.Models.User;

public class AdminAllUsersFilteredAndPagedServiceModel
{
    public AdminAllUsersFilteredAndPagedServiceModel()
    {
        AllUser = new HashSet<AdminUserDetailsViewModel>();
        Admins = new HashSet<AdminUserDetailsViewModel>();
    }


    public int TotalUsersCount { get; set; }

    public ICollection<AdminUserDetailsViewModel> AllUser { get; set; }
    public ICollection<AdminUserDetailsViewModel> Admins { get; set; }
}