using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class Department : BasicEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
