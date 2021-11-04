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
        [Required(ErrorMessage = "Testimony field is required")]
        public string YourTestimony { get; set; }
    }
}
