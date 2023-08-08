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

            var myEvents = await eventService.GetMyEventsAsync(userId);
             
            var userEvents = await eventService.GetUserEventsAsync(userId);

            var userBikes = await bikeService.GetUserBikesAsync(userId);

 
            UserDetailsViewModel? user = await dbContext.AppUsers.Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new UserDetailsViewModel
                {
                    Id = userId,
                    Name = u.Name,
                    Helmet = u.Helmet,
                    Shoes = u.Shoes,
                    Team = u.Team!.Name ,
                    TeamId =  u.Team.Id.ToString(),
                    Town = u.Town.Name,
                    Country = u.Country.Name,
                    ProfileImageUrl = u.ProfileImageUrl,
                    CompletedEvents = completedEvents,
                    TotalDistance = userTotalDistance,
                    TotalAscent = userTotalAscent,
                    UserBikes = userBikes,
                    UserEvents = myEvents,
                    
                }).FirstOrDefaultAsync();

             user.UserUpcomingEvents = userEvents
                .Where(e => e is { IsCompleted: false, IsDeleted: false })
                .ToList();
                
            
            user.UserCompletedEvents = userEvents
                .Where(e => e.IsCompleted)
                .ToList();
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
                user.Shoes = model.Shoes;
                user.Helmet = model.Helmet;
                user.Town = await eventService.GetTownByNameAsync(model.Town);
                user.CountryId = model.CountryId;

                if (!string.IsNullOrEmpty(model.ProfileImageUrl))
                { 
                    user.ProfileImageUrl = model.ProfileImageUrl;
                }
                
                await dbContext.SaveChangesAsync();
            }
        }
        
        public async Task DeleteUserAccountAsync(string userId)
        {
            AppUser? user = await GetUserByIdAsync(userId);
            
            if (user != null)
            {
                user.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }
            
        }

        public async Task AddEditEquipment(EquipmentViewModel model)
        { 
            AppUser? user = await GetUserByIdAsync(model.Id);

            if (user !=null)
            {
                if (!string.IsNullOrEmpty(model.Helmet))
                {
                    user.Helmet = model.Helmet;
                }
                if (!string.IsNullOrEmpty(model.Shoes))
                {
                    user.Shoes = model.Shoes;
                }

                await dbContext.SaveChangesAsync();
            }
            
        }

        public async Task<bool> IsDeletedAsync(string userId)
        {
            return await dbContext.AppUsers
                .AnyAsync(u => u.Id == Guid.Parse(userId) && u.IsDeleted == true);

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