
namespace BikingBuddy.Web.Models.User;

using System.ComponentModel.DataAnnotations;
using BikingBuddy.Common.Enums;
using Event.Enums;
using static BikingBuddy.Common.GlobalConstants;
public class AdminAllUsersQueryModel
{
    public AdminAllUsersQueryModel()
    {
        
        Admins = new HashSet<AdminUserDetailsViewModel>();  
        Users = new HashSet<AdminUserDetailsViewModel>();  
        CurrentPage = DefaultPage;
        UsersPerPage = EntitiesPerPage;
    }
 

    public UserSorting Sorting { get; init; }
    
    [Display(Name = "Search...")] 
    public string SearchTerm { get; set; } = null!;
    public DeleteStatus IsDeleted { get; init; }
 
    public bool IsAdmin { get; init; }
    public int CurrentPage { get; set; }

    public int UsersPerPage { get; set; }

    public int TotalUsersCount { get; set; }
  
    public ICollection<AdminUserDetailsViewModel> Users { get; set; }
    
    
    public ICollection<AdminUserDetailsViewModel> Admins { get; set; }
}