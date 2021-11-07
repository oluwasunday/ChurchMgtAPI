﻿using church_mgt_core.services.interfaces;
using church_mgt_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public CommentsController(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddComment(string comment)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _commentService.AddCommentAsync(user.Id, comment);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, PAstor, SuperPastor")]
        public IActionResult GetComments()
        {
            var result = _commentService.GetAllComments();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{commentId}")]
        [Authorize (Roles = "Admin, PAstor, SuperPastor")]
        public async Task<IActionResult> GetComment(string commentId)
        {
            var result = await _commentService.GetCommentById(commentId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = "Admin, PAstor, SuperPastor")]
        public async Task<IActionResult> DeleteComment(string commentId)
        {
            var result = await _commentService.DeleteCommentById(commentId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
