using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Services.Contracts
{
    public interface ITeamService
    {
        Task<TeamDetailsViewModel> TeamDetails(string teamId);

        Task< ICollection<AllTeamsViewModel>> GetAllTeams();

        Task AddTeam(AddTeamViewModel model, string teamManagerId);

        Task EditTeam(TeamDetailsViewModel model, string teamId);

        Task DeleteTeam(int commentId);

    }
}
