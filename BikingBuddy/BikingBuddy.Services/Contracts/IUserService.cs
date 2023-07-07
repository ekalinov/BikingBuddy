
namespace BikingBuddy.Services.Contracts
{
    using Web.Models.User;
    public interface IUserService
    {

        Task<UserDetailsViewModel?> GetUserDetails(string userId);




    }
}
