using DoseBookAdmin.Entity.Doctor;
using System.Collections.Generic;

namespace DoseBookAdmin.Entity.MedicineDose
{
    public class MedicineDoseEntity
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

    public class MedicineDoseEntityList : List<MedicineDoseEntity>
    {

    }

    public class MedicineDoseListGridItemEntity
    {
        public List<MedicineDoseEntity> MedicineDoseEntityList { get; set; }

        public DoctorEntity DoctorEntity { get; set; }
    }

    public class MedicineDoseSimulationEntity
    {
        public MedicineDoseEntity MedicineDoseEntity { get; set; }

        public DoctorEntityList DoctorEntityList { get; set; }

        public List<string> FrequencyList { get; set; }

        public List<string> DirectionList { get; set; }

        public List<string> DurationList { get; set; }

        public List<string> DoseUnitList { get; set; }

        public List<string> DoseList { get; set; }
    }
}
