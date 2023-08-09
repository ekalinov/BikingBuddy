namespace BikingBuddy.Services.Contracts
{
    using BikingBuddy.Data.Models;
    using Data.Models.Users; 
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

        Task<AdminAllUsersFilteredAndPagedServiceModel> AdminAllUsersAsync(AdminAllUsersQueryModel queryModel);
        Task MakeUserAdminAsync(string userId);
    }
}