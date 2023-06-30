using BikingBuddy.Web.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingBuddy.Services.Contracts
{
    public interface ITeamService
    {

        Task<ICollection<CommentViewModel>> GetAllTeams(string eventId);

        Task AddTeam(string comment, string userId, string eventId);

        Task EditTeam(CommentViewModel commentModel);

        Task DeleteTeam(int commentId);

    }
}
