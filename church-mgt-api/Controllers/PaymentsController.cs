using church_mgt_core.services.interfaces;
using church_mgt_dtos.PaymentDtos;
using church_mgt_dtos.PaymentTypeDtos;
using church_mgt_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using Serilog;
using System.Threading.Tasks;

namespace church_mgt_api.Controllers
{
    [ApiController]
    [Route ("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        private PayStackApi PayStack { get; set; }

        public PaymentsController(
            IPaymentTypeService paymentTypeService, 
            IPaymentService paymentService, 
            UserManager<AppUser> userManager,
            IConfiguration configuration,
            ILogger logger)
        {
            _paymentTypeService = paymentTypeService;
            _paymentService = paymentService;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            PayStack = new PayStackApi(_configuration["Payment:PaystackSK"]);
        }

        [HttpGet()]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetAllPayments()
        {
            _logger.Information("Attempt to get all payments");
            var result = await _paymentService.GetAllPaymentsAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{paymentId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetPayments(string paymentId)
        {
            _logger.Information($"Attempt to get payment for {paymentId}");
            var result = await _paymentService.GetPaymentByIdAsync(paymentId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> MakePayment(MakePaymentDto payment)
        {
            _logger.Information($"Attempt to make payment for {payment.Email} by {payment.PaymentType}");
            var result = await _paymentService.MakePaymentAsync(payment);

            if (result.Succeeded)
                return StatusCode(result.StatusCode, result.Data.Data);

            return StatusCode(result.StatusCode, result.Data.Message);            
        }

        [HttpGet("VerifyPayment")]
        public async Task<IActionResult> Verify(string trxref, string reference)
        {
            _logger.Information($"Attempt to verify payment for reference {reference}");
            var result = await _paymentService.VerifyPaymentAsync(reference);
            return Redirect(_configuration["VerifyRedirectUrl"]);
        }



        // ======================== PaymentType Controller======================
        [HttpGet("AllPaymentTypes")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public IActionResult GetAllPaymentType()
        {
            _logger.Information("Attempt to get all payment types");
            var result = _paymentTypeService.GetAllPaymentType();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("PaymentTypes/{paymentTypeId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> GetPaymentType(string paymentTypeId)
        {
            _logger.Information($"Attempt to get payment type for {paymentTypeId}");
            var result = await _paymentTypeService.GetPaymentTypeById(paymentTypeId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("PaymentTypes")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> PaymentType(AddPaymentTypeDto paymentTypeDto)
        {
            _logger.Information($"Attempt to add payment type {paymentTypeDto.TypeOfPayment}");
            var result = await _paymentTypeService.AddPaymentType(paymentTypeDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("PaymentTypes/{paymentTypeId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> DeletePaymentType(string paymentTypeId)
        {
            _logger.Information($"Attempt to delete payment type for {paymentTypeId}");
            var result = await _paymentTypeService.DeletePaymentType(paymentTypeId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("PaymentTypes/{paymentTypeId}")]
        [Authorize(Roles = "Admin, Pastor, SuperPastor")]
        public async Task<IActionResult> UpdatePaymentType(string paymentTypeId)
        {
            _logger.Information($"Attempt to update payment type for {paymentTypeId}");
            var result = await _paymentTypeService.UpdatePaymentType(paymentTypeId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
