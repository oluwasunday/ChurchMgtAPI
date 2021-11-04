using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.DepartmentDtos
{
    public class MembersInDeptDto
    {
        public string Department { get; set; }
        public List<string> FullName { get; set; }
    }
}
