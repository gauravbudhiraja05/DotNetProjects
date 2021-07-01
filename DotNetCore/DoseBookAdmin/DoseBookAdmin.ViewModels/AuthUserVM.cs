using System.ComponentModel.DataAnnotations;

namespace DoseBookAdmin.ViewModels
{
    public class AuthUserVM
    {
        [Required(ErrorMessage = "Please enter the username")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter the password")]
        public string Password { get; set; }
    }
}
