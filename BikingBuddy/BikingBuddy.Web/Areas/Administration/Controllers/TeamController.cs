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

    
    
        //Delete

        [HttpPost]
        public async Task<IActionResult> Delete(string teamId)
        {
            if (!await teamService.IsManagerAsync(teamId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return Unauthorized();
            }
            if (await teamService.IsDeletedAsync(teamId))
            {
                TempData[ErrorMessage] = AlreadyDeleted;
                return RedirectToAction("All", "Team");
            }
            try
            {
                await teamService.DeleteTeamAsync(teamId);

                TempData[SuccessMessage] = TeamDeletedSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DeleteTeamError;
            }

            return RedirectToAction("All", "Team");
        }

    }
}