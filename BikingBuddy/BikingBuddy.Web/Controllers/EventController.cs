using System.Security.Claims;
using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Mvc;

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

               eventDetails.EventComments = await commentService.GetAllComments(eventId);

                return View(eventDetails);
          

            //return RedirectToAction("All", "Event", new {eventId});

        }

        //Create

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var addEventModel = new EventViewModel()
            {
                ActivityTypes = await service.GetActivityTypesAsync(),
                CountriesCollection = await service.GetCountriesAsync()
            };

            return View(addEventModel);
        }


        [HttpPost]

        public async Task<IActionResult> Add(EventViewModel model)
        {

            if (!ModelState.IsValid)
            {
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

        //TODO:

        //Update

        [HttpGet]
        public async Task<IActionResult> Edit(string eventId)
        {
            EventViewModel editEvent = await service.GetEventViewModelByIdAsync(eventId);

            editEvent.ActivityTypes = await service.GetActivityTypesAsync();
            editEvent.CountriesCollection = await service.GetCountriesAsync();

            return View(editEvent);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {

                model.ActivityTypes = await service.GetActivityTypesAsync();
                model.CountriesCollection = await service.GetCountriesAsync();

                return View(model);

            }


            string userId = this.User.GetId();

            await service.EditEventAsync(model, userId);

            return RedirectToAction("All", "Event");
        }




        //Delete

        //TODO:

        public async Task<IActionResult> Join(string eventId)
        {
            var userId = User.GetId();

            try
            {
             await service.JoinEvent(userId,eventId);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Mine", "Event");
        }

        public async Task<IActionResult> Leave(string eventId)
        {
            var userId = User.GetId();

            try
            {
                await service.LeaveEvent(userId, eventId);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("All", "Event");
        }


        public async Task<IActionResult> Mine()
        {
            var userId = User.GetId();

            var userEvents = await service.GetEventsByUserId(userId);

            if (!userEvents.Any())
            {
                //TODO:
            }

            return View(userEvents);
        }
    }
}
