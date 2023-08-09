namespace BikingBuddy.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using Infrastructure.Extensions;
    using Models.Bike;
    using static Common.NotificationMessagesConstants;
    using static Common.ErrorMessages.BikeErrorMessages;

    public class BikeController : BaseController
    {
        private readonly IBikeService bikeService;


        public BikeController(IBikeService _bikeService)
        {
            bikeService = _bikeService;
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(AddBikeViewModel model)
        {
            if (!ModelState.IsValid)
            {
               // return View(model);
            }


            try
            {
                await bikeService.AddBikeToUserAsync(model, User.GetId());

                TempData[SuccessMessage] = BikeAddedSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = AddBikeError;


              //  return View(model);
            }

            return RedirectToAction("MyProfile", "User");
        }


        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(string bikeId)
        {
            EditBikeViewModel? bikeModel = await bikeService.GetBikeToEditAsync(bikeId);

            try
            {
                if (bikeModel != null)
                {
                    return View(bikeModel);
                }
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditBikeError;
            }

            return RedirectToAction("MyProfile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBikeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = EditBikeError;
                model.OwnerId = User.GetId();
                return View(model);
            }


            try
            {
                await bikeService.EditBike(model, model.Id);
                TempData[SuccessMessage] = BikeEditedSuccessfully; 
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditBikeError; 
            }
            
            return RedirectToAction("MyProfile", "User");
        }


        //Delete 

        public async Task<IActionResult> Remove(string bikeId)
        {
            try
            {
                await bikeService.RemoveBikeAsync(bikeId);
                TempData[SuccessMessage] = BikeRemovedFromUserSuccessfully;
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = BikeRemovedFromUserError;
            }

            return RedirectToAction("MyProfile", "User");
        }
    }
}