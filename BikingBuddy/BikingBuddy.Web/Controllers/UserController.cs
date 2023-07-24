namespace BikingBuddy.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.User;

    using static  Common.ErrorMessages.UserErrorMessages;
    using static  Common.NotificationMessagesConstants;

    public class UserController : BaseController
    {
        public readonly IEventService eventService;

        public readonly IUserService userService;

        public UserController(IEventService _eventService, IUserService _userService)
        {
            eventService = _eventService;
            userService = _userService;
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
            var userDetails = await userService.GetUserDetails(User.GetId());


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
