using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_dtos.CommentDto
{
    public class CommentResponseDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
