using DoseBookAdmin.Entity.Doctor;
using System.Collections.Generic;

namespace DoseBookAdmin.Entity.Test
{
    public class TestEntity
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string TestName { get; set; }

        public string ProblemTags { get; set; }
    }

    public class TestEntityList : List<TestEntity>
    {

    }

    public class TestListGridItemEntity
    {
        public TestEntityList TestEntityList { get; set; }

        public DoctorEntity DoctorEntity { get; set; }
    }
}
