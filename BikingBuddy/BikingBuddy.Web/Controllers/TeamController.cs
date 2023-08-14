namespace BikingBuddy.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    
    using Models;
    using Models.Team;
    using Services.Contracts;
    using Infrastructure.Extensions;
    
    using static Common.NotificationMessagesConstants;
    using static Common.ErrorMessages.TeamErrorMessages;
    using static Common.GlobalConstants;
    using static Services.Helpers.UploadPhotosHelper;
    
    
    public class TeamController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly IEventService eventService;
        private readonly IWebHostEnvironment environment;
        private readonly string envWebRooth;


        public TeamController(ITeamService _teamService,
            IEventService _eventService,
            IWebHostEnvironment _environment
        )
        {
            teamService = _teamService;
            eventService = _eventService;
            environment = _environment;
            envWebRooth = environment.WebRootPath;
        }


        //Read 
        public async Task<IActionResult> Details(string teamId)
        {
            try
            {
                if (await teamService.IsDeletedAsync(teamId))
                {
                    TempData[ErrorMessage] = AlreadyDeleted;
                    return RedirectToAction("All", "Team");
                }
            
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
        [ValidateAntiForgeryToken]
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
                model.TeamImageUrl =
                    await UploadPhotoToLocalStorageAsync(TeamPhotoDestinationPath, model.TeamImage, envWebRooth);
            }

            if (model.GalleryPhotos != null && model.GalleryPhotos.Any())
            {
                foreach (var photo in model.GalleryPhotos)
                {
                    var galleryPhotoModel = new GalleryPhotoModel
                    {
                        Name = photo.Name,
                        URL = await UploadPhotoToLocalStorageAsync(TeamGalleryPhotosDestinationPath, photo, envWebRooth)
                    };
                    model.GalleryPhotosModels!.Add(galleryPhotoModel);
                }
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
                return RedirectToAction("Error", "Home", new {statusCode=401});
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
 

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTeamViewModel model)
        {
            if (!await teamService.IsManagerAsync(model.Id, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return RedirectToAction("Error", "Home", new {statusCode=401});
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
                model.TeamImageUrl =
                    await UploadPhotoToLocalStorageAsync(TeamPhotoDestinationPath, model.TeamImage, envWebRooth);
            }

            if (model.GalleryPhotos != null && model.GalleryPhotos.Any())
            {
                foreach (var photo in model.GalleryPhotos)
                {
                    var galleryPhotoModel = new GalleryPhotoModel
                    {
                        Name = photo.Name,
                        URL = await UploadPhotoToLocalStorageAsync(TeamGalleryPhotosDestinationPath, photo, envWebRooth)
                    };
                    model.GalleryPhotosModels!.Add(galleryPhotoModel);
                }
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

        
        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string teamId, string returnUrl)
        {
            if (!await teamService.IsManagerAsync(teamId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return RedirectToAction("Error", "Home", new {statusCode=401});
            }
            if (await teamService.IsDeletedAsync(teamId))
            {
                TempData[ErrorMessage] = AlreadyDeleted;  
                
                if (!string.IsNullOrEmpty(returnUrl))
                { 
                    return LocalRedirect(returnUrl);
                }
                
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
            
            if (!string.IsNullOrEmpty(returnUrl))
            {
                
                return LocalRedirect(returnUrl);
            }
  
            return RedirectToAction("All", "Team");
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
            if (!await teamService.IsManagerAsync(teamId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return RedirectToAction("Error", "Home", new {statusCode=401});
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