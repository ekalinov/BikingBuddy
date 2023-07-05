using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Team;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Controllers
{
    public class TeamController : BaseController
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


            var teamManagerId = this.User.GetId();


            await teamService.AddTeam(model, teamManagerId);


            return RedirectToAction("All", "Team");

        }


        //Create
        [HttpGet]
        public async Task<IActionResult> Edit(string teamId)
        {
            EditTeamViewModel teamModel = await teamService.GetTeamToEditAsync(teamId);

            teamModel.CountriesCollection = await eventService.GetCountriesAsync();


            return View(teamModel);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.CountriesCollection = await eventService.GetCountriesAsync();
                return View(model);
            }


            var teamManagerId = this.User.GetId();


            await teamService.EditTeam(model, model.Id);


            return RedirectToAction("All", "Team");

        }


        public async Task<IActionResult> Details(string teamId)
        {

            var teamDetails = await teamService.GetTeamDetailsAsync(teamId);


            return View(teamDetails);


        }


        public async Task<IActionResult> RequestToJoin(string teamId)
        {



            await teamService.SendRequest(teamId, this.User.GetId());



            return RedirectToAction("Details", "Team", new {teamId});


        }
    }
}