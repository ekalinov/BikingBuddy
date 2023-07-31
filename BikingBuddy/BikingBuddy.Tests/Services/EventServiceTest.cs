using System.Globalization;
using BikingBuddy.Common;
using BikingBuddy.Data;
using BikingBuddy.Data.Models;
using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Services.Data.Models.Events;
using BikingBuddy.Web.Models;
using BikingBuddy.Web.Models.Activity;
using BikingBuddy.Web.Models.Event;
using BikingBuddy.Web.Models.Event.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace BikingBuddy.Tests.Services;

[TestFixture]
public class EventServiceTest
{
    private BikingBuddyDbContext dbContext;
    private IEventService eventService;

    private AddEventViewModel testEventModel;


    private ICollection<ActivityTypeViewModel> testActivityTypesModels;

    private string? testEventId;
    private string testEventTitle;
    private string testEventDescription;

    private string? testUserId;
    private string testUserName;

    [SetUp]
    public async Task Setup()
    {
        Country testCountry1 = new()
        {
            Code = "BG",
            Name = "Bulgaria"
        };

        Country testCountry2 = new()
        {
            Code = "USA",
            Name = "USA"
        };

        var testCountries = new HashSet<Country>
        {
            testCountry1,
            testCountry2
        };

        Town testTown = new Town
        {
            Id = 1,
            Name = "Sofia"
        };

        testUserId = "df666d9f-4332-45ea-adae-36aba1f83289";

        ActivityType testActivityType1 = new()
        {
            Id = 1,
            Name = "MTBiking"
        };
        ActivityType testActivityType2 = new()
        {
            Id = 2,
            Name = "Road Biking"
        };


        var testActivityTypes = new HashSet<ActivityType>()
        {
            testActivityType1,
            testActivityType2
        };


        ActivityTypeViewModel testActivityTypeModel1 = new()
        {
            Id = 1,
            ActivityTypeName = "MTBiking"
        };
        ActivityTypeViewModel testActivityTypeModel2 = new()
        {
            Id = 2,
            ActivityTypeName = "Road Biking"
        };


        testActivityTypesModels = new HashSet<ActivityTypeViewModel>()
        {
            testActivityTypeModel1,
            testActivityTypeModel2
        };

        testUserName = "Test Testov";

        AppUser testUser = new()
        {
            Id = Guid.Parse(testUserId),
            Name = testUserName,
            Country = new Country { Name = "Bangladesh", Code = "BD" },
            Town = testTown
        };

        testEventTitle = "Test Event";
        testEventDescription = "Test Event Description";
        testEventId = "cd0a1105-ba73-4da7-8769-f24c7a22b4b7";

        Event testEvent = new()
        {
            Id = Guid.Parse(testEventId),
            Title = testEventTitle,
            CreatedOn = DateTime.UtcNow,
            ActivityType = testActivityType1,
            Description = testEventDescription,
            OrganizerId = Guid.Parse(testUserId),
            Organizer = testUser,
            Country = testCountry1,
            CountryId = testCountry1.Code,
            Town = testTown,
            TownId = 1,
            IsDeleted = false
        };


        var options = new DbContextOptionsBuilder<BikingBuddyDbContext>()
            .UseInMemoryDatabase(databaseName: "BikingBuddyDbContextInMemory")
            .Options;

        dbContext = new BikingBuddyDbContext(options);

        await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Events.AddAsync(testEvent);
        await dbContext.AppUsers.AddAsync(testUser);
        await dbContext.ActivityTypes.AddRangeAsync(testActivityTypes);
        await dbContext.Countries.AddRangeAsync(testCountries);


        await dbContext.SaveChangesAsync();

        eventService = new EventService(dbContext);
    }

    [Test]
    public async Task AddEventAsync_ValidEntry()
    {
        testEventModel = new AddEventViewModel
        {
            Title = "testEventTitle2",
            Date = DateTime.UtcNow.AddDays(10).ToString(CultureInfo.InvariantCulture),
            Distance = 100,
            Ascent = 220,
            Description = "testEventDescription description",
            EventImageUrl = "https://via.placeholder.com/150/0000FF/808080 ?Text=PAKAINFO.com",
            ActivityTypes = testActivityTypesModels,
            ActivityTypeId = 1,
            CountriesCollection = await eventService.GetCountriesAsync(),
            CountryId = "BG",
            TownName = "Sofia"
        };

        await eventService.AddEventAsync(testEventModel, testUserId);

        Assert.Multiple(async () =>
        {
            Assert.That(await dbContext.Events.CountAsync(), Is.EqualTo(2));
            Assert.That(await dbContext.Events.AnyAsync(e => e.Title == "testEventTitle2"));
            Assert.That(await dbContext.Events.AnyAsync(e => e.Description == "testEventDescription description"));
        });
    }

