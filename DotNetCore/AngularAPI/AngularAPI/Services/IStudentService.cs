using AngularAPI.Entity;
using System.Collections.Generic;

namespace AngularAPI.Services
{
    public interface IStudentService
    {
        bool CreateStudent(StudentEntity entity);
        List<StudentEntity> GeadAllStudents();
        StudentEntity GetStudentById(int id);
        bool UpdateStudent(StudentEntity entity);
        bool DeleteStudent(int id);
    }
}
