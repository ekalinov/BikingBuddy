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
         Task<bool> IsDeletedAsync(string userId);
         Task DeleteUserAccountAsync(string userId);
         Task AddEditEquipment(EquipmentViewModel model);
    }
}