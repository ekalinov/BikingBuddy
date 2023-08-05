namespace BikingBuddy.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Extensions;
    using Services.Contracts;
    using Models.Event;
    using BikingBuddy.Services.Data.Models.Events;
    using Models;
    using static Common.ErrorMessages.EventErrorMessages;
    using static Common.NotificationMessagesConstants;
    using static Common.GlobalConstants;
    using static Services.Helpers.UploadPhotosHepler;

    public class EventController : BaseController
    {
        private readonly IEventService service;
        private readonly ICommentService commentService;
        private readonly string envWebRooth;

        public EventController(IEventService _service,
            ICommentService _commentService,
            IWebHostEnvironment _environment)
        {
            service = _service;
            commentService = _commentService;
            envWebRooth = _environment.WebRootPath;
        }


        //Details 
        [AllowAnonymous]
        public async Task<IActionResult> Details(string eventId)
        {
            var eventDetails = await service.GetEventDetailsByIdAsync(eventId);

            if (eventDetails != null)
            {
                eventDetails.EventComments = await commentService.GetAllComments(eventId);
            }


            return View(eventDetails);


            //return RedirectToAction("All", "Event", new {eventId});
        }

        //Create

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var addEventModel = new AddEventViewModel()
            {
                ActivityTypes = await service.GetActivityTypesAsync(),
                CountriesCollection = await service.GetCountriesAsync()
            };

            return View(addEventModel);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (model.EventImage is { Length: >= MaxPhotoSizeAllowed })
            {
                ModelState.AddModelError((string)"EventImage", MaxPhotoSizeAllowedErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);
            }

            if (model.EventImage != null)
            {
                model.EventImageUrl =
                    await UploadPhotoToLocalStorageAsync(EventPhotoDestinationPath, model.EventImage, envWebRooth);
            }

            if (model.GalleryPhotos != null && model.GalleryPhotos.Any())
            {
                foreach (var photo in model.GalleryPhotos)
                {
                    var galleryPhotoModel = new GalleryPhotoModel
                    {
                        Name = photo.Name,
                        URL = await UploadPhotoToLocalStorageAsync(EventGalleryPhotosDestinationPath, photo,
                            envWebRooth)
                    };
                    model.GalleryPhotosModels!.Add(galleryPhotoModel);
                }
            }

            string userId = User.GetId();

            try
            {
                await service.AddEventAsync(model, userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("All", "Event");
        }

        //Read

        //All
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllEventsQueryModel queryModel)
        {
            AllEventsFilteredAndPagedServiceModel serviceModel = await service.AllEventsAsync(queryModel);

            queryModel.Events = serviceModel.AllEvents;
            queryModel.TotalEventsCount = serviceModel.TotalEventsCount;
            queryModel.ActivityTypes = await service.GetActivityTypesAsync();


            return View(queryModel);
        }


        //Update

        [HttpGet]
        public async Task<IActionResult> Edit(string eventId)
        {
            EditEventViewModel? editAddEvent = await service.GetEventViewModelByIdAsync(eventId);

            if (!await service.IsOrganiser(eventId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedForError;
                return Unauthorized();
            }


            if (editAddEvent == null)
            {
                TempData[ErrorMessage] = EventNotExistsMessage;
                return RedirectToAction("All");
            }

            editAddEvent.ActivityTypes = await service.GetActivityTypesAsync();
            editAddEvent.CountriesCollection = await service.GetCountriesAsync();
            return View(editAddEvent);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            if (model.EventImage is { Length: >= MaxPhotoSizeAllowed })
            {
                ModelState.AddModelError((string)"EventImage", MaxPhotoSizeAllowedErrorMessage);
            }

            if (!ModelState.IsValid)
            {
                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);
            }

            if (model.EventImage != null)
            {
                model.EventImageUrl =
                    await UploadPhotoToLocalStorageAsync(EventPhotoDestinationPath, model.EventImage, envWebRooth);
            }
            if (model.GalleryPhotos != null && model.GalleryPhotos.Any())
            {
                foreach (var photo in model.GalleryPhotos)
                {
                    var galleryPhotoModel = new GalleryPhotoModel
                    {
                        Name = photo.Name,
                        URL = await UploadPhotoToLocalStorageAsync(EventGalleryPhotosDestinationPath, photo,
                            envWebRooth)
                    };
                    model.GalleryPhotosModels!.Add(galleryPhotoModel);
                }
            }

            if (!await service.IsOrganiser(model.EventId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedForError;
                return Unauthorized();
            }

            try
            {
                await service.EditEventAsync(model);
                TempData[SuccessMessage] = EventSuccessfullyEdited;
                return RedirectToAction("Details", "Event", new { eventId = model.EventId });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditEventError;

                return RedirectToAction("All", "Event");
            }
        }


        //Delete is in Administrator Area
 

        public async Task<IActionResult> Join(string eventId)
        {
            try
            {
                if (!await service.IsParticipating(eventId, User.GetId()))
                {
                    await service.JoinEventAsync(User.GetId(), eventId);
                    TempData[SuccessMessage] = SuccessJoiningEvent;
                }
                else
                {
                    TempData[ErrorMessage] = UserAlreadyParticipatingErrorMessage;
                }
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = JoinEventError;
                return RedirectToAction("All", "Event");
            }

            return RedirectToAction("Mine", "Event");
        }

        public async Task<IActionResult> Leave(string eventId)
        {
            try
            {
                if (await service.IsParticipating(eventId, User.GetId()))
                {
                    await service.LeaveEventAsync(User.GetId(), eventId);
                    TempData[SuccessMessage] = SuccessLeavingEvent;
                }
                else
                {
                    TempData[ErrorMessage] = UserNotParticipatingErrorMessage;
                }
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = LeaveEventError;
                return RedirectToAction("Mine", "Event");
            }

            return RedirectToAction("All", "Event");
        }


        public async Task<IActionResult> Mine([FromQuery] AllEventsQueryModel queryModel)
        {
            AllEventsFilteredAndPagedServiceModel serviceModel = await service.MineAsync(queryModel, User.GetId());

            queryModel.Events = serviceModel.AllEvents;
            queryModel.TotalEventsCount = serviceModel.TotalEventsCount;
            queryModel.ActivityTypes = await service.GetActivityTypesAsync();


            return View(queryModel);
        }

        //TODO: IMPORT GPX file track to event
    }
}