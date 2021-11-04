using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.PrayerRequestDtos
{
    public class AddPrayerRequestDto
    {
        [Required (ErrorMessage = "Request field is required")]
        public string Request { get; set; }
    }
}
