namespace BikingBuddy.Services.Contracts
{          
    using  Web.Models.Bike;
    using  Web.Models.BikeType;
    public interface IBikeService
    {

        Task<ICollection<BikeDetailsViewModel>> GetUserBikesAsync(string userId);



        Task AddBikeToUserAsync(AddBikeViewModel model, string getId);

        Task EditBike(EditBikeViewModel viewModel, string bikeId);

        Task<EditBikeViewModel?> GetBikeToEditAsync(string bikeId);

        Task RemoveBikeAsync(string bikeId);
    }
}
