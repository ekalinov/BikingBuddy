using BikingBuddy.Web.Models.Bike;
using BikingBuddy.Web.Models.BikeType;

namespace BikingBuddy.Services.Contracts
{
    public interface IBikeService
    {

        Task<ICollection<BikeDetailsViewModel>> GetUserBikesAsync(string userId);

        Task<ICollection<BikeTypeViewModel>> GetBikeTypesAsync();


        Task AddBikeToUserAsync(AddBikeViewModel model, string getId);
    }
}                           
