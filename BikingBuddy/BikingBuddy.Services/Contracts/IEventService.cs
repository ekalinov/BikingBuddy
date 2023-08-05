using BikingBuddy.Data.Models;
using BikingBuddy.Services.Data.Models.Events;

namespace BikingBuddy.Services.Contracts
{
    using Web.Models;
    using Web.Models.Activity;
    using Web.Models.Event;

    public interface IEventService
    {
        //All
        Task<ICollection<EventMiniViewModel>> GetAllEventsAsync();

        //Create
        Task AddEventAsync(AddEventViewModel model, string userId);

        //Read
        Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(string id);

        //Update
        Task EditEventAsync(EditEventViewModel model);

        //Delete
        Task DeleteEventAsync(string eventId);

        //Join Event
        Task JoinEventAsync(string userId, string eventId);

        //Leave Event
        Task LeaveEventAsync(string userId, string eventId);

        //Get Events
  

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

        Task<IList<EventMiniViewModel>> GetNewestEventsAsync();
        
        Task<IList<EventMiniViewModel>> GetNewestEventsAsync(string? eventId);
 

        Task<AllEventsFilteredAndPagedServiceModel> AllEventsAsync(AllEventsQueryModel queryModel);

        Task<AdminAllEventsFilteredAndPagedServiceModel> AdminAllEventAsync(AdminAllEventsQueryModel queryModel);
        Task<AllEventsFilteredAndPagedServiceModel> MineAsync(AllEventsQueryModel queryModel, string userId);
        
        
        Task<int> GetActiveEventsCountAsync();
        Task<int> GetAllEventsCountAsync();
        Task<bool> IsOrganiser(string eventId, string userId);

        Task<bool> IsActive(string eventId);

    }
}