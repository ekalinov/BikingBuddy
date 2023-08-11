namespace BikingBuddy.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.User;
    using static Common.GlobalConstants;
    using static Common.ErrorMessages.UserErrorMessages;
    using static Common.NotificationMessagesConstants;
    using static Services.Helpers.UploadPhotosHelper;

    public class UserController : BaseController
    {
        private readonly IEventService eventService;
        private readonly IUserService userService;
        private readonly string envWebRooth;


        public UserController(IEventService _eventService,
            IUserService _userService,
            IWebHostEnvironment _environment)
        {
            eventService = _eventService;
            userService = _userService;

            envWebRooth = _environment.WebRootPath;
        }


        public async Task<IActionResult> MyProfile()
        {
            var userDetails = await userService.GetUserDetails(User.GetId());
            if (userDetails != null)
            {
                return View(userDetails);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            if (User.GetId() != userId && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return RedirectToAction("Error", "Home", new { statusCode = 401 });
            }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (User.GetId().ToUpper() != model.Id && !User.IsAdmin())
            {
                TempData[ErrorMessage] = UnauthorizedErrorMessage;
                return RedirectToAction("Error", "Home", new { statusCode = 401 });
            }

            if (model.ProfileImage is { Length: >= MaxPhotoSizeAllowed })
            {
                ModelState.AddModelError((string)"EventImage", MaxPhotoSizeAllowedErrorMessage);
            }


            if (!ModelState.IsValid)
            {
                model.CountriesCollection = await eventService.GetCountriesAsync();
                return View(model);
            }

            if (model.ProfileImage != null)
            {
                model.ProfileImageUrl =
                    await UploadPhotoToLocalStorageAsync(UserProfilePhotoDestinationPath, model.ProfileImage,
                        envWebRooth);
            }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string userId)
        {
            if ( !User.IsAdmin() && User.GetId() != userId)
            {
                TempData[ErrorMessage] = DeleteErrorUnauthorised;
                return RedirectToAction("Error", "Home", new { statusCode = 401 });
            }

            if (await userService.IsDeletedAsync(userId))
            {
                TempData[ErrorMessage] = UserNotFound;
                return RedirectToAction("index", "Home");
            }

            try
            {
                await userService.DeleteUserAccountAsync(userId);

                TempData[SuccessMessage] = UserAccountDeletedSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = DeleteProfileError;
            }

            return RedirectToAction("index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Equipment(EquipmentViewModel model)
        {
            try
            {
                await userService.AddEditEquipment(model);

                TempData[SuccessMessage] = EquipmentSuccessfully;

                return RedirectToAction("MyProfile", "User");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditEquipmentError;

                return RedirectToAction("MyProfile", "User");
            }
        }
    }
}