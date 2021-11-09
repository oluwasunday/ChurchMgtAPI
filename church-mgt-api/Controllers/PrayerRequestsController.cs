using church_mgt_core.services.implementations;
using church_mgt_core.services.interfaces;
using church_mgt_dtos.PrayerRequestDtos;
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
    public class PrayerRequestsController : ControllerBase
    {
        private readonly IPrayerRequestService _prayerRequestService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;

        public PrayerRequestsController(IPrayerRequestService prayerRequestService, UserManager<AppUser> userManager, ILogger logger)
        {
            _prayerRequestService = prayerRequestService;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult PrayerRequests()
        {
            _logger.Information("Attempt to get all prayer requests");
            var result = _prayerRequestService.GetAllPrayerRequests();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{prayerRequestId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> PrayerRequestById(string prayerRequestId)
        {
            _logger.Information($"Attempt to get prayer requests for {prayerRequestId}");
            var result = await _prayerRequestService.GetPrayerRequestByIdAsync(prayerRequestId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddPrayerRequest(AddPrayerRequestDto prayerRequestDto)
        {
            _logger.Information($"Attempt to prayer request {prayerRequestDto.Request}");
            var user = await _userManager.GetUserAsync(User);
            var result = await _prayerRequestService.AddPrayerRequest(user.Id, prayerRequestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("memberId")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult PrayerRequestsByMemberId(string memberId)
        {
            _logger.Information($"Attempt to get prayer requests for member {memberId}");
            var result = _prayerRequestService.GetPrayerRequestsByMemberId(memberId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{prayerRequestId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> DeletePrayerRequestsById(string prayerRequestId)
        {
            _logger.Information($"Attempt to delete prayer request for {prayerRequestId}");
            var result = await _prayerRequestService.DeletePrayerRequestsById(prayerRequestId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
