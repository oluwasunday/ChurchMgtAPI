using System;

namespace church_mgt_dtos
{
    public class RegisterDto
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public bool IsBornAgain { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
