using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_models
{
    public class AppUser : IdentityUser
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName {  get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsBornAgain { get; set; }
        public DateTime DOB { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string Avatar { get; set; } = "default.jpg";
        public string PublicId { get; set; } 


        public ICollection<Department> Departments { get; set; }
        public ICollection<Support> Supports { get; set; }
        public ICollection<PrayerRequest> PrayerRequests { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
