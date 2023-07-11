using BikingBuddy.Data.Models;

namespace BikingBuddy.Services.Contracts
{
    using Web.Models.Activity;
    using Web.Models.Event;
    using Web.Models;

    public interface IEventService
    {
        //All
        Task<ICollection<AllEventsViewModel>> GetAllEventsAsync();

        //Create
        Task AddEventAsync(AddEventViewModel model, string userId);

        //Read
        Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string id);

        //Update
        Task EditEventAsync(EditEventViewModel model, string eventId);

        //Delete
        Task DeleteEventAsync(int id);

        //Join Event
        Task JoinEventAsync(string userId, string eventId);

        //Leave Event
        Task LeaveEventAsync(string userId, string eventId);

        //Get Events
        Task<ICollection<AllEventsViewModel>> GetEventsByUserIdAsync(string userId);

        Task<ICollection<EventViewModel>> GetUserEventsAsync(string userId);

        Task<int?> GetCompletedEventsCountByUserAsync(string userId);

        Task<EditEventViewModel?> GetEventViewModelByIdAsync(string id);

        Task<Event?> GetEventByIdAsync(string id);


        Task<bool> IsParticipating(string userId, string eventId);

        //ActivityTypes
        Task<List<ActivityTypeViewModel>> GetActivityTypesAsync();

        //Countries
        Task<List<CountryViewModel>> GetCountriesAsync();

        //Towns
        Task<Town> GetTownByNameAsync(string name);

    }
}
