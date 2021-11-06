using AutoMapper;
using church_mgt_core.repositories.abstractions;
using church_mgt_core.services.interfaces;
using church_mgt_dtos.Dtos;
using church_mgt_dtos.PaymentDtos;
using church_mgt_models;
using hotel_booking_utilities;
using Microsoft.Extensions.Configuration;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private PayStackApi PayStack { get; set; }
         
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _configuration = configuration;
            PayStack = new PayStackApi(_configuration["Payment:PaystackSK"]);
        }

        public async Task<Response<TransactionInitializeResponse>> MakePaymentAsync(string userId, MakePaymentDto payment)
        {
            var pay = _mapper.Map<Payment>(payment);
            pay.AppUserId = userId;
            pay.PaymentReference = $"{ReferenceGenerator.GetInitials()}-{ReferenceGenerator.Generate()}";

            TransactionInitializeRequest trxRequest = new()
            {
                AmountInKobo = (int)pay.Amount * 100,
                Email = pay.Email,
                Reference = pay.PaymentReference,
                Currency = "NGN",
                CallbackUrl = $"{_configuration["BaseUrl"]}api/Payments/VerifyPayment"//?reference={pay.PaymentReference}"
            };

            TransactionInitializeResponse trxResponse = PayStack.Transactions.Initialize(trxRequest);
            if (trxResponse.Status)
            {
                await _paymentRepository.AddPaymentAsync(pay);
                return Response<TransactionInitializeResponse>.Success("Successful", trxResponse);
            }

            return Response<TransactionInitializeResponse>.Fail($"Something went wrong {trxResponse.Status} - {trxResponse.Message}");
        }

        public async Task<Response<IEnumerable<PaymentResponseDto>>> GetAllPaymentsAsync()
        {
            var payment = await _paymentRepository.GetAllPaymentsAsync();

            var response = _mapper.Map<IEnumerable<PaymentResponseDto>>(payment);
            var responseMsg = payment.ToList().Count <= 0 ? "No payment data found" : "Success";
            return Response<IEnumerable<PaymentResponseDto>>.Success(responseMsg, response);
        }

        public async Task<Response<PaymentResponseDto>> GetPaymentByIdAsync(string paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null)
                return Response<PaymentResponseDto>.Fail($"Payment with id {payment.Id} not found");

            var response = _mapper.Map<PaymentResponseDto>(payment);
            return Response<PaymentResponseDto>.Success("Success", response);
        }

        public async Task<Response<string>> VerifyPaymentAsync(string reference)
        {
            TransactionVerifyResponse response = PayStack.Transactions.Verify(reference);
            
            if (response.Data.Status == "success")
            {
                var payment = await _paymentRepository.GetPaymentByReference(reference);
                payment.Status = true;

                await _paymentRepository.UpdatePayment(payment);
                return Response<string>.Success("Success", $"Transaction reference - {payment.PaymentReference}");
            }

            return Response<string>.Fail(response.Data.GatewayResponse);
        }



        public async Task<Payment> PaymentByPaymentReferenceAsync(string reference)
        {
            var payment = await _paymentRepository.GetPaymentByReference(reference);
            return payment;
        }

        public async Task<Response<string>> UpdatePaymentAsync(Payment payment)
        {
            if (payment == null)
                return Response<string>.Fail("Transaction not exist");

            payment.Status = true;

            await _paymentRepository.UpdatePayment(payment);
            return Response<string>.Success("Success", $"Payment reference {payment.PaymentReference}");
        }
    }
}
