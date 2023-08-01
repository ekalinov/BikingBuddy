using BikingBuddy.Web.Models.Team;

namespace BikingBuddy.Tests.Services;

using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;
using static DbSeeder;

[TestFixture]
public class TeamServiceTests
{
    private BikingBuddyDbContext dbContext = null!;

    private ITeamService teamService = null!;
    private IUserService userService = null!;
    private IBikeService bikeService = null!;
    private IEventService eventService = null!;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<BikingBuddyDbContext>()
            .UseInMemoryDatabase(databaseName: "BikingBuddyDbContextInMemory" + Guid.NewGuid())
            .Options;

        dbContext = new BikingBuddyDbContext(options);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        await SeedDatabase(dbContext);

        eventService = new EventService(dbContext);
        userService = new UserService(dbContext, eventService, bikeService);

        teamService = new TeamService(eventService, dbContext, userService);
    }


    // Task AddTeamAsync(AddTeamViewModel model, string teamManagerId);
    [Test]
    public async Task AddTeam_ValidEntry()
    {
        var countriesCollection = await eventService.GetCountriesAsync();

        var testTeamModel = new AddTeamViewModel
        {
            Name = "Test Team Title new",
            Description = "Test Team Description new one",
            CountryId = "BG",
            EstablishedOn = DateTime.UtcNow,
            CountriesCollection = countriesCollection,
            TownName = "Sofia"
        };

        string testUserId = testUser.Id.ToString();

        await teamService.AddTeamAsync(testTeamModel, testUserId);

        Assert.Multiple(async () =>
        {
            Assert.That(await dbContext.Teams.CountAsync(), Is.EqualTo(2));
            Assert.That(await dbContext.Teams.AnyAsync(e => e.Name == "Test Team Title new"));
            Assert.That(await dbContext.Teams.AnyAsync(e => e.Description == "Test Team Description new one"));
        });
    }

    // Task EditTeam(EditTeamViewModel model, string teamId);
    [Test]
    public async Task EditTeamAsync_ValidEntry()

    {
        var teamToEdit = await teamService.GetTeamToEditAsync(testTeam.Id.ToString());


        teamToEdit!.Name = "Edited Team Name";
        teamToEdit.Description = "Edited Team Description need more chars";
        teamToEdit.EstablishedOn = DateTime.UtcNow.AddDays(10);
        teamToEdit.CountryId = "BD";

        await teamService.EditTeamAsync(teamToEdit, testTeam.Id.ToString());

        var teamId = testTeam.Id.ToString();

        Team editedTeam = await dbContext.Teams.FirstAsync(t => t.Id.ToString() == teamId);

        Assert.Multiple(() =>
        {
            Assert.That(editedTeam!.Name, Is.EqualTo("Edited Team Name"));
            Assert.That(editedTeam.Description, Is.EqualTo("Edited Team Description need more chars"));
            Assert.That(editedTeam.EstablishedOn.Value.Day, Is.EqualTo(DateTime.UtcNow.AddDays(10).Day));
        });
    }

    // Task DeleteTeam(string teamId);
    [Test]
    public async Task DeleteTeamAsync_ValidEntry()
    {
        await teamService.DeleteTeamAsync(testTeam.Id.ToString());

        var result = await teamService.GetAllTeamsAsync();

        Assert.That(result.Count, Is.EqualTo(0));
    }


    // Task<TeamDetailsViewModel?> GetTeamDetailsAsync(string teamId);
    [Test]
    public async Task GetEventViewModelByIdAsync_ValidEntry()
    {
        string testUserName= testUser.UserName;
        string testTeamId = testTeam.Id.ToString();
        string testTeamName = testTeam.Name;
        string testTeamDescription = testTeam.Description;


        var result = await teamService.GetTeamDetailsAsync(testTeamId);

        Assert.Multiple(() =>
        {
            Assert.That(result!.Name, Is.EqualTo(testTeamName));
            Assert.That(result.Description, Is.EqualTo(testTeamDescription));
            Assert.That(result.TeamManager, Is.EqualTo(testUserName));
        });
    }
    
    
    

    // Task<EditTeamViewModel?> GetTeamToEditAsync(string teamId);

    // Task<ICollection<AllTeamsViewModel>> GetAllTeams();

    //
    //

    //
    //
    // Task<int> GetTeamMembersCount(string teamId);
    //
    //
    // Task AddMemberAsync(string userId, string teamId);
    // Task RemoveMemberAsync(string userId, string teamId);
    //
    //
    //
    // Task<ICollection<TeamRequestViewModel>> GetTeamRequestsByUserAsync(string userId);
    //
    // Task SendRequest(string teamId, string userId);
    //
    // Task<bool> IsRequested(string userId, string teamId);
    //
    // Task<bool> IsMemberAsync(string userId, string teamId);
    //
    // Task RemoveRequest(string userId, string teamId);
    // Task<int> GetTeamsCountAsync();
    // Task<bool> IsManager(string teamId, string userId);
    // Task UploadPhotoToLocalStorageAsync(EditTeamViewModel model, string envWebRoot);
    // Task UploadPhotoToLocalStorageAsync(AddTeamViewModel model, string envWebRoot);
}