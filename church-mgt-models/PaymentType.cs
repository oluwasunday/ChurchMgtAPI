using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class PaymentType
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TypeOfPayment { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
