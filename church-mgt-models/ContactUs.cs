using System;
using System.ComponentModel.DataAnnotations;

namespace church_mgt_models
{
    public class ContactUs
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress (ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
