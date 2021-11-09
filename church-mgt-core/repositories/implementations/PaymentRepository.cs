using church_mgt_core.repositories.abstractions;
using church_mgt_database;
using church_mgt_models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ChurchDbContext _context;

        public PaymentRepository(ChurchDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(string paymentId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.Id == paymentId);
        }

        public async Task<Payment> GetPaymentByReference(string reference)
        {
            var payment = await _context.Payments
                .Where(x => x.PaymentReference == reference)
                .FirstOrDefaultAsync();

            return payment;
        }

        public async Task UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentByPaymentType(string paymentType)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.PaymentType == paymentType);
        }
    }
}
