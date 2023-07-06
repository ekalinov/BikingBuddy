namespace BikingBuddy.Web.Models.Event
{
    public class EventViewModel
    {

        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Date { get; set; } = null!;

        public double Distance { get; set; }

        public bool IsCompleted { get; set; }

        public string? EventImageUrl { get; set; } = null!;

        public int ParticipantsCount { get; set; }


    }
}
