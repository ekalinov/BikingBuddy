using BikingBuddy.Common;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models;
using BikingBuddy.Web.Models.Activity;
using BikingBuddy.Web.Models.Comment;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Team;
using BikingBuddy.Web.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static BikingBuddy.Common.ErrorMessages.EventErrorMessages;

namespace BikingBuddy.Services
{
    public class EventService : IEventService
    {
        private readonly BikingBuddyDbContext dbContext;
        

        public EventService(BikingBuddyDbContext _dbContext)
        {
            this.dbContext = _dbContext;
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
                    Distance = e.Distance.ToString(),
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
                OrganizerId = Guid.Parse(userId),
                CountryId = model.CountryId,
                Town = await GetTownByName(model.TownName),
                Ascent = model.Ascent,
                Distance = model.Distance,

            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();

        }

        //Read
        public async Task<EventDetailsViewModel> GetEventDetailsByIdAsync(string id)
        {
            var eventById = await dbContext.Events
                .Where(e => e.Id.ToString() == id)
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date,
                    Description = e.Description,
                    Distance = e.Distance.ToString(),
                    Ascent = e.Ascent.ToString(),
                    OrganizerName = e.Organizer.Name,
                    OrganizerUsername = e.Organizer.UserName,
                    EventImageUrl = e.EventImageUrl!,
                    ActivityType = e.ActivityType.Name,
                    Country = string.Format("{0}, {1}", e.Country.Name, e.CountryId),
                    Town = e.Town.Name,
                    EventsParticipants = e.EventsParticipants
                        .Select(ep=> new UserViewModel
                        {
                            Id = ep.ParticipantId.ToString(),
                            Name = ep.Participant.Name,
                           ProfileImageUrl = ep.Participant.ProfileImageUrl
                        })
                        .ToList()
                })
                .OrderByDescending(e => e.Date)
                .FirstOrDefaultAsync();

            if (eventById is null)
            {
                throw new NullReferenceException(EventNotExistsMessage);
            }

            return eventById!;
        }

        //Update
        public async Task EditEventAsync(EditEventViewModel model, string eventId)
        {
            var eventToEdit = await GetEventByIdAsync(model.EventId);


            eventToEdit.Title = model.Title;
            eventToEdit.Date = DateTime.Parse(model.Date);
            eventToEdit.Description = model.Description;
            eventToEdit.Distance = model.Distance;
            eventToEdit.Ascent = model.Ascent;
            eventToEdit.EventImageUrl = model.EventImageUrl;
            eventToEdit.ActivityTypeId = model.ActivityTypeId;
            eventToEdit.CountryId = model.CountryId;
            eventToEdit.Town = await GetTownByName(model.TownName);

            await dbContext.SaveChangesAsync();
        }

        //Delete
        public Task DeleteEventAsync(int id)
        {
            //Todo: SoftDelete 
            throw new NotImplementedException();
        }

        //Join Event
        public async Task JoinEvent(string userId, string eventId)
        {
            var currentEvent = await GetEventByIdAsync(eventId);

            EventParticipants newEventParticipant = new()
            {
                ParticipantId = Guid.Parse(userId),
                Event = currentEvent
            };

            try
            {
                currentEvent.EventsParticipants.Add(newEventParticipant);
                await dbContext.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException(UserAlreadyParticipatingErrorMessage);
            }
        }

        //Leave Event  
        public async Task LeaveEvent(string userId, string eventId)
        {
            var currentEventParticipant = await dbContext.EventsParticipants
                .Where(ep => ep.ParticipantId.ToString() == userId
                             && ep.EventId.ToString() == eventId)
                .FirstOrDefaultAsync();

            if (currentEventParticipant is null)
            {
                throw new NullReferenceException(UserNotParticipatingErrorMessage);
            }

            dbContext.Remove(currentEventParticipant);
            await dbContext.SaveChangesAsync();
        }


        //Get Event
        public async Task<EditEventViewModel> GetEventViewModelByIdAsync(string id)
        {
            var eventViewModelById = await dbContext.Events
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


            if (eventViewModelById is null)
            {
                throw new Exception(EventNotExistsMessage);
            }
            return eventViewModelById!;
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            Event? eventById = await dbContext.Events
                .Where(e => e.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            if (eventById is null)
                throw new NullReferenceException(EventNotExistsMessage);

            return eventById;
        }

        public async Task<ICollection<AllEventsViewModel>> GetEventsByUserId(string userId)
        {
            var allUserEvents = await dbContext.Events
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

            if (!allUserEvents.Any())
            {
                throw new Exception(UserDoesNotHaveEvents);
            }

            return allUserEvents;
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

        //Additional collections getter
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


        public async Task<int> GetCompletedEventsCountByUserAsync(string userId)
        {
            return await dbContext.EventsParticipants
                .CountAsync(ep => ep.ParticipantId == Guid.Parse(userId) && ep.IsCompleted == true);
        }

        //----------------------------------------------

        /// <summary>
        /// Method returns Town if there is such by given string,
        /// if the town doesn't exists in the Db the method creates new one 
        /// </summary>
        /// <param name="name"> string name of town </param>
        /// <returns>Returns Town</returns>
        public async Task<Town> GetTownByName(string name)
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
