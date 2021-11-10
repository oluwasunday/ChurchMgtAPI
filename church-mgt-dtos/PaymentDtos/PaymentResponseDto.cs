using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.PaymentDtos
{
    public class PaymentResponseDto
    {
        public string Id { get; set; }
        public string PaymentReference { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string PaymentType { get; set; }
        public string FullName { get; set; }
    }
}
