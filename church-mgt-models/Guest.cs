using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class Guest : BasicEntity
    {
        public string FullName { get; set; }
        public string Eamil { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
