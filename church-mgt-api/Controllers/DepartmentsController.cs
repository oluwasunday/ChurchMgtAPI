using church_mgt_core.services.interfaces;
using church_mgt_dtos.DepartmentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace church_mgt_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;
        public DepartmentsController(IDepartmentService departmentService, ILogger logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult Departments()
        {
            _logger.Information("Attempt to get departments");
            var result = _departmentService.GetAllDepartments();

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{departmentId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetDepartment(string departmentId)
        {
            _logger.Information($"Attempt to get departments for {departmentId}");
            var result = await _departmentService.GetDepartmentById(departmentId);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("byuserid/{userId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult GetDepartmentsByUserId(string userId)
        {
            _logger.Information($"Attempt to get departmentsby user id {userId}");
            var result = _departmentService.DepartmentsByUserId(userId);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("membersin/{departmentId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetMembersInDepartments(string departmentId)
        {
            _logger.Information($"Attempt to get members in department for {departmentId}");
            var result =await _departmentService.GetMembersInDepartmentAsync(departmentId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> AddDepartment(AddDepartmentDto model)
        {
            _logger.Information($"Attempt to add department for {model.Name}");
            var result = await _departmentService.CreateDepartmentAsync(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{departmentId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> UpdateDepartment(string departmentId, [FromBody]AddDepartmentDto model)
        {
            _logger.Information($"Attempt to update department {model.Name} with id {departmentId}");
            var result = await _departmentService.UpdateDepartment(departmentId, model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{departmentId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> DeleteDepartment(string departmentId)
        {
            _logger.Information($"Attempt to delete department for {departmentId}");
            var result = await _departmentService.DeleteDepartmentByIdAsync(departmentId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{userId}/addto-department")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor, Member")]
        public async Task<IActionResult> AddMemberToDepartment(string userId, [FromForm]string departmentId)
        {
            _logger.Information($"Attempt to add member {userId} to department {departmentId}");
            var result = await _departmentService.AddMemberToDepartment(userId, departmentId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
