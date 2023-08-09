namespace BikingBuddy.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Extensions;
    using Services.Contracts;
    using static Common.ErrorMessages.CommentErrorMessages;
    using static Common.NotificationMessagesConstants;

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
            if (string.IsNullOrEmpty(commentBody))
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
                TempData[ErrorMessage] = UnauthorisedDelete;
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