using static BikingBuddy.Common.ErrorMessages.CommentErrorMessages;
using static BikingBuddy.Common.NotificationMessagesConstants;

namespace BikingBuddy.Web.Controllers
{
    using Services.Contracts;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string commentBody, string eventId)
        {
            if (commentBody == null)
            {
                TempData[ErrorMessage] = CommentBoddyEmpty;
                return RedirectToAction("Details", "Event", new { eventId });
            }


            var userId = User.GetId();

            await commentService.AddComment(commentBody, userId, eventId);


            return RedirectToAction("Details", "Event", new { eventId });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string eventId, string body)
        {
            var userId = User.GetId();

            await commentService.AddComment(body, userId, eventId);


            return RedirectToAction("Details", "Event", new { eventId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int commentId, string eventId)
        {
            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = CommentDeleteError;
                return RedirectToAction("Details", "Event", new { eventId });

            }
            
            
            try
            {
                await commentService.DeleteComment(commentId);
                TempData[SuccessMessage] = CommentDeleteSuccessfully;
            }
            catch (Exception e)
            {
                TempData[ErrorMessage] = CommentDeleteError;
            }

            return RedirectToAction("Details", "Event", new { eventId });
        }
    }
}