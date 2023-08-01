using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikingBuddy.Services.Contracts
{          
    using  Web.Models.Bike;
    using  Web.Models.BikeType;
    public interface IBikeService
    {

        Task<ICollection<BikeDetailsViewModel>> GetUserBikesAsync(string userId);

        Task<ICollection<BikeTypeViewModel>> GetBikeTypesAsync();


        Task AddBikeToUserAsync(AddBikeViewModel model, string getId);

        Task EditBike(EditBikeViewModel viewModel, string bikeId);

        Task<EditBikeViewModel?> GetBikeToEditAsync(string bikeId);

        Task RemoveBikeAsync(string bikeId);
    }
}
