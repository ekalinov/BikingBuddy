using System.Threading.Tasks;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Services.Contracts
{
    using Web.Models.User;
    public interface IUserService
    {

        Task<UserDetailsViewModel?> GetUserDetails(string userId);

        Task<EditUserViewModel?> GetUserForEditAsync(string userId);

        Task UpdateProfileInfo(EditUserViewModel userDetails);

        Task<AppUser?> GetUserByIdAsync(string userId);


         Task<int> GetUserSCountAsync();
    }
}