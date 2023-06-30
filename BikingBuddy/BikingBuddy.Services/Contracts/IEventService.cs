using BikingBuddy.Data.Models;

namespace BikingBuddy.Services.Contracts
{
    using BikingBuddy.Web.Models.Activity;
    using BikingBuddy.Web.Models.Event;
    using Web.Models;

    public interface IEventService
    {
        //All
        Task<ICollection<AllEventsViewModel>> GetAllEventsAsync();

        //Create
        Task AddEventAsync(EventViewModel model, string userId);

        //Read
        Task<EventDetailsViewModel> GetEventDetailsByIdAsync(string id);

        //Update
        Task EditEventAsync(EventViewModel model, string eventId);

        //Delete
        Task DeleteEventAsync(int id);

        //Join Event
        Task JoinEvent(string userId, string eventId);

        //Leave Event
        Task LeaveEvent(string userId, string eventId);

        //Get Events
        Task<ICollection<AllEventsViewModel>> GetEventsByUserId(string userId);

        Task<EventViewModel> GetEventViewModelByIdAsync(string id);

        Task<Event> GetEventByIdAsync(string id);


        Task<List<ActivityTypeViewModel>> GetActivityTypesAsync();

        Task<List<CountryViewModel>> GetCountriesAsync();




        //Task<List<DetailsViewModel>> GetDetailsUserModels(string userId);

        //Task LeaveEvent(string userId, int id);

    }
}
