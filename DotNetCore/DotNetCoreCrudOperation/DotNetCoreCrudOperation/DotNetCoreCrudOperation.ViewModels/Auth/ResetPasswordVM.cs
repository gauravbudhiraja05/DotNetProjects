using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.Auth
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Please enter a new password.")]
        [MinLength(8,ErrorMessage = "Your password must be at least 8 characters.")]
        [MaxLength(50,ErrorMessage = "Your password cannot be greater than 50 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("NewPassword",ErrorMessage = "The new password and confirm password do not match.")]
        [MinLength(8, ErrorMessage = "Your password must be at least 8 characters.")]
        [MaxLength(50, ErrorMessage = "Your password cannot be greater than 50 characters.")]
        public string ConfirmPassword { get; set; }
        public Guid Token { get; set; }
    }
}
