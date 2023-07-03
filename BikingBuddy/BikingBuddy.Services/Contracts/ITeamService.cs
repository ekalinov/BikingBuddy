using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Services.Contracts
{
    public interface ITeamService
    {
        Task<TeamViewModel> TeamDetails(string teamId);

        Task< ICollection<TeamViewModel>> GetAllTeams();

        Task AddTeam(TeamViewModel model, string teamManagerId);

        Task EditTeam(TeamViewModel model, string teamId);

        Task DeleteTeam(int commentId);

    }
}
