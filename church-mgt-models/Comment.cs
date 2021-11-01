using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class Comment : BasicEntity
    {
        public string UserId { get; set; }
        public string Comments {  get; set; }
    }
}
