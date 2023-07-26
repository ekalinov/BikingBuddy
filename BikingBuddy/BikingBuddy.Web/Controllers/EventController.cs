﻿using BikingBuddy.Common;
using BikingBuddy.Services.Data.Models.Events;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BikingBuddy.Web.Controllers
{
    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.Event;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static ErrorMessages.EventErrorMessages;
    using static NotificationMessagesConstants;
    using static GlobalConstants;

    public class EventController : BaseController
    {
        private readonly IEventService service;
        private readonly ICommentService commentService;
        private readonly IWebHostEnvironment environment;

        public EventController(IEventService _service,
            ICommentService _commentService,
            IWebHostEnvironment _environment)
        {
            service = _service;
            commentService = _commentService;
            environment = _environment;
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
                ModelState.AddModelError((string)"EventImage",MaxPhotoSizeAllowedErrorMessage); 
            }
            
            if (!ModelState.IsValid)
            {
                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);
            }

            if (model.EventImage != null)
            {
                await UploadPhotoToLocalStorageAsync(model);
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
            AllEventsFilteredAndPagedServiceModel serviceModel = await service.AllAsync(queryModel);

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
                ModelState.AddModelError((string)"EventImage",MaxPhotoSizeAllowedErrorMessage); 
            }
            
            if (!ModelState.IsValid)
            {
                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);
            }

            if (model.EventImage != null)
            {
                await UploadPhotoToLocalStorageAsync(model);
            }

            if (!await service.IsOrganiser(model.EventId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedForError;
                return Unauthorized();
            } 

            try
            {
                await service.EditEventAsync(model, User.GetId());
                TempData[SuccessMessage] = EventSuccessfullyEdited;
                return RedirectToAction("Details", "Event", new { eventId = model.EventId });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditEventError;

                return RedirectToAction("All", "Event");
            }
        }


        //Delete

        [HttpPost]
        public async Task<IActionResult> Delete(string eventId)
        {
            if (!await service.IsOrganiser(eventId, User.GetId()) && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedForError;
                return Unauthorized();
            }

            try
            {
                await service.DeleteEventAsync(eventId);

                TempData[SuccessMessage] = EventDeletedSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DeleteEventError;
                return RedirectToAction("Details", "Event", new { eventId });
            }

            return RedirectToAction("All", "Event");
        }

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
            AllEventsFilteredAndPagedServiceModel serviceModel = await service.MineAsync(queryModel,User.GetId());

            queryModel.Events = serviceModel.AllEvents;
            queryModel.TotalEventsCount = serviceModel.TotalEventsCount;
            queryModel.ActivityTypes = await service.GetActivityTypesAsync();


            return View(queryModel);
        }
        
        

        //-------------Upload Files--------------------

        private async Task UploadPhotoToLocalStorageAsync(EditEventViewModel model)
        {
            var ext = Path.GetExtension(model.EventImage!.FileName).ToLowerInvariant();
 

            string folderStorage = "FileStorage/EventPhotos/";

            folderStorage += Guid.NewGuid() + ext;


            string serverFolder = Path.Combine(environment.WebRootPath, folderStorage);

            await model.EventImage!.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            model.EventImageUrl = "/" + folderStorage;
        }


        private async Task UploadPhotoToLocalStorageAsync(AddEventViewModel model)
        {
            var ext = Path.GetExtension(model.EventImage!.FileName).ToLowerInvariant();
           
            string folderStorage = "FileStorage/EventPhotos/";

            folderStorage += Guid.NewGuid() + ext;


            string serverFolder = Path.Combine(environment.WebRootPath, folderStorage);

            await model.EventImage!.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            model.EventImageUrl = "/" + folderStorage;
        }

        //TODO: IMPORT GPX file track to event
        private async Task UploadTrackFileToLocalStorageAsync(EditEventViewModel model)
        {
            string folderStorage = "FileStorage/EventPhotos/";

            folderStorage += Guid.NewGuid() + ".gpx";


            string serverFolder = Path.Combine(environment.WebRootPath, folderStorage);

            await model.EventImage!.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            model.EventImageUrl = "/" + folderStorage;
        }
    }
}