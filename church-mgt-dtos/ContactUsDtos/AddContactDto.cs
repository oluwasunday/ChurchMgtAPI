using System.ComponentModel.DataAnnotations;

namespace church_mgt_dtos.ContactUsDtos
{
    public class AddContactDto
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Message { get; set; }
    }
}
