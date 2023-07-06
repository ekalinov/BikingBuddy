using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Team;
using Microsoft.AspNetCore.Mvc;

using static BikingBuddy.Common.NotificationMessagesConstants;
using static BikingBuddy.Common.ErrorMessages.TeamErrorMessages;

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
            try
            {
                var allTeams = await teamService.GetAllTeams();
                return View(allTeams);

            }
            catch (Exception e)
            {

                TempData[ErrorMessage] = AllTeamsLoadingFail;
            }

            return View();


        }

        //Create
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new AddTeamViewModel()
                {
                    CountriesCollection = await eventService.GetCountriesAsync()
                };

                if (!model.CountriesCollection.Any())
                {

                    TempData[ErrorMessage] = CountriesNotPreloaded;
                }

                return View(model);

            }
            catch (Exception)
            {
                TempData[ErrorMessage] = AddTeamError;
            }

            return RedirectToAction("All", "Team");
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

            try
            {
                await teamService.AddTeam(model, teamManagerId);

                TempData[SuccessMessage] = TeamAddedSuccessfully;


            }
            catch (Exception)
            {

                TempData[ErrorMessage] = AddTeamError;
                return View(model);
            }


            return RedirectToAction("All", "Team");

        }

        //Create
        [HttpGet]
        public async Task<IActionResult> Edit(string teamId)
        {
            EditTeamViewModel? teamModel = await teamService.GetTeamToEditAsync(teamId);

            try
            {
                if (teamModel != null)
                {
                    teamModel.CountriesCollection = await eventService.GetCountriesAsync();

                    return View(teamModel);
                }
               
            }
            catch (Exception)
            {

                TempData[ErrorMessage] = EditTeamError;

            }

            return RedirectToAction("All", "Team");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.CountriesCollection = await eventService.GetCountriesAsync();
                return View(model);
            }


            try
            {
                await teamService.EditTeam(model, model.Id);
                TempData[SuccessMessage] = TeamEditedSuccessfully;


                return RedirectToAction("Details", "Team", new {teamId= model.Id });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditTeamError;
                return View(model);
            }

        }


        public async Task<IActionResult> Details(string teamId)
        {

            try
            {
                var teamDetails = await teamService.GetTeamDetailsAsync(teamId);
                return View(teamDetails);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = TeamDoesNotExist;
            }


            return RedirectToAction("All", "Team");


        }

        public async Task<IActionResult> RequestToJoin(string teamId)
        {


            try
            {
                if (await teamService.IsRequested(this.User.GetId(), teamId))
                {
                    TempData[ErrorMessage] = RequestAlreadySend;
                }
                else
                {
                    await teamService.SendRequest(teamId, this.User.GetId());
                    TempData[SuccessMessage] = RequestSend;
                }



            }
            catch (Exception)
            {
                TempData[ErrorMessage] = RequestAlreadySend;
            }



            return RedirectToAction("Details", "Team", new { teamId });

        }


        public async Task<IActionResult> RejectRequest(string memberId, string teamId)
        {

            try
            {
                await teamService.RejectRequest(memberId, teamId);
                TempData[InformationMessage] = RequestRejected;
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = RequestNotFound;

            }


            return RedirectToAction("Details", "Team", new { teamId });
        }
         

        public async Task<IActionResult> AddMember(string memberId, string teamId)
        {
            try
            {
                if (!await teamService.IsMemberAsync(memberId, teamId))
                {
                    await teamService.AddMemberAsync(memberId, teamId);
                    TempData[SuccessMessage] = MemberAddedSuccessfully;

                }
                else
                {
                    TempData[ErrorMessage] = UserAlreadyMember;
                }

            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = AddMemberError;

            }


            return RedirectToAction("Details", "Team", new { teamId });
        }


        public async Task<IActionResult> RemoveMember(string memberId, string teamId)
        {

            try
            {
                if (await teamService.IsMemberAsync(memberId, teamId))
                {
                    await teamService.RemoveMemberAsync(memberId, teamId);
                    TempData[SuccessMessage] = MemberRemovedSuccessfully;

                }
                else
                {
                    TempData[ErrorMessage] = UserIsNotAMember;
                }

            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = RemoveMemberError;

            }


            return RedirectToAction("Details", "Team", new { teamId });
        }
    }
}