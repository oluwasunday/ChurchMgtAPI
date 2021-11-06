using church_mgt_core.repositories.abstractions;
using church_mgt_database;
using church_mgt_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.implementations
{
    public class PaymentTypeRepository : Repositories<PaymentType>, IPaymentTypeRepository
    {
        private readonly ChurchDbContext _context;
        private readonly DbSet<PaymentType> _dbSet;
        public PaymentTypeRepository(ChurchDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PaymentType>();
        }

        public void UpdatePaymentType(PaymentType paymentType)
        {
            _dbSet.Update(paymentType);
        }

        public async Task<PaymentType> GetPaymentTypeByName(string paymentType)
        {
            var pType = await _dbSet.Where(x => x.TypeOfPayment.ToLower() == paymentType.Trim().ToLower()).FirstOrDefaultAsync();
            return pType;
        }
    }
}
