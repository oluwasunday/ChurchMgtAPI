using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class Testimony : BasicEntity
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }
        [Required]
        public string YourTestimony { get; set; }
    }
}
