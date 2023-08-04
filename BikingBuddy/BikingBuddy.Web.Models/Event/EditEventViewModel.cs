namespace BikingBuddy.Web.Models.Event
{
    public class EditEventViewModel : AddEventViewModel
    {
        public string EventId { get; set; } = null!;

        public string OrganiserId { get; set; } = null!;
    }
}