using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.AuthenticationDtos
{
    public class RegisterResponseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsBornAgain { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
    }
}
