namespace BikingBuddy.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Common.Enums;
    using BikingBuddy.Data;
    using BikingBuddy.Data.Models;
    using Data.Models.Users;
    using static Common.GlobalConstants;
    using Contracts;
    using Web.Models.User;

    public class UserService : IUserService

    {
        private readonly BikingBuddyDbContext dbContext;

        private readonly IEventService eventService;
        private readonly UserManager<AppUser> userManager;
        private readonly IBikeService bikeService;

        public UserService(BikingBuddyDbContext _dbContext,
            IEventService _eventService,
            IBikeService _bikeService,
            UserManager<AppUser> _userManager)
        {
            dbContext = _dbContext;
            eventService = _eventService;
            bikeService = _bikeService;
            userManager = _userManager;
        }


        public async Task<UserDetailsViewModel?> GetUserDetails(string userId)
        {
            var completedEvents = await eventService.GetCompletedEventsCountByUserAsync(userId);

            var userTotalDistance = await eventService.GetUserTotalDistanceAsync(userId);

            var userTotalAscent = await eventService.GetUserTotalAscentAsync(userId);

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
                    TeamName = u.Team!.Name,
                    TeamId = u.Team.Id.ToString(),
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
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    Helmet = u.Helmet,
                    Shoes = u.Shoes,
                    Town = u.Town.Name,
                    ProfileImageUrl = u.ProfileImageUrl,
                    CountryId = u.CountryId,
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

                if (model.ProfileImageUrl != null)
                {
                    user.ProfileImageUrl = model.ProfileImageUrl;
                }

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

            if (user != null)
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

        public async Task<AdminAllUsersFilteredAndPagedServiceModel> AdminAllUsersAsync(
            AdminAllUsersQueryModel queryModel)
        {
            var adminUsers = await userManager.GetUsersInRoleAsync(AdminRoleName);

            var adminUsersCollection = adminUsers
                .Select(a => new AdminUserDetailsViewModel
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    ProfileImageUrl = a.ProfileImageUrl,
                    Email = a.Email,
                    Username = a.UserName,
                    IsDeleted = a.IsDeleted,
                }).ToList();

            IQueryable<AppUser> usersQuery = dbContext.AppUsers
                .AsQueryable();


            if (!String.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
                usersQuery = usersQuery
                    .Where(u => EF.Functions.Like(u.Name, wildCard) ||
                                EF.Functions.Like(u.Email, wildCard) ||
                                EF.Functions.Like(u.UserName, wildCard) ||
                                EF.Functions.Like(u.Team!.Name, wildCard) ||
                                EF.Functions.Like(u.Country.Name, wildCard) ||
                                EF.Functions.Like(u.Town.Name, wildCard));
            }

            usersQuery = queryModel.IsDeleted switch
            {
                DeleteStatus.Available => usersQuery
                    .Where(t => t.IsDeleted == false),
                DeleteStatus.Deleted => usersQuery
                    .Where(t => t.IsDeleted == true),
                DeleteStatus.All => usersQuery,
                _ => usersQuery
                    .OrderBy(e => e.Name)
            };

            ICollection<AdminUserDetailsViewModel> userCollection = await usersQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.TotalUsersCount)
                .Take(queryModel.UsersPerPage)
                .Select(u => new AdminUserDetailsViewModel
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    ProfileImageUrl = u.ProfileImageUrl,
                    Country = u.Country.Name,
                    Town = u.Town.Name,
                    TeamName = u.Team!.Name,
                    TeamId = u.Team.Id.ToString(),
                    Email = u.Email,
                    Username = u.UserName,
                    IsDeleted = u.IsDeleted
                }).ToListAsync();


            var model = new AdminAllUsersFilteredAndPagedServiceModel()
            {
                AllUser = userCollection,
                Admins = adminUsersCollection,
                TotalUsersCount = usersQuery.Count()
            };
            return model;
        }


        private async Task<bool> IsUserAdmin(string userId)
        {
            var adminUsers = await userManager.GetUsersInRoleAsync("Administration");

            return adminUsers
                .Any(a => a.Id == Guid.Parse(userId));
        }

        public async Task MakeUserAdminAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user != null && !await IsUserAdmin(userId))
            {
                await userManager.AddToRoleAsync(user, AdminRoleName);
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
    }
}