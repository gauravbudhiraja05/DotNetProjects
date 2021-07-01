using System.Collections.Generic;

namespace DoseBookAdmin.Dto.Doctor
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string TelephoneNumber { get; set; }

        public string DoctorEmail { get; set; }
    }

    public class DoctorDtoList : List<DoctorDto>
    {

    }
}
