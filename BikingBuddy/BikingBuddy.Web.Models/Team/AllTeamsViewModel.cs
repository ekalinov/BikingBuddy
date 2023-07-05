using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Team
{
    public class AllTeamsViewModel
    {


        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string TeamImageUrl { get; set; } = null!;

        public string Country { get; set; } = null!;

        public int TeamMembersCount { get; set; }

    }
}
