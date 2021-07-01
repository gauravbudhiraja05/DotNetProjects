using System.ComponentModel.DataAnnotations;

namespace PickfordsIntranet.ViewModels
{
    /// <summary>
    /// Change password data structure
    /// </summary>
    public class ChangePasswordVM
    {
        [Required(AllowEmptyStrings =false)]
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
