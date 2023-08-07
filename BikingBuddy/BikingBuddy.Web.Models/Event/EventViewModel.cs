namespace BikingBuddy.Web.Models.Event
{
    public class EventViewModel : EventMiniViewModel
    {
        
        public bool IsCompleted { get; set; }

        public int ParticipantsCount { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}