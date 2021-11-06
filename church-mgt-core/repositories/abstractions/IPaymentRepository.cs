using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.abstractions
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(string paymentId);
        Task AddPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByReference(string reference);
        Task UpdatePayment(Payment payment);
    }
}