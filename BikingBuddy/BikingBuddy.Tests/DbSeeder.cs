using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Web.Models.Activity;

namespace BikingBuddy.Tests;

public static class DbSeeder
{
    
    private static string testEventId = null!;
    private static string testEventTitle = null!;
    private static string testEventDescription = null!;
    
    private static string testTeamId = null!;
    private static string testTeamTitle = null!;
    private static string testTeamDescription = null!;

    
    private static string testUserId = null!;
    private static string testUserName = null!;
    
    private static string testMemberId = null!;
    private static string testMemberName = null!;


    public static Event testEvent= null!;
    public static AppUser testUser= null!;
    public static AppUser testMember= null!;

    public static Team testTeam= null!;

    public static ICollection<ActivityTypeViewModel>? testActivityTypesModels;


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

        
        
        testMemberName = "Test Member";
        testMemberId = "8056358f-68fe-4c25-8503-282f8ada3090";

        testMember = new()
        {
            Id = Guid.Parse(testMemberId),
            Name = testMemberName,
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
            Date = DateTime.UtcNow.AddDays(10),
            CreatedOn = DateTime.UtcNow,
            ActivityType = dbContext.ActivityTypes.First(a => a.Id == 1),
            ActivityTypeId = 1,
            Description = testEventDescription,
            OrganizerId = Guid.Parse(testUserId),
            Organizer = testUser,
            CountryId = "BG",
            TownId = 1,
            IsDeleted = false
        };

        testTeamId = "26b6c6f3-fe99-494b-a7c9-eb22416eaafa";
        testTeamTitle = "Test Team";
        testTeamDescription = "Test Team Description";

        testTeam = new Team
        {
            Id = Guid.Parse(testTeamId),
            Name = testTeamTitle,
            Description = testTeamDescription,
            CountryId = "BG",
            TownId = 1, 
            IsDeleted = false,
            TeamManagerId = testUser.Id
        };





        await dbContext.Teams.AddAsync(testTeam);
        await dbContext.Events.AddAsync(testEvent);
        await dbContext.AppUsers.AddAsync(testUser);
        await dbContext.AppUsers.AddAsync(testMember);


        await dbContext.SaveChangesAsync();
    }
}