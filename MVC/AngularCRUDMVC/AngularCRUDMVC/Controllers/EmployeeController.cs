using AngularCRUDMVC.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AngularCRUDMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee  
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>  
        ///   
        /// Get All Employee  
        /// </summary>  
        /// <returns></returns>  
        public JsonResult Get_AllEmployee()
        {
            using (MVCEntities Obj = new MVCEntities())
            {
                List<Employee> Emp = Obj.Employees.ToList();
                return Json(Emp, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>  
        /// Get Employee With Id  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public JsonResult Get_EmployeeById(string Id)
        {
            using (MVCEntities Obj = new MVCEntities())
            {
                int EmpId = int.Parse(Id);
                return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>  
        /// Insert New Employee  
        /// </summary>  
        /// <param name="Employe"></param>  
        /// <returns></returns>  
        public string Insert_Employee(Employee Employe)
        {
            if (Employe != null)
            {
                using (MVCEntities Obj = new MVCEntities())
                {
                    Obj.Employees.Add(Employe);
                    Obj.SaveChanges();
                    return "Employee Added Successfully";
                }
            }
            else
            {
                return "Employee Not Inserted! Try Again";
            }
        }

        /// <summary>  
        /// Delete Employee Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Delete_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (MVCEntities Obj = new MVCEntities())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == EntityState.Detached)
                    {
                        Obj.Employees.Attach(Emp);
                        Obj.Employees.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "Employee Deleted Successfully";
                }
            }
            else
            {
                return "Employee Not Deleted! Try Again";
            }
        }

        /// <summary>  
        /// Update Employee Information  
        /// </summary>  
        /// <param name="Emp"></param>  
        /// <returns></returns>  
        public string Update_Employee(Employee Emp)
        {
            if (Emp != null)
            {
                using (MVCEntities Obj = new MVCEntities())
                {
                    var Emp_ = Obj.Entry(Emp);
                    Employee EmpObj = Obj.Employees.Where(x => x.Emp_Id == Emp.Emp_Id).FirstOrDefault();
                    EmpObj.Emp_Age = Emp.Emp_Age;
                    EmpObj.Emp_City = Emp.Emp_City;
                    EmpObj.Emp_Name = Emp.Emp_Name;
                    Obj.SaveChanges();
                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Employee Not Updated! Try Again";
            }
        }
    }
}