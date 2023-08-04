namespace BikingBuddy.Web.Models.Event
{
    public class AllEventsViewModel : EventMiniViewModel
    {
        public string Distance { get; set; } = null!;

        public string OrganizerUsername { get; set; } = null!;

        public string Town { get; set; } = null!;
    }
}