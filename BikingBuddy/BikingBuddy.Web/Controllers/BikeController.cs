
namespace BikingBuddy.Web.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.Bike;

    using static  Common.NotificationMessagesConstants;
    using static  Common.ErrorMessages.BikeErrorMessages;
    
    public class BikeController : BaseController
    {
        private readonly IBikeService bikeService;


        public BikeController(IBikeService _bikeService)
        {
            this.bikeService = _bikeService;
        }

        public async Task<IActionResult> Add()
        {
            var addEventModel = new AddBikeViewModel()
            {
                BikeTypes = await bikeService.GetBikeTypesAsync()
            };

            return View(addEventModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBikeViewModel model)
        {


            if (!ModelState.IsValid)
            {
                model.BikeTypes = await bikeService.GetBikeTypesAsync();


                return View(model);
            }


            try
            {
                await bikeService.AddBikeToUserAsync(model, this.User.GetId());

                TempData[SuccessMessage] = BikeAddedSuccessfully;


            }
            catch (Exception)
            {

                TempData[ErrorMessage] = AddBikeError;

                model.BikeTypes = await bikeService.GetBikeTypesAsync();

                return View(model);
            }


            return RedirectToAction("MyProfile", "User");




        }
    }
}
