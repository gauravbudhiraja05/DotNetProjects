using StudentCrudService.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace StudentCrudService.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        StudentCRUDEntities wmsEN = new StudentCRUDEntities();
        [Route("StudentInsert")]
        [HttpPost]
        public object StudentInsert(StudentData objVM)
        {
            try
            {
                if (objVM.Id == 0)
                {
                    var objlm = new StudentData();
                    objlm.StudentName = objVM.StudentName;
                    objlm.FName = objVM.FName;
                    objlm.MName = objVM.MName;
                    objlm.ContactNo = objVM.ContactNo;
                    wmsEN.StudentDatas.Add(objlm);
                    wmsEN.SaveChanges();
                    return new ResultVM
                    { Status = "Success", Message = "SuccessFully Saved." };
                }
                else
                {
                    var objlm = wmsEN.StudentDatas.Where(s => s.Id == objVM.Id).ToList<StudentData>().FirstOrDefault();
                    if (objlm.Id > 0)
                    {
                        objlm.StudentName = objVM.StudentName;
                        objlm.FName = objVM.FName;
                        objlm.MName = objVM.MName;
                        objlm.ContactNo = objVM.ContactNo;
                        wmsEN.SaveChanges();
                        return new ResultVM
                        { Status = "Success", Message = "SuccessFully Update." };
                    }
                    return new ResultVM
                    { Status = "Error", Message = "Invalid." };
                }
            }
            catch (Exception ex)
            {
                return new ResultVM
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }
        [Route("GetStudentData")]
        [HttpGet]
        public object GetStudentData()
        {
            var obj = from u in wmsEN.StudentDatas
                      select u;
            return obj;
        }
        [Route("GetStudentById")]
        [HttpGet]
        public object GetStudentById(int Id)
        {
            return wmsEN.StudentDatas.Where(s => s.Id == Id).ToList<StudentData>().FirstOrDefault();
        }
        [Route("DeleteStudent")]
        [HttpGet]
        public object DeleteStudent(int Id)
        {
            try
            {
                var objlm = wmsEN.StudentDatas.Where(s => s.Id == Id).ToList<StudentData>().FirstOrDefault();
                wmsEN.StudentDatas.Remove(objlm);
                wmsEN.SaveChanges();
                return new ResultVM
                { Status = "Success", Message = "SuccessFully Delete." };
            }
            catch (Exception ex)
            {
                return new ResultVM
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }
    }
}
