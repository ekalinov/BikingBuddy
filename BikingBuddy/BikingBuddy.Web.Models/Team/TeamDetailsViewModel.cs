using BikingBuddy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Team
{
    public class TeamDetailsViewModel
    {


        public TeamDetailsViewModel()
        {

            this.TeamMembers = new HashSet<TeamMemberViewModel>();
        }


        public string Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string EstablishedOn { get; set; }

        public string TeamImageUrl { get; set; }

        public string Country { get; set; } = null!;

        public string Town { get; set; } = null!;

        public ICollection<TeamMemberViewModel> TeamMembers { get; set; }

        public string TeamManager { get; set; } = null!;


    }
}
