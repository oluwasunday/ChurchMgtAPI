using church_mgt_core.services.interfaces;
using church_mgt_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace church_mgt_api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;

        public CommentsController(ICommentService commentService, UserManager<AppUser> userManager, ILogger logger)
        {
            _commentService = commentService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddComment(string comment)
        {
            var userInfo = await _userManager.GetUserAsync(User);
            _logger.Information($"Adding comment for {userInfo.Id}");

            var user = await _userManager.GetUserAsync(User);
            var result = await _commentService.AddCommentAsync(user.Id, comment);

            _logger.Information($"Adding comment for {userInfo.Id}");
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult GetComments()
        {
            _logger.Information("Getting all comments");
            var result = _commentService.GetAllComments();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{commentId}")]
        [Authorize (Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetComment(string commentId)
        {
            _logger.Information($"Getting comment for {commentId}");
            var result = await _commentService.GetCommentById(commentId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            _logger.Information($"Delete comment attempt for {commentId}");
            var result = await _commentService.DeleteCommentById(commentId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
