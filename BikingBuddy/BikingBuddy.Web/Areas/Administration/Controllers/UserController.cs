
namespace BikingBuddy.Web.Areas.Administration.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Contracts;
using Models.User;
using static Common.ErrorMessages.UserErrorMessages;
using static Common.NotificationMessagesConstants;

public class UserController : BaseAdminController
{
    private readonly IUserService userService;

    public UserController(IUserService _userService)
    {
        userService = _userService;
    }


    public async Task<IActionResult> All([FromQuery] AdminAllUsersQueryModel queryModel)
    {
        var serviceModel = await userService.AdminAllUsersAsync(queryModel);

        queryModel.Users = serviceModel.AllUser;
        queryModel.Admins = serviceModel.Admins;
        queryModel.TotalUsersCount = serviceModel.TotalUsersCount;

        return View(queryModel);
    }

    public async Task<IActionResult> MakeAdmin(string userId)
    {
        try
        {
            await userService.MakeUserAdminAsync(userId);

            TempData[SuccessMessage] = MakeAdminSuccess;
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = MakeAdminError;
        }

        return RedirectToAction("All", "User", new { Area = "Administration" });
    }
}