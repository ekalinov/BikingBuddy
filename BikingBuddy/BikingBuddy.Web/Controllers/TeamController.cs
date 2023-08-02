using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BikingBuddy.Common.NotificationMessagesConstants;
using static BikingBuddy.Common.ErrorMessages.TeamErrorMessages;
using static BikingBuddy.Common.GlobalConstants;
using static BikingBuddy.Services.Helpers.UploadPhotosHepler;

namespace BikingBuddy.Web.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly IEventService eventService;
        private readonly IWebHostEnvironment environment;

        public TeamController(ITeamService _teamService,
            IEventService _eventService,
            IWebHostEnvironment _environment
        )
        {
            teamService = _teamService;
            eventService = _eventService;
            environment = _environment;
        }


        //Read
        [AllowAnonymous]
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

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var allTeams = await teamService.GetAllTeamsAsync();
                return View(allTeams);
            }
            catch (Exception)
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
            if (model.TeamImage is { Length: >= MaxPhotoSizeAllowed })
            {
                ModelState.AddModelError((string)"TeamImage", MaxPhotoSizeAllowedErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                model.CountriesCollection = await eventService.GetCountriesAsync();
                return View(model);
            }

            if (model.TeamImage != null)
            {
                string envWebRooth = environment.WebRootPath;

                model.TeamImageUrl =  await UploadPhotoToLocalStorageAsync(TeamPhotoDestinationPath, model.TeamImage, envWebRooth);
            }

            var teamManagerId = User.GetId();

            try
            {
                await teamService.AddTeamAsync(model, teamManagerId);

                TempData[SuccessMessage] = TeamAddedSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = AddTeamError;
                return View(model);
            }


            return RedirectToAction("All", "Team");
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(string teamId)
        {
            EditTeamViewModel? teamModel = await teamService.GetTeamToEditAsync(teamId);

            if (!await teamService.IsManagerAsync(teamId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return Unauthorized();
            }

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

        //Delete

        [HttpPost]
        public async Task<IActionResult> Delete(string teamId)
        {
            if (!await teamService.IsManagerAsync(teamId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return Unauthorized();
            }

            try
            {
                await teamService.DeleteTeamAsync(teamId);

                TempData[SuccessMessage] = TeamDeletedSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DeleteTeamError;
                return RedirectToAction("Details", "Team", new { teamId });
            }

            return RedirectToAction("All", "Team");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditTeamViewModel model)
        {
            if (!await teamService.IsManagerAsync(model.Id, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return Unauthorized();
            }

            if (model.TeamImage is { Length: >= MaxPhotoSizeAllowed })
            {
                ModelState.AddModelError((string)"TeamImage", MaxPhotoSizeAllowedErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                model.CountriesCollection = await eventService.GetCountriesAsync();
                return View(model);
            }

            if (model.TeamImage != null)
            {
                string envWebRooth = environment.WebRootPath;
                model.TeamImageUrl =
                    await  UploadPhotoToLocalStorageAsync(TeamPhotoDestinationPath, model.TeamImage, envWebRooth);
            }

            try
            {
                await teamService.EditTeamAsync(model, model.Id);
                TempData[SuccessMessage] = TeamEditedSuccessfully;


                return RedirectToAction("Details", "Team", new { teamId = model.Id });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditTeamError;
                return View(model);
            }
        }


        public async Task<IActionResult> RequestToJoin(string teamId)
        {
            try
            {
                if (await teamService.IsRequestedAsync(User.GetId(), teamId))
                {
                    TempData[ErrorMessage] = RequestAlreadySend;
                }
                else
                {
                    await teamService.SendRequestAsync(teamId, User.GetId());
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
                await teamService.RemoveRequestAsync(memberId, teamId);
                TempData[InformationMessage] = RequestRejected;
            }
            catch (Exception)
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
            catch (Exception)
            {
                TempData[ErrorMessage] = AddMemberError;
            }


            return RedirectToAction("Details", "Team", new { teamId });
        }


        public async Task<IActionResult> RemoveMember(string memberId, string teamId)
        {
            if (await teamService.IsManagerAsync(teamId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return Unauthorized();
            }

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
            catch (Exception)
            {
                TempData[ErrorMessage] = RemoveMemberError;
            }


            return RedirectToAction("Details", "Team", new { teamId });
        }
    }
}