using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class Support : BasicEntity
    {
        public string AppUserId { get; set; }
        public string Suggestion {  get; set; }
        public bool IsOpen { get; set; }
    }
}
