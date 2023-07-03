using BikingBuddy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Web.Models.Team
{
    public class TeamViewModel
    {
        public TeamViewModel()
        {
            this.TeamMembers = new HashSet<TeamMemberViewModel>();
        }


        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public DateTime? EstablishedOn { get; set; }

        public string? TeamImageUrl { get; set; }

        public string TeamManager { get; set; } = null!;

        public string TeamMenagerId { get; set; } = null!;


        public ICollection<TeamMemberViewModel> TeamMembers { get; set; } 
    }
}
