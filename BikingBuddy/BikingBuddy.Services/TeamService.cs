﻿using BikingBuddy.Common;
using BikingBuddy.Common.Enums;
using BikingBuddy.Services.Data.Models.Events;
using BikingBuddy.Web.Models;
using BikingBuddy.Web.Models.Comment;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Event.Enums;

namespace BikingBuddy.Services
{
    using Microsoft.EntityFrameworkCore;
    using BikingBuddy.Data;
    using BikingBuddy.Data.Models;
    using Contracts;
    using Web.Models.Team;
    using Web.Models.User;

    public class TeamService : ITeamService
    {
        private readonly IEventService eventService;
        private readonly BikingBuddyDbContext dbContext;
        private readonly IUserService userService;

        public TeamService(IEventService _eventService,
            BikingBuddyDbContext _dbContext,
            IUserService _userService)
        {
            eventService = _eventService;
            dbContext = _dbContext;
            userService = _userService;
        }


        //Details
        public async Task<TeamDetailsViewModel?> GetTeamDetailsAsync(string teamId)
        {
            return await dbContext.Teams
                .Include(t => t.TeamManager)
                .Where(t => t.Id == Guid.Parse(teamId) && t.IsDeleted == false)
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
                        }).ToList(),
                    GalleryPhotosModels = t.GalleryPhotos
                        .Select(p => new GalleryPhotoModel
                        {
                            Id = p.Id.ToString(),
                            Name = p.Name,
                            URL = p.Url
                        }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<EditTeamViewModel?> GetTeamToEditAsync(string teamId)
        {
            return await dbContext.Teams
                .Where(t => t.Id == Guid.Parse(teamId) && t.IsDeleted == false)
                .Select(t => new EditTeamViewModel()
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl,
                    EstablishedOn = t.EstablishedOn,
                    Description = t.Description,
                    TownName = t.Town.Name,
                    TeamManagerId = t.TeamManagerId.ToString(),
                }).FirstOrDefaultAsync();
        }

