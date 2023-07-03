using BikingBuddy.Data.Models;
using BikingBuddy.Services;
using BikingBuddy.Services.Contracts;
using BikingBuddy.Web.Infrastructure.Extensions;
using BikingBuddy.Web.Models.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace BikingBuddy.Web.Controllers
{
    public class CommentController : Controller
    {

        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            this.commentService = _commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string commentBody ,string eventId)
        {
            var userId = User.GetId();

            await commentService.AddComment(commentBody, userId,eventId);


            return RedirectToAction("Details", "Event", new {eventId});
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string eventId, string body)
        {
            var userId = User.GetId();

            await commentService.AddComment(body, userId, eventId);


            return RedirectToAction("Details", "Event", new { eventId });
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(CommentViewModel model)
        //{
        //    var userId = User.GetId();

        //  // await commentService.AddComment(body, userId, eventId);


        //   // return RedirectToAction("Details", "Event", new { eventId });
        //}
    }
}
