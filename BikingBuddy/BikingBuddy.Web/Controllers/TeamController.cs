using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Team;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Controllers
{
    public class TeamController:BaseController
    {
        private readonly ITeamService teamService;
        private readonly IEventService eventService;

        public TeamController(ITeamService _teamService, IEventService _eventService)
        {
            this.teamService = _teamService;
            this.eventService = _eventService;
        }

        public async Task<IActionResult> All()
        {
            var allTeams = await teamService.GetAllTeams();


            return View(allTeams);

        }

        //Create
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var model = new AddTeamViewModel()
            {
                CountriesCollection = await eventService.GetCountriesAsync()
            };



            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeamViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.CountriesCollection = await eventService.GetCountriesAsync();
                return View(model);
            }


            var teamManagerId =  this.User.GetId();


            await teamService.AddTeam(model,teamManagerId);


            return RedirectToAction("All", "Team");

        }


    }
}
