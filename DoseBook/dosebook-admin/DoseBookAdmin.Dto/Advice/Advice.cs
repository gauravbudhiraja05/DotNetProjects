using DoseBookAdmin.Dto.Doctor;
using System.Collections.Generic;

namespace DoseBookAdmin.Dto.Advice
{
    public class AdviceDto
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string Description { get; set; }

        public string ProblemTags { get; set; }
    }

    public class AdviceDtoList : List<AdviceDto>
    {

    }

    public class AdviceListGridItemDto
    {
        public AdviceDtoList AdviceDtoList { get; set; }

        public DoctorDto DoctorDto { get; set; }
    }
}
