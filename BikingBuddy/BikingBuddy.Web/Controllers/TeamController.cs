using BikingBuddy.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Controllers
{
    public class TeamController:BaseController
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService _teamService)
        {
            this.teamService = _teamService;
        }

        public async Task<IActionResult> All()
        {
            var allteams = await teamService.GetAllTeams();


            return View(allteams);

        }





    }
}
