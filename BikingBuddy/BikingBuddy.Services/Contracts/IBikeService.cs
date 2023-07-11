using BikingBuddy.Web.Models.Bike;
using BikingBuddy.Web.Models.BikeType;

namespace BikingBuddy.Services.Contracts
{
    public interface IBikeService
    {

        Task<ICollection<BikeDetailsViewModel>> GetUserBikesAsync(string userId);

        Task<ICollection<BikeTypeViewModel>> GetBikeTypesAsync();


        Task AddBikeToUserAsync(AddBikeViewModel model, string getId);

        Task EditBike(EditBikeViewModel viewModel, string bikeId);

        Task<EditBikeViewModel?> GetBikeToEditAsync(string bikeId);

        Task RemoveBikeFromUserAsync(string bikeId, string userId);
    }
}                           
