namespace BikingBuddy.Services
{
    using Microsoft.EntityFrameworkCore;
    using BikingBuddy.Data;
    using BikingBuddy.Data.Models;
    using Data;
    using Data.Models;
    using Contracts;
    using Web.Models.Team;
    using Web.Models.User;

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
        public async Task<TeamDetailsViewModel?> GetTeamDetailsAsync(string teamId)
        {
            return await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(teamId))
                .Select(t => new TeamDetailsViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl!,
                    EstablishedOn = t.EstablishedOn.ToString(),
                    Description = t.Description,
                    Country = t.Country.Name,
                    Town = t.Town.Name,
                    TeamManager = t.TeamManager.UserName,
                    MembersRequests = t.TeamRequests
                        .Where(tr => tr.TeamId == Guid.Parse(teamId) && tr.IsAccepted == false)
                        .Select(tr => new UserViewModel
                        {
                            Id = tr.RequestFrom.Id.ToString(),
                            Name = tr.RequestFrom.Name,
                            ProfileImageUrl = tr.RequestFrom.ProfileImageUrl
                        }).ToList(),
                    TeamMembers = t.TeamMembers
                        .Select(tm => new UserViewModel()
                        {
                            Id = tm.Id.ToString(),
                            Name = tm.Name,
                            ProfileImageUrl = tm.ProfileImageUrl
                        }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<EditTeamViewModel?> GetTeamToEditAsync(string teamId)
        {
            return await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(teamId))
                .Select(t => new EditTeamViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl,
                    EstablishedOn = t.EstablishedOn,
                    Description = t.Description,
                    TownName = t.Town.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<ICollection<AllTeamsViewModel>> GetAllTeams()
        {
            return await dbContext.Teams
                .Select(t => new AllTeamsViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl!,
                    Country = t.Country.Name,
                    TeamMembersCount = t.TeamMembers.Count
                }).ToListAsync();
        }

        //Create
        public async Task AddTeam(AddTeamViewModel model, string teamManagerId)
        {
            var teamToAdd = new Team()
            {
                Name = model.Name,
                TeamImageUrl = model.TeamImageUrl,
                EstablishedOn = model.EstablishedOn,
                TeamManagerId = Guid.Parse(teamManagerId),
                CountryId = model.CountryId,
                Town = await eventService.GetTownByNameAsync(model.TownName),
                Description = model.Description
            };

            await dbContext.Teams.AddAsync(teamToAdd);
            await dbContext.SaveChangesAsync();
        }

        //Update
        public async Task EditTeam(EditTeamViewModel model, string teamId)
        {
            var teamToEdit = await GetTeamByIdAsync(teamId);

            if (teamToEdit != null)
            {
                teamToEdit.Name = model.Name;
                teamToEdit.TeamImageUrl = model.TeamImageUrl;
                teamToEdit.EstablishedOn = model.EstablishedOn;
                teamToEdit.CountryId = model.CountryId;
                teamToEdit.Town = await eventService.GetTownByNameAsync(model.TownName);
                teamToEdit.Description = model.Description;

                await dbContext.SaveChangesAsync();
            }
        }

        //Delete
        public Task DeleteTeam(int commentId)
        {
            //TODO: Soft Delete
            throw new NotImplementedException();
        }


        public async Task<int> GetTeamMembersCount(string teamId)
        {
            return await dbContext.AppUsers
                .CountAsync(u => u.TeamId == Guid.Parse(teamId));
        }

        public async Task SendRequest(string teamId, string userId)
        {
            Team? team = await GetTeamByIdAsync(teamId);

            AppUser? user = await GetUserByIdAsync(userId);

            TeamRequest? request = await GetTeamRequestAsync(userId, teamId);


            //Check if the user was accepted once 
            if (request is { IsAccepted: true })
            {
                request.IsAccepted = false;
                await dbContext.SaveChangesAsync();
            }


            if (request == null
                && team != null
                && user != null)
            {
                request = new TeamRequest
                {
                    Team = team,
                    RequestFrom = user,
                    IsAccepted = false
                };

                team.TeamRequests.Add(request);

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddMemberAsync(string userId, string teamId)
        {
            Team? team = await GetTeamByIdAsync(teamId);

            AppUser? user = await GetUserByIdAsync(userId);

            TeamRequest? request = await GetTeamRequestAsync(userId, teamId);


            if (request != null
                && team != null
                && user != null
                && !await IsMemberAsync(userId, teamId))
            {
                team.TeamMembers.Add(user);

                request.IsAccepted = true;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveMemberAsync(string userId, string teamId)
        {
            var team = await GetTeamByIdAsync(teamId);

            var userToRemove = await GetUserByIdAsync(userId);

            if (team != null
                && userToRemove != null
                && await IsMemberAsync(userId, teamId)
               )
            {
                await RemoveRequest(userId, teamId);
                team.TeamMembers.Remove(userToRemove);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<TeamRequestViewModel>> GetTeamRequestsByUserAsync(string userId)
        {
            var teamRequests = await dbContext.TeamsRequests
                .Where(tr => tr.RequestFromId == Guid.Parse(userId))
                .Select(tr => new TeamRequestViewModel
                {
                    Id = tr.TeamId.ToString(),
                    Name = tr.Team.Name,
                    Country = tr.Team.Country.Name,
                    TeamImageUrl = tr.Team.TeamImageUrl
                }).ToListAsync();


            foreach (var team in teamRequests)
            {
                team.MembersCount = await GetTeamMembersCount(team.Id);
            }


            return teamRequests;
        }

        public async Task<bool> IsRequested(string userId, string teamId)
        {
            return await dbContext.TeamsRequests
                .AnyAsync(tr => tr.TeamId == Guid.Parse(teamId)
                                && tr.RequestFromId == Guid.Parse(userId)
                                && tr.IsAccepted == false);
        }

        public async Task<bool> IsMemberAsync(string userId, string teamId)
        {
            return await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(teamId)
                            && t.TeamMembers.Any(tm => tm.Id == Guid.Parse(userId)))
                .AnyAsync();
        }

        public async Task RemoveRequest(string userId, string teamId)
        {
            var request = await GetTeamRequestAsync(userId, teamId);

            if (request != null)
            {
                dbContext.TeamsRequests.Remove(request);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetTeamsCountAsync()
        {
            return await this.dbContext.Events
                .Where(e => e.IsDeleted == false)
                .CountAsync();
        }


        private async Task<TeamRequest?> GetTeamRequestAsync(string userId, string teamId)
        {
            return await dbContext.TeamsRequests
                .Where(tr => tr.TeamId == Guid.Parse(teamId)
                             && tr.RequestFromId == Guid.Parse(userId)
                             && tr.IsAccepted == false)
                .FirstOrDefaultAsync();
        }

        private async Task<Team?> GetTeamByIdAsync(string id)
        {
            return await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();
        }

        private async Task<AppUser?> GetUserByIdAsync(string id)
        {
            return await dbContext.Users
                .Where(u => u.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();
        }
    }
}