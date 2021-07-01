using System.Collections.Generic;
using System.Linq;

namespace WPFStudent
{
    public class StudentRepository
    {
        private StudentDbEntities studentContext = null;

        public StudentRepository()
        {
            studentContext = new StudentDbEntities();
        }

        public StudentsData Get(int id)
        {
            return studentContext.StudentsDatas.Find(id);
        }

        public List<StudentsData> GetAll()
        {
            return studentContext.StudentsDatas.ToList();
        }

        public void AddStudent(StudentsData student)
        {
            if (student != null)
            {
                studentContext.StudentsDatas.Add(student);
                studentContext.SaveChanges();
            }
        }

        public void UpdateStudent(StudentsData student)
        {
            var studentFind = this.Get(student.ID);
            if (studentFind != null)
            {
                studentFind.Name = student.Name;
                studentFind.Contact = student.Contact;
                studentFind.Age = student.Age;
                studentFind.Address = student.Address;
                studentContext.SaveChanges();
            }
        }

        public void RemoveStudent(int id)
        {
            var studObj = studentContext.StudentsDatas.Find(id);
            if (studObj != null)
            {
                studentContext.StudentsDatas.Remove(studObj);
                studentContext.SaveChanges();
            }
        }
    }
}
