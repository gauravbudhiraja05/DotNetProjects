using System.ComponentModel.DataAnnotations;

namespace PickfordsIntranet.ViewModels
{
    /// <summary>
    /// User Authentication Params  data structure
    /// </summary>
    public class AuthUserVM
    {
        [Required(ErrorMessage ="Please enter the username")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        public string WindowsUserId { get; set; }

        //[Required(ErrorMessage ="Please enter the password")]
        public string Password { get; set; }
    }
}
