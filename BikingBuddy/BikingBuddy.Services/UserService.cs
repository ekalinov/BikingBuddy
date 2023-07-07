using BikingBuddy.Common;
using BikingBuddy.Data;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Team;
using BikingBuddy.Web.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Immutable;

namespace BikingBuddy.Services
{
    using static BikingBuddy.Common.GlobalConstants;
    public class UserService : IUserService
    {
        private readonly BikingBuddyDbContext dbContext;

        private readonly IEventService eventService;

        private readonly ITeamService teamService;

        public UserService(BikingBuddyDbContext _dbContext, IEventService _eventService, ITeamService _teamService)
        {
            this.dbContext = _dbContext;
            this.eventService = _eventService;
            this.teamService = _teamService;
        }





        public async Task<UserDetailsViewModel?> GetUserDetails(string userId)
        {

            var completedEvents = await eventService.GetCompletedEventsCountByUserAsync(userId);

            var userTotalDistance = await GetUserTotalDistanceAsync(userId);

            var userTotalAscent = await GetUserTotalAscentAsync(userId);

            var userEvents = await eventService.GetUserEventsAsync(userId);

            var userTeamRequests = await teamService.GetTeamRequestsByUserAsync(userId);


            UserDetailsViewModel? user = await dbContext.AppUsers.
                Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new UserDetailsViewModel
                {
                    Id= userId,
                    Name = u.Name,
                    BikeId = u.BikeId,
                    Helmet = u.Helmet,
                    Shoes = u.Shoes,
                    TeamId = u.TeamId.ToString(),
                    ProfileImageUrl = u.ProfileImageUrl,
                    CompletedEvents = completedEvents,
                    TotalDistance = userTotalDistance,
                    TotalAscent = userTotalAscent,
                    UserEvents = userEvents,
                    TeamRequests = userTeamRequests,
                }).FirstOrDefaultAsync();


            return user;
        }






        private async Task<double> GetUserTotalDistanceAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true)
                .SumAsync(ep => ep.Event.Distance);
        }

        private async Task<double> GetUserTotalAscentAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true)
                .SumAsync(ep => ep.Event.Ascent);
        }

        //  private async Task<ICollection<EventPa>>

    }
}
