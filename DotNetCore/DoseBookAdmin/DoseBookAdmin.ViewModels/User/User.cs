using System.ComponentModel.DataAnnotations;

namespace DoseBookAdmin.ViewModels.User
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter the User name.")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        public string UserName { get; set; }

        public bool IsActive { get; set; }

        public string CreationDate { get; set; }

        public string CreationDateDisplay { get; set; }

        [Required(ErrorMessage = "Please enter the telephone number.")]
        [MaxLength(20, ErrorMessage = "More than 20 character is not allowed")]
        public string TelephoneNumber { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedByType { get; set; }
        public int ModifiedBy { get; set; }
    }
}
