using System.ComponentModel.DataAnnotations;

namespace DoseBookAdmin.Dto.User
{
    public class AuthUserDto
    {
        [Required(ErrorMessage = "Please enter the username")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter the password")]
        public string Password { get; set; }
    }
}
