using System.ComponentModel.DataAnnotations;

namespace church_mgt_dtos.PaymentDtos
{
    public class MakePaymentDto
    {
        [Required]
        [Range(1.0, double.MaxValue, ErrorMessage = "Enter valid amount")]
        public decimal Amount { get; set; }
        public string PaymentReference { get; set; }
        public string PaymentTypeId { get; set; }
        public string Email { get; set; }
    }
}
