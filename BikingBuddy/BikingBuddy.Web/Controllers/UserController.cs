using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BikingBuddy.Web.Controllers
{
    public class UserController : BaseController
    {
        public readonly IEventService eventService;

        public UserController(IEventService _eventService)
        {
            this.eventService = _eventService;
        }
       
    }
}
