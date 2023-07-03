using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Team;
using Microsoft.EntityFrameworkCore;

using static BikingBuddy.Common.ErrorMessages.TeamErrorMessages;

namespace BikingBuddy.Services
{
    public class TeamService : ITeamService
    {

        private readonly IEventService eventService;
        private readonly BikingBuddyDbContext dbContext;

        public TeamService(IEventService _eventService, BikingBuddyDbContext _dbContext)
        {
            this.eventService = _eventService;
            this.dbContext = _dbContext;
        }


        public async Task<TeamViewModel> TeamDetails(string teamId)
        {
            var teamDetails = await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(teamId))
                .Select(t => new TeamViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl,
                    EstablishedOn = t.EstablishedOn,
                    TeamMembers = t.TeamMembers
                        .Select(tm => new TeamMemberViewModel()
                        {
                            Id = tm.Id.ToString(),
                            Name = tm.Name,
                            TeamMemberImageUrl = tm.ImageURL

                        }).ToList()

                }).FirstOrDefaultAsync();

            if (teamDetails is null)
                throw new NullReferenceException(TeamDoesNotExist);

            return teamDetails;
        }

        public async Task<ICollection<TeamViewModel>> GetAllTeams()
        {
            var allTeams = await dbContext.Teams
                .Select(t => new TeamViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl,
                    EstablishedOn = t.EstablishedOn,
                    TeamManager = t.TeamManager.Name,
                    TeamMembers = t.TeamMembers
                        .Select(tm => new TeamMemberViewModel()
                        {
                            Id = tm.Id.ToString(),
                            Name = tm.Name,
                            TeamMemberImageUrl = tm.ImageURL,

                        }).ToList()

                }).ToListAsync();



            return allTeams;
        }

        public async Task AddTeam(TeamViewModel model,string teamManagerId)
        {
            var teamToAdd = new Team()
            {
                Name = model.Name,
                TeamImageUrl = model.TeamImageUrl,
                EstablishedOn = model.EstablishedOn,
                TeamManagerId = Guid.Parse(teamManagerId)
            };

            await dbContext.Teams.AddAsync(teamToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditTeam(TeamViewModel model, string teamId)
        {
            var teamToAdd = await GetTeamById(teamId);

            teamToAdd.Name = model.Name;
            teamToAdd.TeamImageUrl = model.TeamImageUrl;
            teamToAdd.EstablishedOn = model.EstablishedOn;


            await dbContext.SaveChangesAsync();
        }

        public Task DeleteTeam(int commentId)
        {  
            //TODO: Soft Delete
            throw new NotImplementedException();
        }


        public async Task AddMember(string userId, string teamId)
        {
            var teamToAdd = await GetTeamById(teamId);

            var userToAdd = await GetUserById(userId);

            if (await IsMember(userId))
            {
                throw new Exception(UserAlreadyAMember);
            }

            teamToAdd.TeamMembers.Add(userToAdd);
            await dbContext.SaveChangesAsync();
        }


        public async Task RemoveMember(string userId, string teamId)
        {
            var teamToAdd = await GetTeamById(teamId);

            var userToAdd = await GetUserById(userId);

            if (!await IsMember(userId))
            {
                throw new Exception(UserIsNotAMember);
            }

            teamToAdd.TeamMembers.Remove(userToAdd);
            await dbContext.SaveChangesAsync();

        }


        private async Task<Team> GetTeamById(string id)
        {
            var teamById = await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            if (teamById is null)
                throw new NullReferenceException(TeamDoesNotExist);

            return teamById;
        }

        private async Task<AppUser> GetUserById(string id)
        {
            var userById = await dbContext.Users
                .Where(u => u.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            if (userById is null)
                throw new NullReferenceException(UserDoesNotExist);

            return userById;
        }

        private  async Task<bool> IsMember(string userId)
        {
           return await dbContext.Teams
                .Where(t => t.TeamMembers.Any(tm => tm.Id == Guid.Parse(userId)))
                .AnyAsync();

        }
    }
}
