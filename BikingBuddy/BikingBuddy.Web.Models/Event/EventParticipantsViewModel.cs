using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Event
{
    public class EventParticipantsViewModel
    {
        public string ParticipantId { get; set; } = null!;

        public string ParticipantName { get; set; } = null!;

        public string EventId { get; set; } = null!;

        public string EventName { get; set; } = null!;
    }
}
