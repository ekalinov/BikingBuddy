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
        public async Task<ICollection<AllEventsViewModel>> GetAllEventsAsync()
        {
            var allEvents = await dbContext.Events
                .Select(e => new AllEventsViewModel()
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
            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();
        }

        //Read
        public async Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string id)
        {
            var eventParticipants = await GetEventParticipants(id);

            var eventById = await dbContext.Events
                .Where(e => e.Id.ToString() == id)
                .OrderByDescending(e => e.Date)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    Distance = e.Distance.ToString(CultureInfo.InvariantCulture),
                    Ascent = e.Ascent.ToString(CultureInfo.InvariantCulture),
                    OrganizerName = e.Organizer.Name,
                    OrganizerUsername = e.Organizer.UserName,
                    EventImageUrl = e.EventImageUrl!,
                    ActivityType = e.ActivityType.Name,
                    Country = $"{e.Country.Name}, {e.CountryId}",
                    Town = e.Town.Name,
                    EventsParticipants = eventParticipants
                })
                .FirstOrDefaultAsync();

            return eventById;
        }

        //Update
        public async Task EditEventAsync(EditEventViewModel model, string eventId)
        {
            var eventToEdit = await GetEventByIdAsync(model.EventId);
            if (eventToEdit != null)
            {
                eventToEdit.Title = model.Title;
                eventToEdit.Date = DateTime.Parse(model.Date);
                eventToEdit.Description = model.Description;
                eventToEdit.Distance = model.Distance;
                eventToEdit.Ascent = model.Ascent;
                eventToEdit.EventImageUrl = model.EventImageUrl;
                eventToEdit.ActivityTypeId = model.ActivityTypeId;
                eventToEdit.CountryId = model.CountryId;
                eventToEdit.Town = await GetTownByNameAsync(model.TownName);
            }

            await dbContext.SaveChangesAsync();
        }

        //Delete
        public Task DeleteEventAsync(int id)
        {
            //Todo: SoftDelete 
            throw new NotImplementedException();
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
                .Where(e => e.Id.ToString() == id)
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
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IList<EventMiniViewModel>> GetNewestEventsAsync()
        {
            return await dbContext.Events
                .OrderByDescending(e => e.CreatedOn)
                .Select(e => new EventMiniViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    EventImageUrl = e.EventImageUrl,
                })
                .Take(3)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<AllEventsFilteredAndPagedServiceModel> AllAsync(AllEventsQueryModel queryModel)
        {
            IQueryable<Event> eventsQuery = dbContext.Events
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


            ICollection<AllEventsViewModel> eventCollection = await eventsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.EventsPerPage)
                .Take(queryModel.EventsPerPage)
                .Select(e => new AllEventsViewModel()
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

        public async Task<int> GetActiveEventsCountAsync()
        {
            return await  dbContext.Events
                .Where(e => e.IsDeleted == false && e.Date > DateTime.Today)
                .CountAsync();
        }

        public async Task<int> GetAllEventsCountAsync()
        {
            return await dbContext.Events
                .CountAsync();
        }

        public async Task<Event?> GetEventByIdAsync(string id)
        {
            return await dbContext.Events
                .Where(e => e.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<AllEventsViewModel>> GetEventsByUserIdAsync(string userId)
        {
            return await dbContext.Events
                .Where(e => e.EventsParticipants
                                .Any(ep => ep.ParticipantId.ToString() == userId)
                            || e.OrganizerId.ToString() == userId)
                .Select(e => new AllEventsViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    OrganizerUsername = e.Organizer.UserName,
                    Distance = e.Distance.ToString(CultureInfo.InvariantCulture),
                    EventImageUrl = e.EventImageUrl!,
                    ActivityType = e.ActivityType.Name,
                    Town = e.Town.Name,
                })
                .AsNoTracking()
                .ToArrayAsync();
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
                    Title = ep.Event.Title,
                    Date = ep.Event.Date.ToString(DateTimeFormats.DateFormat),
                    Distance = ep.Event.Distance,
                    EventImageUrl = ep.Event.EventImageUrl,
                    ParticipantsCount = ep.Event.EventsParticipants.Count(),
                    IsCompleted = ep.IsCompleted
                }).ToListAsync();
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

        public async Task<int?> GetCompletedEventsCountByUserAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .CountAsync(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true);
        }

        private async Task<ICollection<UserViewModel>> GetEventParticipants(string id)
        {
            return await dbContext.EventsParticipants
                .Where(e => e.EventId == Guid.Parse(id))
                .Select(ep => new UserViewModel
                {
                    Id = ep.ParticipantId.ToString(),
                    Name = ep.Participant.Name,
                    ProfileImageUrl = ep.Participant.ProfileImageUrl
                })
                .ToListAsync();
        }

        public async Task<bool> IsParticipating(string eventId, string userId)
        {
            return await dbContext.EventsParticipants
                .AnyAsync(ep => ep.EventId == Guid.Parse(eventId)
                                && ep.ParticipantId == Guid.Parse(userId));
        }

        private async Task<EventParticipants?> GetEventParticipantByIdAsync(string eventId, string userId)
        {
            return await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId.ToString() == userId
                             && ep.EventId.ToString() == eventId)
                .FirstOrDefaultAsync();
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
    }
}