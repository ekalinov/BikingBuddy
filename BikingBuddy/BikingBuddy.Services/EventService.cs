using BikingBuddy.Common.Enums;
using BikingBuddy.Web.Models.Comment;
using Microsoft.Extensions.Logging;

namespace BikingBuddy.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Globalization;
    using BikingBuddy.Data;
    using BikingBuddy.Data.Models;
    using Data.Models.Events;
    using Common;
    using Contracts;
    using Web.Models;
    using Web.Models.Activity;
    using Web.Models.Event;
    using Web.Models.User;
    using Web.Models.Event.Enums;


    public class EventService : IEventService
    {
        private readonly BikingBuddyDbContext dbContext;


        public EventService(BikingBuddyDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        //All
        public async Task<ICollection<EventMiniViewModel>> GetAllEventsAsync()
        {
            var allEvents = await dbContext.Events
                .Where(e => e.IsDeleted == false)
                .Select(e => new EventMiniViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    Distance = e.Distance.ToString(CultureInfo.InvariantCulture),
                    OrganizerUsername = e.Organizer.UserName,
                    EventImageUrl = e.EventImageUrl!,
                    ActivityType = e.ActivityType.Name,
                    Town = e.Town.Name,
                })
                .AsNoTracking()
                .ToArrayAsync();


            return allEvents;
        }

        //Create
        public async Task AddEventAsync(AddEventViewModel model, string userId)
        {
            Event newEvent = new()
            {
                Title = model.Title,
                Date = DateTime.Parse(model.Date),
                Description = model.Description,
                EventImageUrl = model.EventImageUrl,
                ActivityTypeId = model.ActivityTypeId,
                CreatedOn = DateTime.UtcNow,
                OrganizerId = Guid.Parse(userId),
                CountryId = model.CountryId,
                Town = await GetTownByNameAsync(model.TownName),
                Ascent = model.Ascent,
                Distance = model.Distance,
                Currency = model.Currency,
                Price = model.Price
            };

            if (model.Latitude != 0 && model.Longitude != 0)
            {
                newEvent.EventLocation = new EventLocation
                {
                    Longitude = model.Longitude,
                    Latitude = model.Latitude
                };
            }

            if (model.GalleryPhotosModels != null && model.GalleryPhotosModels.Any())
            {
                foreach (var photo in model.GalleryPhotosModels)
                {
                    newEvent.GalleryPhotos.Add(new EventGalleryPhoto()
                    {
                        Name = photo.Name,
                        Url = photo.URL,
                    });
                }
            }

            if (model.EventTracks != null && model.EventTracks.Count > 0)
            {
                foreach (var track in model.EventTracks)
                {
                    using (var reader = new StreamReader(track.OpenReadStream()))
                    {
                        string gpxContent = reader.ReadToEnd();

                        EventTrack eventTrack = new()
                        {
                            FileName = track.FileName,
                            GPXContent = gpxContent,
                        };

                        newEvent.Tracks!.Add(eventTrack);
                    }
                }
            }

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        //Read
        public async Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string eventId)
        {
            var eventParticipants = await GetEventParticipants(eventId);


            var eventById = await dbContext.Events
                .Where(e => e.Id == Guid.Parse(eventId))
                .Select(e => new EventDetailsViewModel
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    Distance = e.Distance.ToString(CultureInfo.InvariantCulture),
                    Ascent = e.Ascent.ToString(CultureInfo.InvariantCulture),
                    OrganizerName = e.Organizer.Name,
                    OrganizerUsername = e.Organizer.UserName,
                    EventImageUrl = e.EventImageUrl,
                    ActivityType = e.ActivityType.Name,
                    Country = $"{e.Country.Name}, {e.CountryId}",
                    Town = e.Town.Name,
                    EventsParticipants = eventParticipants,
                    EventComments = null,
                    GalleryPhotosModels = e.GalleryPhotos
                        .Select(p => new GalleryPhotoModel
                        {
                            Id = p.Id.ToString(),
                            Name = p.Name,
                            URL = p.Url
                        }).ToList()
                })
                .FirstOrDefaultAsync();


            var eventTracks = dbContext.EventsTracks
                .Where(et => et.EventId == Guid.Parse(eventId))
                .ToList();
            if (eventById!=null)
            {
            eventById.EventTracks = eventTracks;
            }
            
            return eventById;
        }


        //Update
        public async Task EditEventAsync(EditEventViewModel model)
        {
            var eventToEdit = await GetEventByIdAsync(model.EventId);
            if (eventToEdit != null)
            {
                eventToEdit.Title = model.Title;
                eventToEdit.Date = DateTime.Parse(model.Date);
                eventToEdit.Description = model.Description;
                eventToEdit.Distance = model.Distance;
                eventToEdit.Ascent = model.Ascent;
                eventToEdit.ActivityTypeId = model.ActivityTypeId;
                eventToEdit.CountryId = model.CountryId;
                eventToEdit.Town = await GetTownByNameAsync(model.TownName);
                eventToEdit.Currency = model.Currency;
                eventToEdit.Price = model.Price;

                if (eventToEdit.EventLocation != null && model.Latitude != eventToEdit.EventLocation.Latitude &&
                    model.Longitude != eventToEdit.EventLocation.Longitude)
                {
                    eventToEdit.EventLocation = new EventLocation
                    {
                        Longitude = model.Longitude,
                        Latitude = model.Latitude
                    };
                }

                if (model.EventImageUrl != null)
                {
                    eventToEdit.EventImageUrl = model.EventImageUrl;
                }

                if (model.GalleryPhotosModels != null && model.GalleryPhotosModels.Any())
                {
                    ICollection<EventGalleryPhoto> galleryPhotos = new HashSet<EventGalleryPhoto>();

                    foreach (var photo in model.GalleryPhotosModels)
                    {
                        galleryPhotos.Add(new EventGalleryPhoto
                        {
                            Event = eventToEdit,
                            Name = photo.Name,
                            Url = photo.URL
                        });
                    }

                    await dbContext.EventGalleryPhotos.AddRangeAsync(galleryPhotos);
                }

                if (model.EventTracks != null && model.EventTracks.Count > 0)
                {
                    foreach (var track in model.EventTracks)
                    {
                        using (var reader = new StreamReader(track.OpenReadStream()))
                        {
                            string gpxContent = reader.ReadToEnd();

                            EventTrack eventTrack = new()
                            {
                                FileName = track.FileName,
                                GPXContent = gpxContent,
                            };

                            eventToEdit.Tracks!.Add(eventTrack);
                        }
                    } 
                }
                    await dbContext.SaveChangesAsync();
            }
        }

        //Delete
        public async Task DeleteEventAsync(string eventId)
        {
            Event? eventToDelete = await GetEventByIdAsync(eventId);

            if (eventToDelete != null)
            {
                eventToDelete.IsDeleted = true;

                await dbContext.SaveChangesAsync();
            }
        }

        //Join Event
        public async Task JoinEventAsync(string userId, string eventId)
        {
            var currentEvent = await GetEventByIdAsync(eventId);

            if (currentEvent != null)
            {
                EventParticipants newEventParticipant = new()
                {
                    ParticipantId = Guid.Parse(userId),
                    Event = currentEvent
                };

                currentEvent.EventsParticipants.Add(newEventParticipant);
                await dbContext.SaveChangesAsync();
            }
        }

        //Leave Event  
        public async Task LeaveEventAsync(string userId, string eventId)
        {
            var currentEventParticipant = await GetEventParticipantByIdAsync(eventId, userId);

            if (currentEventParticipant != null)
            {
                dbContext.Remove(currentEventParticipant);
                await dbContext.SaveChangesAsync();
            }
        }

        //Get Event
        public async Task<EditEventViewModel?> GetEventViewModelByIdAsync(string id)
        {
            return await dbContext.Events
                .Where(e => e.Id.ToString() == id
                            && e.IsDeleted == false)
                .Select(e => new EditEventViewModel()
                {
                    EventId = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    Distance = e.Distance,
                    Ascent = e.Ascent,
                    EventImageUrl = e.EventImageUrl!,
                    TownName = e.Town.Name,
                    OrganiserId = e.OrganizerId.ToString()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IList<EventMiniViewModel>> GetNewestEventsAsync()
        {
            return await dbContext.Events
                .Where(e => e.IsDeleted == false && e.Date > DateTime.Today)
                .OrderByDescending(e => e.CreatedOn)
                .Select(e => new EventMiniViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    ActivityType = e.ActivityType.Name,
                    EventImageUrl = e.EventImageUrl,
                })
                .Take(3)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<EventMiniViewModel>> GetNewestEventsAsync(string? eventId)
        {
            return await dbContext.Events
                .Where(e => e.IsDeleted == false
                            && e.Date > DateTime.Today
                            && e.Id != Guid.Parse(eventId ?? string.Empty))
                .OrderByDescending(e => e.CreatedOn)
                .Select(e => new EventMiniViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    ActivityType = e.ActivityType.Name,
                    EventImageUrl = e.EventImageUrl,
                })
                .Take(3)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<AdminAllEventsFilteredAndPagedServiceModel> AdminAllEventAsync(
            AdminAllEventsQueryModel queryModel)
        {
            IQueryable<Event> eventsQuery = dbContext.Events
                .AsQueryable()
                .OrderBy(e => e.IsDeleted);


            if (!String.IsNullOrWhiteSpace(queryModel.ActivityType))
            {
                eventsQuery = eventsQuery
                    .Where(e => e.ActivityType.Name == queryModel.ActivityType);
            }

            if (!String.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
                eventsQuery = eventsQuery
                    .Where(e => EF.Functions.Like(e.ActivityType.Name, wildCard) ||
                                EF.Functions.Like(e.Description, wildCard) ||
                                EF.Functions.Like(e.Title, wildCard) ||
                                EF.Functions.Like(e.Country.Name, wildCard) ||
                                EF.Functions.Like(e.Town.Name, wildCard));
            }


            eventsQuery = queryModel.IsDeleted switch
            {
                DeleteStatus.Available => eventsQuery
                    .Where(t => t.IsDeleted == false),
                DeleteStatus.Deleted => eventsQuery
                    .Where(t => t.IsDeleted == true),
                DeleteStatus.All => eventsQuery,
                _ => eventsQuery
            };
            eventsQuery = queryModel.Sorting switch
            {
                EventSorting.Newest => eventsQuery
                    .OrderBy(e => e.IsDeleted)
                    .ThenByDescending(e => e.CreatedOn),
                EventSorting.MostParticipants => eventsQuery
                    .OrderBy(e => e.IsDeleted)
                    .ThenByDescending(e => e.EventsParticipants.Count()),
                EventSorting.ThisMonth => eventsQuery
                    .Where(e => e.Date.Month == DateTime.Now.Month),
                EventSorting.ThisWeek => eventsQuery
                    .Where(e => (e.Date.Day - DateTime.Now.Day) <= 7),
                _ => eventsQuery
            };


            ICollection<EventDetailsViewModel> eventCollection = await eventsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                .Take(queryModel.EventsPerPage)
                .Select(e => new EventDetailsViewModel
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    ActivityType = e.ActivityType.Name,
                    Distance = $"{e.Distance} km",
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    EventImageUrl = e.EventImageUrl,
                    Description = e.Description,
                    Town = e.Town.Name,
                    Ascent = $"{e.Ascent} m",
                    OrganizerUsername = e.Organizer.UserName,
                    OrganizerName = e.Organizer.Name,
                    Country = e.Country.Name,
                    GalleryPhotosModels = e.GalleryPhotos
                        .Select(p => new GalleryPhotoModel
                        {
                            Id = p.Id.ToString(),
                            Name = p.Name,
                            URL = p.Url
                        }).ToList(),
                    EventComments = e.EventComments
                        .Select(c => new CommentViewModel
                        {
                            Id = c.Id,
                            CommentBody = c.CommentBody,
                            CreatedOn = c.CommentedOn,
                            UserName = c.User.Name,
                            IsEdited = c.IsEdited,
                            EditedOn = c.EditedOn
                        }).ToList(),
                    EventsParticipants = e.EventsParticipants
                        .Select(ep => new UserViewModel
                        {
                            Id = ep.ParticipantId.ToString(),
                            Name = ep.Participant.Name,
                            ProfileImageUrl = ep.Participant.ProfileImageUrl
                        }).ToList(),
                })
                .ToListAsync();

            var model = new AdminAllEventsFilteredAndPagedServiceModel()
            {
                AllEvents = eventCollection,
                TotalEventsCount = eventsQuery.Count()
            };
            return model;
        }

        public async Task<AllEventsFilteredAndPagedServiceModel> AllEventsAsync(AllEventsQueryModel queryModel)
        {
            IQueryable<Event> eventsQuery = dbContext.Events
                .Where(e => e.IsDeleted == false)
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(queryModel.ActivityType))
            {
                eventsQuery = eventsQuery
                    .Where(e => e.ActivityType.Name == queryModel.ActivityType);
            }

            if (!String.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
                eventsQuery = eventsQuery
                    .Where(e => EF.Functions.Like(e.ActivityType.Name, wildCard) ||
                                EF.Functions.Like(e.Description, wildCard) ||
                                EF.Functions.Like(e.Title, wildCard) ||
                                EF.Functions.Like(e.Country.Name, wildCard) ||
                                EF.Functions.Like(e.Town.Name, wildCard));
            }

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            DateTime currentDate = DateTime.Now.Date;
            int daysUntilMonday = (int)currentDate.DayOfWeek - (int)DayOfWeek.Monday;
            DateTime startOfWeek = currentDate.AddDays(-daysUntilMonday);
            DateTime endOfWeek = startOfWeek.AddDays(6);


            eventsQuery = queryModel.Sorting switch
            {
                EventSorting.Newest => eventsQuery
                    .OrderByDescending(e => e.CreatedOn),
                EventSorting.MostParticipants => eventsQuery
                    .OrderByDescending(e => e.EventsParticipants.Count),
                EventSorting.ThisMonth => eventsQuery
                    .Where(e => e.Date.Month == currentMonth && e.Date.Year == currentYear)
                    .OrderByDescending(e => e.Date),
                EventSorting.ThisWeek => eventsQuery
                    .Where(e => e.Date >= startOfWeek && e.Date <= endOfWeek)
                    .OrderByDescending(e => e.Date),
                _ => eventsQuery
                    .OrderByDescending(e => e.Date)
            };


            ICollection<EventMiniViewModel> eventCollection = await eventsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                .Take(queryModel.EventsPerPage)
                .Select(e => new EventMiniViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    ActivityType = e.ActivityType.Name,
                    Distance = $"{e.Distance} km",
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    EventImageUrl = e.EventImageUrl,
                    Description = e.Description,
                    Town = e.Town.Name,
                }).ToListAsync();

            AllEventsFilteredAndPagedServiceModel model = new AllEventsFilteredAndPagedServiceModel()
            {
                AllEvents = eventCollection,
                TotalEventsCount = eventsQuery.Count()
            };
            return model;
        }


        public async Task<int> GetActiveEventsCountAsync()
        {
            return await dbContext.Events
                .Where(e => e.IsDeleted == false && e.Date > DateTime.Today)
                .CountAsync();
        }

        public async Task<int> GetAllEventsCountAsync()
        {
            return await dbContext.Events
                .Where(e => e.IsDeleted == false)
                .CountAsync();
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await dbContext.Events
                .Where(e => e.Id == Guid.Parse(id) && e.IsDeleted == false)
                .FirstOrDefaultAsync();
        }


        //This DTO will be used in /User/Details
        //Get All events where user is participating (completed and not completed)
        public async Task<ICollection<EventViewModel>> GetUserEventsAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId))
                .Select(ep => new EventViewModel
                {
                    Id = ep.EventId.ToString(),
                    Town = ep.Event.Town.Name,
                    Country = ep.Event.Country.Name,
                    Title = ep.Event.Title,
                    Date = ep.Event.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Distance = $"{ep.Event.Distance} km",
                    Ascent = $"{ep.Event.Ascent} m",
                    EventImageUrl = ep.Event.EventImageUrl,
                    ParticipantsCount = ep.Event.EventsParticipants.Count,
                    IsCompleted = ep.IsCompleted,
                    IsDeleted = ep.Event.IsDeleted
                }).ToListAsync();
        }

        public async Task<ICollection<EventViewModel>> GetMyEventsAsync(string userId)
        {
            return await dbContext.Events
                .Where(e => e.OrganizerId == Guid.Parse(userId))
                .Select(e => new EventViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Town = e.Town.Name,
                    Country = e.Country.Name,
                    ActivityType = e.ActivityType.Name,
                    Distance = $"{e.Distance} m",
                    Ascent = $"{e.Ascent} m",
                    Description = e.Description,
                    EventImageUrl = e.EventImageUrl
                }).ToListAsync();
        }

        public async Task<ICollection<UserViewModel>> GetEventParticipants(string eventId)
        {
            var participants = await dbContext.Users
                .Where(u => u.EventsParticipants
                    .Any(e => e.EventId == Guid.Parse(eventId)))
                .Select(u => new UserViewModel
                {
                    Id = u.Id.ToString(),
                    Name = u.Name,
                    ProfileImageUrl = u.ProfileImageUrl,
                    Country = u.Country.Name,
                    Town = u.Town.Name,
                    Helmet = u.Helmet,
                    Shoes = u.Shoes,
                    TeamName = u.Team.Name,
                    TeamId = u.TeamId.ToString(),
                })
                .ToListAsync();

            foreach (var user in participants)
            {
                user.TotalAscent = await GetUserTotalAscentAsync(user.Id);
                user.TotalDistance = await GetUserTotalDistanceAsync(user.Id);
                user.CompletedEvents = await GetCompletedEventsCountByUserAsync(user.Id);
            }

            return participants;
        }

        public async Task<List<ActivityTypeViewModel>> GetActivityTypesAsync()
        {
            var activityTypes = await dbContext.ActivityTypes
                .Select(a => new ActivityTypeViewModel()
                {
                    Id = a.Id,
                    ActivityTypeName = a.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return activityTypes;
        }

        public async Task<int?> GetCompletedEventsCountByUserAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .CountAsync(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true);
        }


        public async Task<bool> IsOrganiser(string eventId, string userId)
        {
            Event? eventToCheck = await GetEventByIdAsync(eventId);
            if (eventToCheck != null)
            {
                return eventToCheck.OrganizerId.ToString() == userId;
            }

            return false;
        }

        public async Task<bool> IsCompleted(string eventId, string userId)
        {
            return await dbContext.EventsParticipants
                .AnyAsync(ep =>
                    ep.ParticipantId == Guid.Parse(userId) && ep.EventId == Guid.Parse(eventId) &&
                    ep.IsCompleted == true);
        }

        public async Task<AllEventsFilteredAndPagedServiceModel> MineAsync(AllEventsQueryModel queryModel,
            string userId)
        {
            IQueryable<Event> eventsQuery = dbContext.Events
                .Where(e => e.EventsParticipants.Any(ep => ep.ParticipantId == Guid.Parse(userId))
                            || e.OrganizerId == Guid.Parse(userId))
                .AsQueryable();

            if (!String.IsNullOrWhiteSpace(queryModel.ActivityType))
            {
                eventsQuery = eventsQuery
                    .Where(e => e.ActivityType.Name == queryModel.ActivityType);
            }

            if (!String.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";
                eventsQuery = eventsQuery
                    .Where(e => EF.Functions.Like(e.ActivityType.Name, wildCard) ||
                                EF.Functions.Like(e.Description, wildCard) ||
                                EF.Functions.Like(e.Title, wildCard) ||
                                EF.Functions.Like(e.Country.Name, wildCard) ||
                                EF.Functions.Like(e.Town.Name, wildCard));
            }

            eventsQuery = queryModel.Sorting switch
            {
                EventSorting.Newest => eventsQuery
                    .OrderByDescending(e => e.CreatedOn),
                EventSorting.MostParticipants => eventsQuery
                    .OrderByDescending(e => e.EventsParticipants.Count()),
                EventSorting.ThisMonth => eventsQuery
                    .Where(e => e.Date.Month == DateTime.Now.Month)
                    .OrderByDescending(e => e.Date),
                EventSorting.ThisWeek => eventsQuery
                    .Where(e => (e.Date.Day - DateTime.Now.Day) <= 7)
                    .OrderByDescending(e => e.Date),
                _ => eventsQuery
                    .OrderByDescending(e => e.Date)
            };


            ICollection<EventMiniViewModel> eventCollection = await eventsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                .Take(queryModel.EventsPerPage)
                .Select(e => new EventMiniViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    ActivityType = e.ActivityType.Name,
                    Distance = $"{e.Distance} km",
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    EventImageUrl = e.EventImageUrl!,
                    Description = e.Description,
                    Town = e.Town.Name,
                }).ToListAsync();

            AllEventsFilteredAndPagedServiceModel model = new AllEventsFilteredAndPagedServiceModel()
            {
                AllEvents = eventCollection,
                TotalEventsCount = eventsQuery.Count()
            };
            return model;
        }

        public async Task<bool> IsParticipating(string eventId, string userId)
        {
            var result = await dbContext.EventsParticipants
                .AnyAsync(ep => ep.EventId == Guid.Parse(eventId)
                                && ep.ParticipantId == Guid.Parse(userId.ToLower()));

            return result;
        }

        public async Task<bool> IsActive(string eventId)
        {
            return await dbContext.Events
                .AnyAsync(ep => ep.Id == Guid.Parse(eventId)
                                && ep.Date >= DateTime.UtcNow);
        }

        public async Task<bool> IsDeleteAsync(string eventId)
        {
            return await dbContext.Events
                .AnyAsync(t => t.Id == Guid.Parse(eventId) && t.IsDeleted == true);
        }


        public async Task CompleteEventAsync(string eventId, string userId)
        {
            EventParticipants? eventToComplete = await dbContext.EventsParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == Guid.Parse(eventId)
                                           && ep.ParticipantId == Guid.Parse(userId));

            if (eventToComplete != null)
            {
                eventToComplete.IsCompleted = true;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddGalleryPhotos(AddGalleryPhotoModel model)
        {
            if (model.GalleryPhotosModels != null && model.GalleryPhotosModels.Any())
            {
                Event? eventToAddPhotos = await GetEventByIdAsync(model.EventId);

                if (eventToAddPhotos != null)
                {
                    ICollection<EventGalleryPhoto> galleryPhotos = new HashSet<EventGalleryPhoto>();

                    foreach (var photo in model.GalleryPhotosModels)
                    {
                        galleryPhotos.Add(new EventGalleryPhoto
                        {
                            Event = eventToAddPhotos,
                            Name = photo.Name,
                            Url = photo.URL
                        });
                    }

                    await dbContext.EventGalleryPhotos.AddRangeAsync(galleryPhotos);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task UploadGPXFiles(UploadGPXFileViewModel model)
        {
            var eventToEdit = await GetEventByIdAsync(model.EventId);
 
            if (eventToEdit != null && model.EventTracks is { Count: > 0 })
            {
                foreach (var track in model.EventTracks)
                {
                    using var reader = new StreamReader(track.OpenReadStream());
                    string gpxContent = await reader.ReadToEndAsync();

                    EventTrack eventTrack = new()
                    {
                        FileName = track.FileName,
                        GPXContent = gpxContent,
                    };

                    eventToEdit.Tracks!.Add(eventTrack);
                } 
            }
            await dbContext.SaveChangesAsync();
        }


        private async Task<EventParticipants?> GetEventParticipantByIdAsync(string eventId, string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId.ToString() == userId
                             && ep.EventId.ToString() == eventId)
                .FirstOrDefaultAsync();
        }

        public async Task<double> GetUserTotalDistanceAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true)
                .SumAsync(ep => ep.Event.Distance);
        }

        public async Task<double> GetUserTotalAscentAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true)
                .SumAsync(ep => ep.Event.Ascent);
        }


        //----------------------------------------------


        /// <summary>
        /// Method returns Town if there is such by given string,
        /// if the town doesn't exists in the Db the method creates new one 
        /// </summary>
        /// <param name="name"> string name of town </param>
        /// <returns>Returns Town</returns>
        public async Task<Town> GetTownByNameAsync(string name)
        {
            Town? town = await dbContext.Towns
                .Where(t => t.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();

            if (town is null)
            {
                town = new()
                {
                    Name = name
                };

                await dbContext.Towns.AddAsync(town);
                await dbContext.SaveChangesAsync();
            }

            return town;
        }

        public async Task<List<CountryViewModel>> GetCountriesAsync()
        {
            var countries = await dbContext.Countries
                .Select(a => new CountryViewModel()
                {
                    Id = a.Code,
                    Name = a.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return countries;
        }
    }
}