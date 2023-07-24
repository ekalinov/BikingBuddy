
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

        //Create
        [HttpGet]
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
                await bikeService.AddBikeToUserAsync(model, User.GetId());

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


        //Update
        [HttpGet]
        public async Task<IActionResult> Edit(string bikeId)
        {
            EditBikeViewModel? bikeModel = await bikeService.GetBikeToEditAsync(bikeId);





            try
            {
                if (bikeModel != null)
                {
                    bikeModel.BikeTypes = await bikeService.GetBikeTypesAsync();

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
                model.BikeTypes = await bikeService.GetBikeTypesAsync();

                return View(model);
            }


            try
            {
                await bikeService.EditBike(model, model.Id);
                TempData[SuccessMessage] = BikeEditedSuccessfully;


                return RedirectToAction("MyProfile", "User");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = EditBikeError;
                model.BikeTypes = await bikeService.GetBikeTypesAsync();

                return View(model);
            }

        }


        //Delete 

        public async Task<IActionResult> Remove(string bikeId)
        {


            try
            {

                await bikeService.RemoveBikeFromUserAsync(bikeId, User.GetId());
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
