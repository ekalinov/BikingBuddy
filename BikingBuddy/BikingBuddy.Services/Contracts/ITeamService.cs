using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Services.Contracts
{
    public interface ITeamService
    {
        Task<TeamDetailsViewModel> GetTeamDetailsAsync(string teamId);


        Task<EditTeamViewModel> GetTeamToEditAsync(string teamId);


        Task< ICollection<AllTeamsViewModel>> GetAllTeams();

        Task AddTeam(AddTeamViewModel model, string teamManagerId);

        Task EditTeam(EditTeamViewModel model, string teamId);

        Task DeleteTeam(int commentId);

        Task SendRequest(string teamId, string userId);
    }
}