        public async Task<ICollection<AllTeamsViewModel>> GetAllTeamsAsync()
        {
            return await dbContext.Teams
                .Where(t => t.IsDeleted == false)
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
        public async Task AddTeamAsync(AddTeamViewModel model, string teamManagerId)
        {
            var teamToAdd = new Team
            {
                Name = model.Name,
                TeamImageUrl = model.TeamImageUrl,
                EstablishedOn = model.EstablishedOn,
                TeamManagerId = Guid.Parse(teamManagerId),
                CountryId = model.CountryId,
                Town = await eventService.GetTownByNameAsync(model.TownName),
                Description = model.Description,
            };

            if (model.GalleryPhotosModels != null && model.GalleryPhotosModels.Any())
            {
                foreach (var photo in model.GalleryPhotosModels)
                {
                    teamToAdd.GalleryPhotos.Add(new TeamGalleryPhoto
                    {
                        Name = photo.Name,
                        Url = photo.URL,
                    });
                }
            }

            await dbContext.Teams.AddAsync(teamToAdd);
            await dbContext.SaveChangesAsync();
        }

        //Update
        public async Task EditTeamAsync(EditTeamViewModel model, string teamId)
        {
            var teamToEdit = await GetTeamByIdAsync(teamId);

            if (teamToEdit != null)
            {
                teamToEdit.Name = model.Name;
                teamToEdit.TeamImageUrl = model.TeamImageUrl;
                teamToEdit.CountryId = model.CountryId;
                teamToEdit.Town = await eventService.GetTownByNameAsync(model.TownName);
                teamToEdit.Description = model.Description;

                if (model.TeamImageUrl != null)
                {
                    teamToEdit.TeamImageUrl = model.TeamImageUrl;
                }

                if (model.EstablishedOn != model.EstablishedOn)
                {
                    teamToEdit.EstablishedOn = model.EstablishedOn;
                }
                
                if (model.GalleryPhotosModels != null && model.GalleryPhotosModels.Any())
                {
                    ICollection<TeamGalleryPhoto> galleryPhotos = new HashSet<TeamGalleryPhoto>();

                    foreach (var photo in model.GalleryPhotosModels)
                    {
                        galleryPhotos.Add(new TeamGalleryPhoto
                        {
                            Team = teamToEdit,
                            Name = photo.Name,
                            Url = photo.URL
                        });
                    }

                    await dbContext.TeamsGalleryPhotos.AddRangeAsync(galleryPhotos);
                }

                await dbContext.SaveChangesAsync();
            }
        }

        //Delete
        public async Task DeleteTeamAsync(string teamId)
        {
            Team? teamToDelete = await GetTeamByIdAsync(teamId);

            if (teamToDelete != null)
            {
                teamToDelete.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }
        }


        public async Task<int> GetTeamMembersCountAsync(string teamId)
        {
            return await dbContext.AppUsers
                .CountAsync(u => u.TeamId == Guid.Parse(teamId));
        }

        public async Task SendRequestAsync(string teamId, string userId)
        {
            Team? team = await GetTeamByIdAsync(teamId);

            AppUser? user = await userService.GetUserByIdAsync(userId);

            TeamRequest? request = await GetTeamRequestAsync(userId, teamId);

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

            AppUser? user = await userService.GetUserByIdAsync(userId);

            TeamRequest? request = await GetTeamRequestAsync(userId, teamId);


            if (request != null
                && team != null
                && user != null
                && !await IsMemberAsync(userId, teamId))
            {
                team.TeamMembers.Add(user);

                dbContext.TeamsRequests.Remove(request);

                request.IsAccepted = true;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveMemberAsync(string userId, string teamId)
        {
            var team = await GetTeamByIdAsync(teamId);

            var userToRemove = await userService.GetUserByIdAsync(userId);

            if (team != null
                && userToRemove != null
                && await IsMemberAsync(userId, teamId)
               )
            {
                await RemoveRequestAsync(userId, teamId);
                team.TeamMembers.Remove(userToRemove);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<AdminAllTeamsFilteredAndPagedServiceModel> AdminAllTeamsAsync(AdminAllTeamsQueryModel queryModel)
        {
            IQueryable<Team> teamssQuery = dbContext.Teams
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
                teamssQuery = teamssQuery
                    .Where(e => EF.Functions.Like(e.TeamManager.Name, wildCard) ||
                                EF.Functions.Like(e.Description, wildCard) ||
                                EF.Functions.Like(e.Name, wildCard) ||
                                EF.Functions.Like(e.Country.Name, wildCard) ||
                                EF.Functions.Like(e.Town.Name, wildCard));
            }
            
            teamssQuery = queryModel.IsDeleted switch
            {
                DeleteStatus.Available => teamssQuery
                    .Where(t=> t.IsDeleted==false) ,
                DeleteStatus.Deleted => teamssQuery
                    .Where(t=> t.IsDeleted==true) ,  
                DeleteStatus.All => teamssQuery ,
                _ => teamssQuery
                    .OrderBy(e => e.Name)
            };

            teamssQuery = queryModel.Sorting switch
            {
                TeamSorting.Newest => teamssQuery
                    .OrderByDescending(e => e.EstablishedOn),
                TeamSorting.MostMembers => teamssQuery
                    .OrderByDescending(e => e.TeamMembers.Count),
                TeamSorting.LeastMembers => teamssQuery
                    .OrderBy(e => e.TeamMembers.Count),
                TeamSorting.OnlyAvailable => teamssQuery
                    .Where(t=> t.IsDeleted==false)
                    .OrderBy(e => e.TeamMembers.Count),
                TeamSorting.OnlyDeleted => teamssQuery
                    .Where(t=> t.IsDeleted==true)
                    .OrderBy(e => e.TeamMembers.Count),
                _ => teamssQuery
                    .OrderBy(e => e.IsDeleted)    
                    .ThenBy(e => e.Name)
            };
            


            ICollection<TeamDetailsViewModel> teamCollection = await teamssQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.TeamsPerPage)
                .Take(queryModel.TeamsPerPage)
                .Select(t => new TeamDetailsViewModel
                {
                    Id = t.Id.ToString(),
                    Name = t.Name,
                    TeamImageUrl = t.TeamImageUrl!,
                    Country = t.Country.Name,
                    TeamMembersCount = t.TeamMembers.Count,
                    Description = t.Description,
                    EstablishedOn = t.EstablishedOn.ToString(DateTimeFormats.DateFormat),
                    Town = t.Town.Name,
                    TeamMembers = t.TeamMembers
                        .Select(tm => new UserViewModel
                        {
                            Id = tm.Id.ToString(),
                            Name = tm.Name,
                            ProfileImageUrl = tm.ProfileImageUrl
                        }).ToList(),
                    MembersRequests = t.TeamRequests
                        .Where(tr =>  tr.IsAccepted == false)
                        .Select(tr => new UserViewModel
                        {
                            Id = tr.RequestFrom.Id.ToString(),
                            Name = tr.RequestFrom.Name,
                            ProfileImageUrl = tr.RequestFrom.ProfileImageUrl
                        }).ToList(),
                    GalleryPhotosModels  = t.GalleryPhotos
                        .Select(p => new GalleryPhotoModel
                        {
                            Id = p.Id.ToString(),
                            Name = p.Name,
                            URL = p.Url
                        }).ToList(),
                    TeamManager = t.TeamManager.Name
                }).ToListAsync();

            var model = new AdminAllTeamsFilteredAndPagedServiceModel()
            {
                AllTeams = teamCollection,
                TotalTeamsCount = teamssQuery.Count()
            };
            return model;
        }

        public async Task<ICollection<AllTeamsViewModel>> GetTeamRequestsByUserAsync(string userId)
        {
            var teamRequests = await dbContext.TeamsRequests
                .Where(tr => tr.RequestFromId == Guid.Parse(userId))
                .Select(tr => new AllTeamsViewModel()
                {
                    Id = tr.TeamId.ToString(),
                    Name = tr.Team.Name,
                    Country = tr.Team.Country.Name,
                    TeamImageUrl = tr.Team.TeamImageUrl!
                }).ToListAsync();


            foreach (var team in teamRequests)
            {
                team.TeamMembersCount = await GetTeamMembersCountAsync(team.Id);
            }

            return teamRequests;
        }

        public async Task<bool> IsRequestedAsync(string userId, string teamId)
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
                            && t.IsDeleted == false
                            && t.TeamMembers.Any(tm => tm.Id == Guid.Parse(userId)))
                .AnyAsync();
        }
        
        public async Task<bool> HasTeamAsync(string userId)
        {
            return await dbContext.AppUsers
                .Where(u=>u.Id == Guid.Parse(userId))
                .AnyAsync(u=>u.TeamId.HasValue);
        }

        public async Task<bool> IsDeletedAsync(string teamId)
        {
            return  await dbContext.Teams 
                .AnyAsync(t => t.Id == Guid.Parse(teamId) &&  t.IsDeleted==true);
        }

        public async Task RemoveRequestAsync(string userId, string teamId)
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
            return await dbContext.Teams
                .Where(t => t.IsDeleted == false)
                .CountAsync();
        }

        public async Task<bool> IsManagerAsync(string teamId, string userId)
        {
            Team? teamToCheck = await GetTeamByIdAsync(teamId);

            if (teamToCheck != null)
            {
                return teamToCheck.TeamManagerId.ToString() == userId;
            }

            return false;
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
                .Where(t => t.Id == Guid.Parse(id) && t.IsDeleted == false)
                .FirstOrDefaultAsync();
        }
    }
}