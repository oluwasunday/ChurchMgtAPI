using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class PrayerRequest : BasicEntity
    {
        public string AppUserId { get; set; }
        public string Request {  get;set; }
    }
}
