namespace BikingBuddy.Tests.Services;

using System.Globalization;

using Microsoft.EntityFrameworkCore;

using Data;
using Data.Models;

using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;

using Web.Models.Event;
using Web.Models.Event.Enums;



using static DbSeeder;

[TestFixture]
public class EventServiceTest
{
    private BikingBuddyDbContext dbContext = null!;
    private IEventService eventService = null!;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<BikingBuddyDbContext>()
            .UseInMemoryDatabase(databaseName: "BikingBuddyDbContextInMemory" + Guid.NewGuid() )
            .Options;

        dbContext = new BikingBuddyDbContext(options);
        
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        await SeedDatabase(dbContext);

        eventService = new EventService(dbContext);
    }

    [Test]
    public async Task AddEventAsync_ValidEntry()
    {
        var testEventModel = new AddEventViewModel
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

        string testUserId = testUser.Id.ToString();

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
        string testEventId = testEvent.Id.ToString();
    
        await eventService.DeleteEventAsync(testEventId);
    
        var events = await eventService.GetAllEventsAsync();
    
        Assert.That(events.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task JoinEventAsync_ValidEntry()
    {
        Assert.That(await dbContext.EventsParticipants.CountAsync(), Is.EqualTo(0));

        string testUserId = testUser.Id.ToString();
        string testEventId = testEvent.Id.ToString();

        await eventService.JoinEventAsync(testUserId, testEventId);
        Assert.Multiple(async () =>
        {
            Assert.That(await dbContext.EventsParticipants.CountAsync(), Is.EqualTo(1));
            Assert.That(await dbContext.EventsParticipants.AnyAsync(ep => ep.EventId == Guid.Parse(testEventId)));
            Assert.That(await dbContext.EventsParticipants.AnyAsync(ep => ep.ParticipantId == Guid.Parse(testUserId)));
        });
        await eventService.LeaveEventAsync(testUserId, testEventId);


        Assert.That(await dbContext.EventsParticipants.CountAsync(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetEventViewModelByIdAsync_ValidEntry()
    {
        string testUserId = testUser.Id.ToString();
        string testEventId = testEvent.Id.ToString();
        string testEventTitle = testEvent.Title;
        string testEventDescription = testEvent.Description;


        var testModel = await eventService.GetEventViewModelByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(testModel!.Title, Is.EqualTo(testEventTitle));
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
            Assert.That(activityTypes.Count, Is.EqualTo(9));
            Assert.That(activityTypes.Any(model => model.ActivityTypeName == "Mountain Biking"));
            Assert.That(activityTypes.Any(model => model.ActivityTypeName == "Road Cycling"));
        });
    }

    [Test]
    public async Task EditEventAsync_ValidEntry()
    
    {
        string testEventId = testEvent.Id.ToString();

        var eventToEdit = await eventService.GetEventViewModelByIdAsync(testEventId);

        eventToEdit.EventId = testEventId;
        eventToEdit!.Title = "Edited Title";
        eventToEdit.Distance = 200;
        eventToEdit.Ascent = 250;
        eventToEdit.Description = "Edited testEventDescription description";


        await eventService.EditEventAsync(eventToEdit);


        Event? editedEvent = await eventService.GetEventByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(editedEvent!.Title, Is.EqualTo("Edited Title"));
            Assert.That(editedEvent.Description, Is.EqualTo("Edited testEventDescription description"));
            Assert.That(editedEvent.Distance, Is.EqualTo(200));
            Assert.That(editedEvent.Ascent, Is.EqualTo(250));
        });
    }


    [Test]
    public async Task AllAsync_ValidEntry()
    {
        var queryModel = new AllEventsQueryModel
        {
            ActivityType = "MTBiking",
            SearchTerm = "Running",
            Sorting = EventSorting.Newest,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };

        var result = await eventService.AllEventsAsync(queryModel);

        Assert.That(result.AllEvents, Is.Empty);

        var queryModel1 = new AllEventsQueryModel
        {
            ActivityType ="Mountain Biking",
            SearchTerm = "",
            Sorting = EventSorting.Newest,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };

        result = await eventService.AllEventsAsync(queryModel1);

        Assert.That(result.AllEvents, Has.Count.EqualTo(1));

        var queryModel2 = new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Biking",
            Sorting = EventSorting.MostParticipants,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };

        result = await eventService.AllEventsAsync(queryModel2);

        Assert.That(result.AllEvents, Has.Count.EqualTo(1));
        var queryModel3= new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Biking",
            Sorting = EventSorting.ThisMonth,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        result = await eventService.AllEventsAsync(queryModel3);
        
        Assert.That(result.AllEvents, Has.Count.EqualTo(1));
        
        var queryModel4= new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Biking",
            Sorting = EventSorting.ThisWeek,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        result = await eventService.AllEventsAsync(queryModel4);
        
        Assert.That(result.AllEvents, Has.Count.EqualTo(0));
    }

    [Test]
    public async Task MineAsync_ValidEntry()
    { 

        string testUserId = testUser.Id.ToString();
        string testEventId = testEvent.Id.ToString();

        await eventService.JoinEventAsync(testUserId, testEventId);
        
        
        var queryModel = new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Running",
            Sorting = EventSorting.Newest,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        var result = await eventService.MineAsync(queryModel,testUserId);
        
        Assert.That(result.AllEvents, Is.Empty);
        
        var queryModel1 = new AllEventsQueryModel
        {
            ActivityType ="Mountain Biking",
            SearchTerm = "",
            Sorting = EventSorting.Newest,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        result = await eventService.MineAsync(queryModel1,testUserId);
        
        Assert.That(result.AllEvents, Has.Count.EqualTo(1));
        
        var queryModel2 = new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Biking",
            Sorting = EventSorting.MostParticipants,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        result = await eventService.MineAsync(queryModel2,testUserId);
        
        Assert.That(result.AllEvents, Has.Count.EqualTo(1));
        
        var queryModel3= new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Biking",
            Sorting = EventSorting.ThisMonth,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        result = await eventService.MineAsync(queryModel3,testUserId);
        
        Assert.That(result.AllEvents, Has.Count.EqualTo(1));
        
        var queryModel4= new AllEventsQueryModel
        {
            ActivityType = "Mountain Biking",
            SearchTerm = "Biking",
            Sorting = EventSorting.ThisWeek,
            CurrentPage = 0,
            EventsPerPage = 1,
            TotalEventsCount = 1,
            ActivityTypes = await eventService.GetActivityTypesAsync(),
            Events = await eventService.GetAllEventsAsync()
        };
        
        result = await eventService.MineAsync(queryModel4,testUserId);
        
        Assert.That(result.AllEvents, Has.Count.EqualTo(0));
    }

    [Test]
    public async Task GetEventDetailsByIdAsync_ValidEntry()
    {
        string testUserName = testUser.Name;
        string testEventId = testEvent.Id.ToString();
        string testEventTitle = testEvent.Title;
        string testEventDescription = testEvent.Description;


        var testModel = await eventService.GetEventDetailsByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(testModel!.Title, Is.EqualTo(testEventTitle));
            Assert.That(testModel.Description, Is.EqualTo(testEventDescription));
            Assert.That(testModel.OrganizerName, Is.EqualTo(testUserName));
        });
    }

    [Test]
    public async Task GetEventByIdAsync_ValidEntry()
    {
        string testEventId = testEvent.Id.ToString();
        string testEventTitle = testEvent.Title;
        string testEventDescription = testEvent.Description;


        Event? result = await eventService.GetEventByIdAsync(testEventId);

        Assert.Multiple(() =>
        {
            Assert.That(result!.Title, Is.EqualTo(testEventTitle));
            Assert.That(result.Description, Is.EqualTo(testEventDescription));
            Assert.That(result.Id, Is.EqualTo(Guid.Parse(testEventId)));
        });
    }
}