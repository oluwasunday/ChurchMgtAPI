using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.PaymentTypeDtos
{
    public class AddPaymentTypeDto
    {
        [Required(ErrorMessage = "Type of payment is required")]
        public string TypeOfPayment { get; set; }
    }
}
