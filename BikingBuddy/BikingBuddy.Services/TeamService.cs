using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Team;
using Microsoft.EntityFrameworkCore;

using static BikingBuddy.Common.ErrorMessages.TeamErrorMessages;
using static BikingBuddy.Common.DateTimeFormats;
using BikingBuddy.Common;

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

        //Details
        public async Task<TeamDetailsViewModel> TeamDetails(string teamId)
        {
            var teamDetails = await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(teamId))
                .Select(t => new TeamDetailsViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl,
                    EstablishedOn = t.EstablishedOn.ToString(),
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

        public async Task<ICollection<AllTeamsViewModel>> GetAllTeams()
        {
            var allTeams = await dbContext.Teams
                .Select(t => new AllTeamsViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl,
                    Country=t.Country.Name,
                    TeamMembersCount = t.TeamMembers.Count
                       
                }).ToListAsync();



            return allTeams;
        }


        //Create
        public async Task AddTeam(AddTeamViewModel model,string teamManagerId)
        {
            var teamToAdd = new Team()
            {
                Name = model.Name,
                TeamImageUrl = model.TeamImageUrl,
                EstablishedOn = model.EstablishedOn,
                TeamManagerId = Guid.Parse(teamManagerId),
                CountryId= model.CountryId,
                Town =  await eventService.GetTownByName(model.TownName),
                Description = model.Description  

            };

            await dbContext.Teams.AddAsync(teamToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditTeam(TeamDetailsViewModel model, string teamId)
        {
            var teamToEdit = await GetTeamById(teamId);

            teamToEdit.Name = model.Name;
            teamToEdit.TeamImageUrl = model.TeamImageUrl;
            teamToEdit.EstablishedOn = DateTime.Parse(model.EstablishedOn);
            teamToEdit.CountryId = model.Country;
            teamToEdit.Town = await eventService.GetTownByName(model.Town);
            teamToEdit.Description = model.Description;


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
