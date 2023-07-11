using BikingBuddy.Common;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using static BikingBuddy.Common.NotificationMessagesConstants;
using static BikingBuddy.Common.ErrorMessages.UserErrorMessages;
using BikingBuddy.Services;
using BikingBuddy.Web.Models.Bike;

namespace BikingBuddy.Web.Controllers
{
    public class UserController : BaseController
    {
        public readonly IEventService eventService;

        public readonly IUserService userService;

        public UserController(IEventService _eventService, IUserService _userService)
        {
            this.eventService = _eventService;
            this.userService = _userService;
        }


        public async Task<IActionResult> Details(string userId)
        {
            UserDetailsViewModel? model = await userService.GetUserDetails(userId);

            if (model != null)
            {
                return View(model);

            }
            else
            {
                TempData[ErrorMessage] = UserNotFound;

            }

            //Todo: return to same page
            return BadRequest();

        }


        public async Task<IActionResult> MyProfile()
        {
            var userDetails = await userService.GetUserDetails(this.User.GetId());


            return View(userDetails);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            EditUserViewModel? model = await userService.GetUserForEditAsync(userId);


            try
            {
                if (model != null)
                {
                    model.CountriesCollection = await eventService.GetCountriesAsync();

                    return View(model);
                }

            }
            catch (Exception)
            {

                TempData[ErrorMessage] = UpdatingProfileError;

            }

            return RedirectToAction("MyProfile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {


            try
            {

                await userService.UpdateProfileInfo(model);

                TempData[SuccessMessage] = ProfileChangesSaved;


                return RedirectToAction("MyProfile", "User");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = UpdatingProfileError;
                model.CountriesCollection = await eventService.GetCountriesAsync();


                return View(model);
            }

        }

    }
}
