using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Team;
using Microsoft.AspNetCore.Mvc;

using static BikingBuddy.Common.NotificationMessagesConstants;
using static BikingBuddy.Common.ErrorMessages.TeamErrorMessages; 


namespace BikingBuddy.Web.Areas.Administration.Controllers
{
    public class TeamController : BaseAdminController
    {  
        private readonly ITeamService teamService; 
        public TeamController(ITeamService _teamService
        )
        {
            teamService = _teamService; 
        }
        
        public async Task<IActionResult> All([FromQuery] AdminAllTeamsQueryModel queryModel)
        {
            var serviceModel = await teamService.AdminAllTeamsAsync(queryModel);

            queryModel.Teams = serviceModel.AllTeams;
            queryModel.TotalTeamsCount = serviceModel.TotalTeamsCount; 


            return View(queryModel);
        }

    
    

    }
}