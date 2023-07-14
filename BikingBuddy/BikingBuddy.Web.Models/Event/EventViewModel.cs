namespace BikingBuddy.Web.Models.Event
{
    public class EventViewModel: EventMiniViewModel
	{
        public double Distance { get; set; }

        public bool IsCompleted { get; set; }

        public int ParticipantsCount { get; set; }


    }
}
