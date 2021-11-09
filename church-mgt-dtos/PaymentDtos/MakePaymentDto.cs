using System.ComponentModel.DataAnnotations;

namespace church_mgt_dtos.PaymentDtos
{
    public class MakePaymentDto
    {
        [Required]
        [Range(100.0, double.MaxValue, ErrorMessage = "Enter valid amount 'N100 and above'")]
        public decimal Amount { get; set; }
        [Display(Name = "Payment Reference")]
        public string PaymentReference { get; set; }
        [Display(Name = "Payment type 'eg Offering'")]
        public string PaymentType { get; set; }
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
    }
}
