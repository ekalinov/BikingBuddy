using System;
using System.Linq;
using System.Threading.Tasks;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Services
{
    using Microsoft.EntityFrameworkCore;
    using Contracts;
    using Web.Models.User;

    public class UserService : IUserService
    {
        private readonly BikingBuddyDbContext dbContext;

        private readonly IEventService eventService;


        private readonly IBikeService bikeService;

        public UserService(BikingBuddyDbContext _dbContext,
            IEventService _eventService,
            IBikeService _bikeService)
        {
            dbContext = _dbContext;
            eventService = _eventService;
            bikeService = _bikeService;
        }

        public async Task<UserDetailsViewModel?> GetUserDetails(string userId)
        {
            var completedEvents = await eventService.GetCompletedEventsCountByUserAsync(userId);

            var userTotalDistance = await GetUserTotalDistanceAsync(userId);

            var userTotalAscent = await GetUserTotalAscentAsync(userId);

            var userEvents = await eventService.GetUserEventsAsync(userId);

            var userBikes = await bikeService.GetUserBikesAsync(userId);


            UserDetailsViewModel? user = await dbContext.AppUsers.Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new UserDetailsViewModel
                {
                    Id = userId,
                    Name = u.Name,
                    Helmet = u.Helmet,
                    Shoes = u.Shoes,
                    Team = u.Team!.Name,
                    Town = u.Town.Name,
                    Country = u.Country.Name,
                    ProfileImageUrl = u.ProfileImageUrl,
                    CompletedEvents = completedEvents,
                    TotalDistance = userTotalDistance,
                    TotalAscent = userTotalAscent,
                    UserBikes = userBikes,
                    UserEvents = userEvents
                }).FirstOrDefaultAsync();


            return user;
        }

        public async Task<EditUserViewModel?> GetUserForEditAsync(string userId)
        {
            return await dbContext.AppUsers.Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new EditUserViewModel
                {
                    Id = userId,
                    Helmet = u.Helmet,
                    Shoes = u.Shoes,
                    Town = u.Town.Name,
                    ProfileImageUrl = u.ProfileImageUrl,
                }).FirstOrDefaultAsync();
        }

        public async Task UpdateProfileInfo(EditUserViewModel model)
        {
            AppUser? user = await GetUserByIdAsync(model.Id);

            if (user != null)
            {
                user.ProfileImageUrl = model.ProfileImageUrl;
                user.Shoes = model.Shoes;
                user.Helmet = model.Helmet;
                user.Town = await eventService.GetTownByNameAsync(model.Town);
                user.CountryId = model.CountryId;

                await dbContext.SaveChangesAsync();
            }
        }

         public async Task<int> GetUserSCountAsync()
         {
             return await dbContext.AppUsers
                 .Where(u => u.IsDeleted == false)
                 .CountAsync();
         }

        public async Task<AppUser?> GetUserByIdAsync(string userId)
        {
            return await dbContext.AppUsers
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
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
    }
}