using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikingBuddy.Common;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models;
using BikingBuddy.Web.Models.Activity;
using BikingBuddy.Web.Models.Comment;
using BikingBuddy.Web.Models.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;

namespace BikingBuddy.Services
{
    public class EventService : IEventService
    {
        private readonly BikingBuddyDbContext dbContext;

        public EventService(BikingBuddyDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }



        public async Task<ICollection<AllEventsViewModel>> GetAllEventsAsync()
        {
            var events = await dbContext.Events
                .Select(e => new AllEventsViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    Distance = e.Distance.ToString(),
                    EventImageUrl = e.EventImageUrl!,
                    ActivityType = e.ActivityType.Name,
                    Town = e.Town.Name,
                })
                .AsNoTracking()
                .ToArrayAsync();
            
            return events;

        }

        public async Task<List<ActivityTypeViewModel>> GetTypesAsync()
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

        public async Task AddEventAsync(EventViewModel model, string userId)
        {
            Town town = await GetTownByName(model.TownName);

            Municipality municipality;

            if (!string.IsNullOrEmpty(model.Municipality))
            {
                municipality = await GetMunicipalityByName(model.Municipality);

            }
            else
            {
                municipality = null!;
            }

            Event newEvent = new()
            {
                Title = model.Title,
                Date = DateTime.Parse(model.Date),
                Description = model.Description,
                EventImageUrl = model.EventImageUrl,
                ActivityTypeId = model.ActivityTypeId,
                OrganizerId = Guid.Parse(userId),
                CountryId = model.CountryId,
                Town = town,
                Municipality = municipality,
                Ascent = model.Ascent,
                Distance = model.Distance,

            };

            await dbContext.Events.AddAsync(newEvent);
            await dbContext.SaveChangesAsync();

        }

        public async Task<EventViewModel> GetEventViewModelByIdAsync(string id)
        {
            var eventViewModelById = await dbContext.Events
                .Where(e => e.Id.ToString() == id)
                .Select(e => new EventViewModel()
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Date = e.Date.ToString(DateTimeFormats.DateTimeFormat),
                    Description = e.Description,
                    Distance = e.Distance,
                    Ascent = e.Ascent,
                    EventImageUrl = e.EventImageUrl!,
                    TownName = e.Town.Name,
                    Municipality = e.Municipality.Name,
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return eventViewModelById!;
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            Event? eventById = await dbContext.Events
                .Where(e => e.Id == Guid.Parse(id))
                .FirstOrDefaultAsync();

            if (eventById is null)
                  throw new NullReferenceException("There is no event with this Id!");

            return eventById;
        }


        public async Task EditEventAsync(EventViewModel model, string eventId)
        {
            var eventToEdit = await GetEventByIdAsync(model.Id);

            Town town = await GetTownByName(model.TownName);

            Municipality municipality;

            if (!string.IsNullOrEmpty(model.Municipality))
            {
                municipality = await GetMunicipalityByName(model.Municipality);

            }
            else
            {
                municipality = null!;
            }


            eventToEdit.Title=model.Title;
            eventToEdit.Date = DateTime.Parse(model.Date);
            eventToEdit.Description = model.Description;
            eventToEdit.Distance = model.Distance;
            eventToEdit.Ascent = model.Ascent;
            eventToEdit.EventImageUrl = model.EventImageUrl;
            eventToEdit.ActivityTypeId = model.ActivityTypeId;
            eventToEdit.CountryId = model.CountryId;
            eventToEdit.Municipality = municipality;
            
            
            await dbContext.SaveChangesAsync();



        }

        public async Task<EventDetailsViewModel> GetEventDetailsByIdAsync(string id)
        {
            var eventById = await dbContext.Events
                .Where(e => e.Id.ToString() == id)
                .Select(e => new  EventDetailsViewModel()
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
                    Country = string.Format("{0}, {1}",e.Country.Name,e.CountryId),
                    Town = e.Town.Name,
                    Municipality = e.Municipality!.Name,
                    
                })
                .OrderByDescending(e=>e.Date)
                .FirstOrDefaultAsync();

            return eventById!;
        }

        public Task DeleteEventAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task JoinEvent(string userId, int id)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------


        public async Task<Town> GetTownByName(string name)
        {
            Town? town = await dbContext.Towns
                .Where(t => t.Name == name)
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

        public async Task<Municipality> GetMunicipalityByName(string name)
        {
            Municipality? municipality = await dbContext.Municipalities
                .Where(t => t.Name == name)
                .FirstOrDefaultAsync();

            if (municipality is null)
            {
                municipality = new()
                {
                    Name = name
                };

                await dbContext.Municipalities.AddAsync(municipality);
                await dbContext.SaveChangesAsync();
            }

            return municipality;
        }
    }
}
