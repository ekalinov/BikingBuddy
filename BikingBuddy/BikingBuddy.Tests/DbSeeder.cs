using System.Runtime.InteropServices.ComTypes;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Activity;
using BikingBuddy.Web.Models.Event;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BikingBuddy.Tests;

public static class DbSeeder
{ 

    private static string testEventId= null!;
    private static string testEventTitle= null!;
    private static string testEventDescription= null!;

    private static string testUserId= null!;
    private static string testUserName= null!;



    public static Event testEvent;
    public static AppUser testUser;

    public static ICollection<ActivityTypeViewModel> testActivityTypesModels;


    public static async Task SeedDatabase(BikingBuddyDbContext dbContext)
    {
       

       testUserName = "Test Testov";
        testUserId = "df666d9f-4332-45ea-adae-36aba1f83289";
        
         testUser = new()
        {
            Id = Guid.Parse(testUserId),
            Name = testUserName,
            CountryId = "BG",
            TownId = 1 
        };
        
        
        

        testEventTitle = "Test Event";
        testEventDescription = "Test Event Description";
        testEventId = "cd0a1105-ba73-4da7-8769-f24c7a22b4b7"; 
        
        
         testEvent = new()
        {
            Id = Guid.Parse(testEventId),
            Title = testEventTitle,
            CreatedOn = DateTime.UtcNow,
            ActivityType = dbContext.ActivityTypes.First(a=>a.Id==1),
            ActivityTypeId = 1,
            Description = testEventDescription,
            OrganizerId = Guid.Parse(testUserId),
            Organizer = testUser,
            CountryId =  "BG", 
            TownId = 1,
            IsDeleted = false
        };

  
        await dbContext.Events.AddAsync(testEvent);
        await dbContext.AppUsers.AddAsync(testUser);
        

        await dbContext.SaveChangesAsync();
 
    }
    
}