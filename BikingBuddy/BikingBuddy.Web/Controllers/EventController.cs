using System.Security.Claims;
using BikingBuddy.Common;
using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Mvc;

using static BikingBuddy.Common.ErrorMessages.EventErrorMessages;
using static BikingBuddy.Common.NotificationMessagesConstants;



namespace BikingBuddy.Web.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService service;
        private readonly ICommentService commentService;

        public EventController(IEventService _service, ICommentService _commentService)
        {
            this.service = _service;
            this.commentService = _commentService;
        }



        //Details 
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

            string userId = this.User.GetId();

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
        public async Task<IActionResult> All()
        {
            var events = await service.GetAllEventsAsync();

            return View(events);
        }

        //TODO: TOastr notifications in Event Controller

        //Update

        [HttpGet]
        public async Task<IActionResult> Edit(string eventId)
        {
            EditEventViewModel? editAddEvent = await service.GetEventViewModelByIdAsync(eventId);

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


            string userId = this.User.GetId();

            try
            {

                await service.EditEventAsync(model, userId);
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

        //TODO: Soft Delete 

        public async Task<IActionResult> Join(string eventId)
        {
            var userId = User.GetId();

            try
            {
                if (!await service.IsParticipating(eventId, userId))
                {
                    await service.JoinEventAsync(userId, eventId);
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
            var userId = User.GetId();

            try
            {

                if (await service.IsParticipating(eventId, userId))
                {
                    await service.LeaveEventAsync(userId, eventId);
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
                var userEvents = await service.GetEventsByUserIdAsync(this.User.GetId());
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
