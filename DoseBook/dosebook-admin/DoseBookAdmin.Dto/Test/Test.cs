using DoseBookAdmin.Dto.Doctor;
using System.Collections.Generic;

namespace DoseBookAdmin.Dto.Test
{
    public class TestDto
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string TestName { get; set; }

        public string ProblemTags { get; set; }

    }

    public class TestDtoList: List<TestDto>
    {

    }

    public class TestListGridItemDto
    {
        public TestDtoList TestDtoList { get; set; }

        public DoctorDto DoctorDto { get; set; }
    }
}
