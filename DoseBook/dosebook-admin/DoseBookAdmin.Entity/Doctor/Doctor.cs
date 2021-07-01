using System.Collections.Generic;

namespace DoseBookAdmin.Entity.Doctor
{
    public class DoctorEntity
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string TelephoneNumber { get; set; }

        public string DoctorEmail { get; set; }
    }

    public class DoctorEntityList : List<DoctorEntity>
    {

    }
}
