using System.Security.Claims;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService service;

        public EventController(IEventService _service)
        {
            this.service = _service;
        }


        //All
        public async Task<IActionResult> All()
        {
            var events = await service.GetAllEventsAsync();

            return View(events);
        }

        //Details 
        public async Task<IActionResult> Details(string id)
        {
            var eventDetails = await service.GetEventDetailsByIdAsync(id); 

            return View(eventDetails);
        }


        //Create

        public async Task<IActionResult> Add()
        {
            var addEventModel = new EventViewModel()
            {
                ActivityTypes = await service.GetTypesAsync(),
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

            await service.AddEventAsync(model, userId);

            return RedirectToAction("Index","Home");
        }

        //Read

        //TODO:

        //Update

        public async Task<IActionResult> Edit(string eventId)
        {
            EventViewModel editEvent = await service.GetEventAsync(eventId);



            editEvent.ActivityTypes = await service.GetTypesAsync();
            editEvent.CountriesCollection = await service.GetCountriesAsync();
        

            return View(editEvent);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(EventViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);

            }


            string userId = this.User.GetId();

            await service.AddEventAsync(model, userId);

            return RedirectToAction("Index", "Home");
        }




        //Delete

        //TODO:
    }
}
