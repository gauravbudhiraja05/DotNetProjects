using DoseBookAdmin.Entity.Doctor;
using System.Collections.Generic;

namespace DoseBookAdmin.Entity.Advice
{
    public class AdviceEntity
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string Description { get; set; }

        public string ProblemTags { get; set; }
    }

    public class AdviceEntityList: List<AdviceEntity>
    {

    }

    public class AdviceListGridItemEntity
    {
        public AdviceEntityList AdviceEntityList { get; set; }

        public DoctorEntity DoctorEntity { get; set; }
    }
}
