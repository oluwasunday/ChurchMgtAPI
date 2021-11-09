using church_mgt_core.services.interfaces;
using church_mgt_dtos.ContactUsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace church_mgt_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;
        private readonly ILogger _logger;

        public ContactUsController(IContactUsService contactUsService, ILogger logger)
        {
            _contactUsService = contactUsService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetAllContactsMessage()
        {
            _logger.Information("Attempt to get all contact ");
            var result = await _contactUsService.GetAllContactsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{contactId}")]
        [Authorize (Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetContactMessage(string contactId)
        {
            _logger.Information("Attempt to get a contact message ");
            var result = await _contactUsService.GetAllContactsAsync(contactId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostContactMessage(AddContactDto contactDto)
        {
            _logger.Information("Attempt to post contact message");
            var result = await _contactUsService.AddContactAsync(contactDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{contactId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> DeleteContactMessage(string contactId)
        {
            _logger.Information("Attempt to delete contact message ");
            var result = await _contactUsService.DeletContactsAsync(contactId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
