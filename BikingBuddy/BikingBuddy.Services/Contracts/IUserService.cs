
namespace BikingBuddy.Services.Contracts
{
    using BikingBuddy.Data.Models;
    using Web.Models.User;
    public interface IUserService
    {

        Task<UserDetailsViewModel?> GetUserDetails(string userId);

        Task UpdateProfileInfo(UserDetailsViewModel userDetails);

        Task CompleteProfile(UserDetailsViewModel userDetails);


        Task<AppUser?> GetUserByIdAsync(string userId);

    }
}
