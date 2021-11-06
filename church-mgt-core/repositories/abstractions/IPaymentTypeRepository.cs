using church_mgt_models;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.abstractions
{
    public interface IPaymentTypeRepository : IRepository<PaymentType>
    {
        Task<PaymentType> GetPaymentTypeByName(string paymentType);
        void UpdatePaymentType(PaymentType paymentType);
    }
}