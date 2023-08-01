using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikingBuddy.Services.Contracts
{
    using Web.Models.Team;
    public interface ITeamService
    {
        Task<TeamDetailsViewModel?> GetTeamDetailsAsync(string teamId);


        Task<EditTeamViewModel?> GetTeamToEditAsync(string teamId);


        Task<ICollection<AllTeamsViewModel>> GetAllTeamsAsync();

        Task AddTeamAsync(AddTeamViewModel model, string teamManagerId);

        Task EditTeamAsync(EditTeamViewModel model, string teamId);

        Task DeleteTeamAsync(string teamId);


        Task<int> GetTeamMembersCountAsync(string teamId);


        Task AddMemberAsync(string userId, string teamId);
        Task RemoveMemberAsync(string userId, string teamId);



        Task<ICollection<TeamRequestViewModel>> GetTeamRequestsByUserAsync(string userId);

        Task SendRequestAsync(string teamId, string userId);

        Task<bool> IsRequestedAsync(string userId, string teamId);

        Task<bool> IsMemberAsync(string userId, string teamId);

        Task RemoveRequestAsync(string userId, string teamId);
        Task<int> GetTeamsCountAsync();
        Task<bool> IsManagerAsync(string teamId, string userId);
        Task UploadPhotoToLocalStorageAsync(EditTeamViewModel model, string envWebRoot);
        Task UploadPhotoToLocalStorageAsync(AddTeamViewModel model, string envWebRoot);

    }
}
