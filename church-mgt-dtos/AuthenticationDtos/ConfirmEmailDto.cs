using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.AuthenticationDtos
{
    public class ConfirmEmailDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
