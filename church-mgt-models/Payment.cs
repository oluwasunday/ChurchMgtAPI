using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class Payment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PaymentReference { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Email { get; set; }
        public string PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
