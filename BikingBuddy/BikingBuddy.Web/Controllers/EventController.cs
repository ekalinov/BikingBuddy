using BikingBuddy.Common;
using BikingBuddy.Services.Data.Models.Events;

namespace BikingBuddy.Web.Controllers
{
    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.Event;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.ErrorMessages.EventErrorMessages;
    using static Common.NotificationMessagesConstants;

    public class EventController : BaseController
    {
        private readonly IEventService service;
        private readonly ICommentService commentService;

        public EventController(IEventService _service, ICommentService _commentService)
        {
            service = _service;
            commentService = _commentService;
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

            if (!ModelState.IsValid)
            {
                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);

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
            return RedirectToAction("", "Home");

        }

        //Read

        //All
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery ]AllEventsQueryModel queryModel)
        {
            AllEventsFilteredAndPagedServiceModel serviceModel = await service.AllAsync(queryModel);

            queryModel.Events = serviceModel.AllEvents;
            queryModel.TotalEventsCount = serviceModel.TotalEventsCount;
            queryModel.ActivityTypes=await service.GetActivityTypesAsync();
            
                
             
             

            return View(queryModel);
        }
 

        //Update

        [HttpGet]
        public async Task<IActionResult> Edit(string eventId)
        {

            
            EditEventViewModel? editAddEvent = await service.GetEventViewModelByIdAsync(eventId);

            if (!await service.IsOrganiser(eventId,User.GetId()) && !User.IsAdmin())
            { 
                TempData[ErrorMessage] = UnauthorizedForError;
                return Unauthorized();
            }
            
            
            if (editAddEvent != null)
            {
                editAddEvent.ActivityTypes = await service.GetActivityTypesAsync();
                editAddEvent.CountriesCollection = await service.GetCountriesAsync();

                return View(editAddEvent);
            }
            else
            {
                TempData[ErrorMessage] = EventNotExistsMessage;
                return RedirectToAction("All");

            }


        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);

            }
            
            if (!await service.IsOrganiser(model.EventId,User.GetId()) && !User.IsAdmin())
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
            
            if (!await service.IsOrganiser(eventId,User.GetId()) && !User.IsAdmin())
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
                return RedirectToAction("Details", "Event", new {eventId});
            }
 
            return RedirectToAction("All", "Event"); 
        }

        public async Task<IActionResult> Join(string eventId)
        {

            try
            {
                if (!await service.IsParticipating(eventId, User.GetId()) )
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


        public async Task<IActionResult> Mine()
        {

            try
            {
                var userEvents = await service.GetEventsByUserIdAsync(User.GetId());
                return View(userEvents);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = UserDoesNotHaveEvents;
                return RedirectToAction("All");
            }

        }
    }
}
