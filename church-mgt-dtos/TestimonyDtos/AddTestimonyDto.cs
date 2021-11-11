using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.TestimonyDtos
{
    public class AddTestimonyDto
    {
        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }

        [Required]
        public string Sex { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your location")]
        public string Address { get; set; }

        [Required]
        public string YourTestimony { get; set; }
    }
}
