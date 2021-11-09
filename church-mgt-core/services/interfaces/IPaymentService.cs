using church_mgt_dtos.Dtos;
using church_mgt_dtos.PaymentDtos;
using church_mgt_models;
using PayStack.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IPaymentService
    {
        Task<Response<IEnumerable<PaymentResponseDto>>> GetAllPaymentsAsync();
        Task<Response<PaymentResponseDto>> GetPaymentByIdAsync(string paymentId);
        Task<Response<TransactionInitializeResponse>> MakePaymentAsync(MakePaymentDto payment);
        Task<Payment> PaymentByPaymentReferenceAsync(string reference);
        Task<Response<string>> UpdatePaymentAsync(Payment payment);
        Task<Response<string>> VerifyPaymentAsync(string reference);
    }
}