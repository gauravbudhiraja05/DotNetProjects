using DoseBookAdmin.Dto.Doctor;
using System.Collections.Generic;

namespace DoseBookAdmin.Dto.MedicineDose
{
    public class MedicineDoseDto
    {
        public int MedicineId { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string MedicineName { get; set; }

        public string Frequency { get; set; }

        public string Directions { get; set; }

        public string Composition { get; set; }

        public string Duration { get; set; }

        public string DoseUnit { get; set; }

        public string Dose { get; set; }

        public string ProblemTags { get; set; }

        //public string DoctorMedicineDoseType { get; set; }

        //public string MasterMedicineDoseMedicineName { get; set; }

    }

    public class MedicineDoseDtoList : List<MedicineDoseDto>
    {

    }


    public class MedicineDoseListGridItemDto
    {
        public MedicineDoseDtoList MedicineDoseDtoList { get; set; }

        public DoctorDto DoctorDto { get; set; }
    }

    public class MedicineDoseSimulationDto
    {
        public MedicineDoseDto MedicineDoseDto { get; set; }

        public DoctorDtoList DoctorDtoList { get; set; }

        public List<string> FrequencyList { get; set; }

        public List<string> DirectionList { get; set; }

        public List<string> DurationList { get; set; }

        public List<string> DoseUnitList { get; set; }

        public List<string> DoseList { get; set; }
    }
}
