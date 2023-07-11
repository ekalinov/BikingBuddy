﻿namespace BikingBuddy.Web.Controllers
{
    using Services.Contracts;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : BaseController
    {

        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            this.commentService = _commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(string commentBody, string eventId)
        {
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

        //[HttpGet]
        //public async Task<IActionResult> Edit(CommentViewModel model)
        //{
        //    var userId = User.GetId();

        //  // await commentService.AddComment(body, userId, eventId);


        //   // return RedirectToAction("Details", "Event", new { eventId });
        //}
    }
}
