using BikingBuddy.Common;
using BikingBuddy.Data;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BikingBuddy.Services
{
using static BikingBuddy.Common.GlobalConstants;
    public class UserService : IUserService
    {
        private readonly BikingBuddyDbContext dbContext;

        public UserService(BikingBuddyDbContext _dbContext)
        {
            this.dbContext = _dbContext;
            
        }





        public async Task<UserDetailsViewModel> GetUserDetails(string userId)
        {

            var completedEvents = await GetUserCompletedEventsCountAsync(userId);

            var userTotalDistance = await GetUserTotalDistanceAsync(userId);

            var userTotalAscent = await GetUserTotalAscentAsync(userId);

            var userEvents = await GetUserEventsAsync(userId);




            UserDetailsViewModel user = await  dbContext.AppUsers.
                Where(u=>u.Id == Guid.Parse(userId))
                .Select( u=> new UserDetailsViewModel
                {
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
                    TeamRequests = null
                }).FirstOrDefaultAsync();




            return   user;
        }


        //Get All events where user is participating (completed and not completed)
        private async Task<ICollection<EventViewModel>> GetUserEventsAsync(string userId)
        {

            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId))
                .Select(ep => new EventViewModel
                {
                    Id = ep.EventId.ToString(),
                    Title = ep.Event.Title,
                    Date = ep.Event.Date.ToString(DateTimeFormats.DateFormat),
                    Distance = ep.Event.Distance,
                    EventImageUrl = ep.Event.EventImageUrl,
                    ParticipantsCount = ep.Event.EventsParticipants.Count(),
                    IsCompleted = ep.IsCompleted
                }).ToListAsync();
        }

        private  async Task<int> GetUserCompletedEventsCountAsync(string userId)
        {
            return  await dbContext.EventsParticipants
                .CountAsync(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true);
        }

        private async Task<double> GetUserTotalDistanceAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true)
                .SumAsync(ep=>ep.Event.Distance);
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
