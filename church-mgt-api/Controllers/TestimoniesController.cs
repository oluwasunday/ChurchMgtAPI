﻿using church_mgt_core.services.interfaces;
using church_mgt_dtos.TestimonyDtos;
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
    [Route("api/[controller]")]
    public class TestimoniesController : ControllerBase
    {
        private readonly ITestimonyService _testimonyService;
        private readonly UserManager<AppUser> _userManager;

        public TestimoniesController(ITestimonyService testimonyService, UserManager<AppUser> userManager)
        {
            _testimonyService = testimonyService;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddTestimony(AddTestimonyDto testimony)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _testimonyService.AddTestimony(user.Id, testimony);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult Testimonies()
        {
            var result = _testimonyService.GetTestimonies();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{testimonyId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> Testimonies(string testimonyId)
        {
            var result = await _testimonyService.GetTestimonyById(testimonyId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{testimonyId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> Testimony(string testimonyId)
        {
            var result = await _testimonyService.DeleteTestimonyById(testimonyId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