    [Test]
    public async Task DeleteEventAsync_ValidEntry()
    {
        await eventService.DeleteEventAsync(testEventId);

        var events = await eventService.GetAllEventsAsync();

        Assert.That(events.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task JoinEventAsync_ValidEntry()
    {
        Assert.That(await dbContext.EventsParticipants.CountAsync(), Is.EqualTo(0));

        await eventService.JoinEventAsync(testUserId, testEventId);

        Assert.That(await dbContext.EventsParticipants.CountAsync(), Is.EqualTo(1));
        Assert.That(await dbContext.EventsParticipants.AnyAsync(ep => ep.EventId == Guid.Parse(testEventId)));
        Assert.That(await dbContext.EventsParticipants.AnyAsync(ep => ep.ParticipantId == Guid.Parse(testUserId)));

        await eventService.LeaveEventAsync(testUserId, testEventId);


        Assert.That(await dbContext.EventsParticipants.CountAsync(), Is.EqualTo(0));
    }


    [Test]
    public async Task GetEventViewModelByIdAsync_ValidEntry()
    {
        var testModel = await eventService.GetEventViewModelByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(testModel.Title, Is.EqualTo(testEventTitle));
            Assert.That(testModel.Description, Is.EqualTo(testEventDescription));
            Assert.That(testModel.OrganiserId, Is.EqualTo(testUserId));
        });
    }

    [Test]
    public async Task GetAllEventsCountAsync_ValidEntry()
    {
        var eventsCount = await eventService.GetAllEventsCountAsync();

        Assert.That(eventsCount, Is.EqualTo(1));
    }

    [Test]
    public async Task GetActivityTypesAsync_ValidEntry()
    {
        var activityTypes = await eventService.GetActivityTypesAsync();

        Assert.Multiple(() =>
        {
            Assert.That(activityTypes.Count, Is.EqualTo(2));
            Assert.That(activityTypes.Any(model =>model.ActivityTypeName=="MTBiking" ));
            Assert.That(activityTypes.Any(model =>model.ActivityTypeName=="Road Biking" ));
        });
    }

    [Test]
    public async Task EditEventAsync_ValidEntry()
    {
        var eventToEdit = await eventService.GetEventViewModelByIdAsync(testEventId);

        eventToEdit.Title = "Edited Title";
        eventToEdit.Distance = 200;
        eventToEdit.Ascent = 250;
        eventToEdit.Description = "Edited testEventDescription description";


        await eventService.EditEventAsync(eventToEdit, testEventId);


        var editedEvent = await eventService.GetEventByIdAsync(testEventId);

        Assert.Multiple(async () =>
        {
            Assert.That(editedEvent.Title, Is.EqualTo("Edited Title"));
            Assert.That(editedEvent.Description, Is.EqualTo("Edited testEventDescription description"));
            Assert.That(editedEvent.Distance, Is.EqualTo(200));
            Assert.That(editedEvent.Ascent, Is.EqualTo(250));
        });
    }


    [Test]
    public async Task AllAsync_ValidEntry()
    {
        AllEventsFilteredAndPagedServiceModel all;
        
        var queryModel = new AllEventsQueryModel
        {
            ActivityType = "MTBiking",
            SearchTerm = "asdsadsa",
            Sorting = EventSorting.Newest,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };

        all =  await eventService.AllAsync(queryModel);

       Assert.That(all.AllEvents.Count, Is.EqualTo(0));

       var queryModel1 = new AllEventsQueryModel
       {
           ActivityType = "MTBiking",
           SearchTerm = "Biking",
           Sorting = EventSorting.Newest,
           CurrentPage = 0,
           EventsPerPage = 1,
           TotalEventsCount = 1,
           ActivityTypes = await eventService.GetActivityTypesAsync(),
           Events = await eventService.GetAllEventsAsync()
       };

        all =  await eventService.AllAsync(queryModel1);

       Assert.That(all.AllEvents.Count, Is.EqualTo(1));



    }

    [Test]
    public async Task GetEventDetailsByIdAsync_ValidEntry()
    {
        var testModel = await eventService.GetEventDetailsByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(testModel.Title, Is.EqualTo(testEventTitle));
            Assert.That(testModel.Description, Is.EqualTo(testEventDescription));
            Assert.That(testModel.OrganizerName, Is.EqualTo(testUserName));
        });
    }

    [Test]
    public async Task GetEventByIdAsync_ValidEntry()
    {
        var testEvent = await eventService.GetEventByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(testEvent.Title, Is.EqualTo(testEventTitle));
            Assert.That(testEvent.Description, Is.EqualTo(testEventDescription));
            Assert.That(testEvent.Id, Is.EqualTo(Guid.Parse(testEventId)));
        });
    }
}