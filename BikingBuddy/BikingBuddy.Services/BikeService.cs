
using BikingBuddy.Data;
using BikingBuddy.Data.Models;

namespace BikingBuddy.Services
{
    using Microsoft.EntityFrameworkCore;


    using Web.Models.Bike;
    using Web.Models.BikeType;
    using Data;
    using Contracts; 

    public class BikeService : IBikeService
    {

        
        public readonly BikingBuddyDbContext dbContext;


        public BikeService(BikingBuddyDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<ICollection<BikeDetailsViewModel>> GetUserBikesAsync(string userId)
        {
            return await dbContext.Bikes
                .Where(b => b.AppUserId == Guid.Parse(userId))
                .Select(b => new BikeDetailsViewModel
                {
                    Id = b.Id,
                    FrameBrand = b.FrameBrand,
                    FrameSize = b.FrameSize,
                    WheelSize = b.WheelSize,
                    WheelBrand = b.WheelBrand,
                    Drivetrain = b.Drivetrain,
                    Fork = b.Fork,
                    BikeType = b.BikeType.Name
                })
                .ToListAsync();

        }

        public async Task<EditBikeViewModel?> GetBikeToEditAsync(string bikeId)
        {
            return await dbContext.Bikes
                .Where(b => b.Id.ToLower() == bikeId.ToLower())
                .Select(b => new EditBikeViewModel
                {
                    Id = b.Id,
                    FrameBrand = b.FrameBrand,
                    FrameSize = b.FrameSize,
                    WheelSize = b.WheelSize,
                    WheelBrand = b.WheelBrand,
                    Drivetrain = b.Drivetrain,
                    Fork = b.Fork
                })
                .FirstOrDefaultAsync();


        }

        public async Task RemoveBikeAsync(string bikeId)
        {
            Bike? bikeToEdit = await GetBikeByIdAsync(bikeId);

            if (bikeToEdit != null)
            {
                bikeToEdit.IsDeleted = true;
                
                await dbContext.SaveChangesAsync();
            }

        }

        public async Task<ICollection<BikeTypeViewModel>> GetBikeTypesAsync()
        {
            return await dbContext.BikeTypes
                .Select(a => new BikeTypeViewModel()
                {
                    Id = a.Id,
                    BikeTypeName = a.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddBikeToUserAsync(AddBikeViewModel model, string userId)
        {
            Bike bike = new()
            {
                FrameBrand = model.FrameBrand,
                FrameSize = model.FrameSize,
                WheelSize = model.WheelSize,
                WheelBrand = model.WheelBrand,
                Drivetrain = model.Drivetrain,
                Fork = model.Fork,
                BikeTypeId = model.BikeTypesId,
                AppUserId = Guid.Parse(userId)
            };


            await dbContext.Bikes.AddAsync(bike);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditBike(EditBikeViewModel viewModel, string bikeId)
        {
            Bike? bikeToEdit = await GetBikeByIdAsync(bikeId);

            if (bikeToEdit != null)
            {

                bikeToEdit.FrameBrand = viewModel.FrameBrand;
                bikeToEdit.FrameSize = viewModel.FrameSize;
                bikeToEdit.WheelSize = viewModel.WheelSize;
                bikeToEdit.WheelBrand = viewModel.WheelBrand;
                bikeToEdit.Drivetrain = viewModel.Drivetrain;
                bikeToEdit.Fork = viewModel.Fork;
                bikeToEdit.BikeTypeId = viewModel.BikeTypesId;

                await dbContext.SaveChangesAsync();
            }
        }

      
        
        
        private async Task<Bike?> GetBikeByIdAsync(string bikeId)
        {
            return await dbContext.Bikes.FirstOrDefaultAsync(b => b.Id == bikeId);
        }
    }
}