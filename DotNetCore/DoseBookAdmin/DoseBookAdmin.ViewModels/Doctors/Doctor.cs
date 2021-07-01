using System.ComponentModel.DataAnnotations;

namespace DoseBookAdmin.ViewModels.Doctors
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Please enter the Doctor name.")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Please enter the telephone number.")]
        [MaxLength(20, ErrorMessage = "More than 20 character is not allowed")]
        public string TelephoneNumber { get; set; }
    }
}
