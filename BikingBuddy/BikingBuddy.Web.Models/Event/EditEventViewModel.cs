using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Event
{
    public class EditEventViewModel :AddEventViewModel
    {

        public string EventId { get; set; } = null!;
    }
}
