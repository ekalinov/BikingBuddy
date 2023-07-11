using BikingBuddy.Common;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Bike;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Team;
using BikingBuddy.Web.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace BikingBuddy.Services
{
    using static BikingBuddy.Common.GlobalConstants;
    public class UserService : IUserService
    {
        private readonly BikingBuddyDbContext dbContext;

        private readonly IEventService eventService;

        private readonly ITeamService teamService;

        private readonly IBikeService bikeService;

        public UserService(BikingBuddyDbContext _dbContext, 
            IEventService _eventService, 
            ITeamService _teamService, 
            IBikeService _bikeService)
        {
            this.dbContext = _dbContext;
            this.eventService = _eventService;
            this.teamService = _teamService;
            this.bikeService = _bikeService;
        }





        public async Task<UserDetailsViewModel?> GetUserDetails(string userId)
        {

            var completedEvents = await eventService.GetCompletedEventsCountByUserAsync(userId);

            var userTotalDistance = await GetUserTotalDistanceAsync(userId);

            var userTotalAscent = await GetUserTotalAscentAsync(userId);

            var userEvents = await eventService.GetUserEventsAsync(userId);

            var userTeamRequests = await teamService.GetTeamRequestsByUserAsync(userId);

            var userBikes = await bikeService.GetUserBikesAsync(userId);



            UserDetailsViewModel? user = await dbContext.AppUsers.
                Where(u => u.Id == Guid.Parse(userId))
                .Select(u => new UserDetailsViewModel
                {
                    Id= userId,
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
                    UserEvents = userEvents,
                    TeamRequests = userTeamRequests,
                }).FirstOrDefaultAsync();


            return user;
        }

        public async Task<EditUserViewModel?> GetUserForEditAsync(string userId)
        {
           return await dbContext.AppUsers.
                Where(u => u.Id == Guid.Parse(userId))
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

            if (user!=null)
            {
                user.ProfileImageUrl = model.ProfileImageUrl;
                user.Shoes = model.Shoes;
                user.Helmet= model.Helmet;
                user.Town = await eventService.GetTownByName(model.Town);
                user.CountryId = model.CountryId;

                await dbContext.SaveChangesAsync();
            }


        }

        public Task CompleteProfile(UserDetailsViewModel userDetails)
        {
            throw new NotImplementedException();
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
