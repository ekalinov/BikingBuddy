using System.Security.Claims;
using static BikingBuddy.Common.NotificationMessagesConstants;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Models.Event;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
 
using BikingBuddy.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq; 

namespace BikingBuddy.Tests.Controllers;


[TestFixture]
public class EventControllerTests
{
    private EventController _controller;
    private Mock<IEventService> _eventServiceMock;
    private Mock<ICommentService> _commentServiceMock;
    private Mock<IWebHostEnvironment> _webHostEnvironmentMock;

    [SetUp]
    public void Setup()
    {
        _eventServiceMock = new Mock<IEventService>();
        _commentServiceMock = new Mock<ICommentService>();
        _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
        
        _controller = new EventController(_eventServiceMock.Object, _commentServiceMock.Object, _webHostEnvironmentMock.Object);
        
        
        // Set up mock HttpContext for User.GetId()
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "userId123")  
        };

        var identity = new ClaimsIdentity(claims, "TestAuthentication");
        var principal = new ClaimsPrincipal(identity);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = principal
            }
        };
        
        // Assign the TempData to the controller
        _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

      
    }

    [Test]
    public async Task Details_EventExists_ReturnsViewWithEventDetails()
    {
        // Arrange
        var eventId = "eventId123";
        var eventDetails = new EventDetailsViewModel();
        _eventServiceMock.Setup(service => service.GetEventDetailsByIdAsync(eventId)).ReturnsAsync(eventDetails);
     
        // Act
        var result = await _controller.Details(eventId) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(eventDetails, result.Model);
    }

    [Test]
    public async Task Details_EventNotExists_RedirectsToAllAction()
    {
        // Arrange
        var eventId = "nonExistentEventId";
        _eventServiceMock.Setup(service => service.GetEventDetailsByIdAsync(eventId)).ReturnsAsync((EventDetailsViewModel)null);
        
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

        // Assign the TempData to the controller
        _controller.TempData = tempData;

        // Act
        var result = await _controller.Details(eventId) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual("All", result.ActionName);
    }

    
    
    [Test]
    public async Task Add_InvalidModel_ReturnsViewWithModel()
    {
        // Arrange
        var invalidModel = new AddEventViewModel();
        _controller.ModelState.AddModelError("someField", "some error");

        // Act
        var result = await _controller.Add(invalidModel) as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(invalidModel, result.Model);
    }

    [Test]
    public async Task Add_ValidModel_RedirectsToAllAction()
    {
        // Arrange
        var validModel = new AddEventViewModel();
        _eventServiceMock.Setup(service => service.AddEventAsync(validModel, It.IsAny<string>())).Verifiable();

        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

        // Assign the TempData to the controller
        _controller.TempData = tempData;
        
        // Act
        var result = await _controller.Add(validModel) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual("All", result.ActionName);
        Assert.AreEqual("Event", result.ControllerName);
        _eventServiceMock.Verify();
    }
    
    
     [Test]
        public async Task Edit_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var invalidModel = new EditEventViewModel();
            _controller.ModelState.AddModelError("someField", "some error");

            // Act
            var result = await _controller.Edit(invalidModel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(invalidModel, result.Model);
        }

       
       
        [Test]
        public async Task Delete_ValidEventId_RedirectsToAllAction()
        {
            // Arrange
            var validModel = new EditEventViewModel
            {
                EventImage = new FormFile(null, 0, 0, "image.jpg", "image.jpg"),
                EventId = "eventId123"
            }; 
            _eventServiceMock.Setup(service => service.DeleteEventAsync(validModel.EventId)).Verifiable();
            
    
            // Set up IsOrganiser to return true
            _eventServiceMock.Setup(service => service.IsOrganiser(validModel.EventId, It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(validModel.EventId, null) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("All", result.ActionName);
            Assert.AreEqual("Event", result.ControllerName);
            _eventServiceMock.Verify();
        }

}