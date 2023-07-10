using BikingBuddy.Data.Models;
using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Services.Contracts
{
    public interface ITeamService
    {
        Task<TeamDetailsViewModel?> GetTeamDetailsAsync(string teamId);


        Task<EditTeamViewModel?> GetTeamToEditAsync(string teamId);


        Task<ICollection<AllTeamsViewModel>> GetAllTeams();

        Task AddTeam(AddTeamViewModel model, string teamManagerId);

        Task EditTeam(EditTeamViewModel model, string teamId);

        Task DeleteTeam(int commentId);


        Task<int> GetTeamMembersCount(string teamId);


        Task AddMemberAsync(string userId, string teamId);
        Task RemoveMemberAsync(string userId, string teamId);



        Task<ICollection<TeamRequestViewModel>> GetTeamRequestsByUserAsync(string userId);

        Task SendRequest(string teamId, string userId);

        Task<bool> IsRequested(string userId, string teamId);

        Task<bool> IsMemberAsync(string userId, string teamId);

        Task RemoveRequest(string userId, string teamId);
    }
}
