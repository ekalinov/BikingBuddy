using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Controllers;
using BikingBuddy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static BikingBuddy.Services.Helpers.UploadPhotosHelper;
using static BikingBuddy.Common.GlobalConstants;
using static BikingBuddy.Common.ErrorMessages.EventErrorMessages;
using static BikingBuddy.Common.NotificationMessagesConstants;


public class GalleryPhotosController : BaseController
{
    private readonly string envWebRooth;
    private readonly IEventService eventService;


    public GalleryPhotosController(IWebHostEnvironment _environment, IEventService _eventService)
    {
        eventService = _eventService;
        envWebRooth = _environment.WebRootPath;
    }


    public async Task<IActionResult> Add(AddGalleryPhotoModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        foreach (var galleryPhoto in model.GalleryPhotos!)
        {
            if (galleryPhoto is { Length: >= MaxPhotoSizeAllowed })
            {
                ModelState.AddModelError((string)"EventImage", MaxPhotoSizeAllowedErrorMessage);
                return View(model);
            }
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.GalleryPhotos != null && model.GalleryPhotos.Any())
        {
            foreach (var photo in model.GalleryPhotos)
            {
                var galleryPhotoModel = new GalleryPhotoModel
                {
                    Name = photo.Name,
                    URL = await UploadPhotoToLocalStorageAsync(EventGalleryPhotosDestinationPath, photo,
                        envWebRooth)
                };
                model.GalleryPhotosModels!.Add(galleryPhotoModel);
            }
        }

        try
        {
            await eventService.AddGalleryPhotos(model);
            TempData[SuccessMessage] = AddGalleryPhotosSuccess;
            return RedirectToAction("Details", "Event", new { eventId = model.EventId });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = AddGalleryPhotosError;

            return RedirectToAction("All", "Event");
        }
    }
}