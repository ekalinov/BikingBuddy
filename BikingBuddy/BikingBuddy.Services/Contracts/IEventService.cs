namespace BikingBuddy.Services.Contracts
{
    using BikingBuddy.Web.Models.Activity;
    using BikingBuddy.Web.Models.Event;
    using Web.Models;

    public interface IEventService
    {
        Task<ICollection<AllEventsViewModel>> GetAllEventsAsync();

        Task<List<ActivityTypeViewModel>> GetTypesAsync();

        Task AddEventAsync(EventViewModel model, string userId);

        Task<EventViewModel> GetEventAsync(string id);

        Task EditEventAsync(EventViewModel model, int eventId);

        Task<EventDetailsViewModel> GetEventDetailsByIdAsync(string id);

        Task DeleteEventAsync(int id);
        
        Task JoinEvent(string userId, int id);


        Task<List<CountryViewModel>> GetCountriesAsync();

        //Task<List<DetailsViewModel>> GetDetailsUserModels(string userId);

        //Task LeaveEvent(string userId, int id);

    }
}
