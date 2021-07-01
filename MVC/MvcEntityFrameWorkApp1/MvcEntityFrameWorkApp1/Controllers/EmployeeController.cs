using MvcEntityFrameWorkApp1.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcEntityFrameWorkApp1.Controllers
{
    public class EmployeeController : Controller
    {
        EmpDataContext objDataContext = new EmpDataContext();

        // GET: Employee
        [HttpGet]
        public ActionResult EmpDetails()
        {
            return View(objDataContext.employees.ToList());
        }

        [HttpGet]
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Employee objEmp)
        {
            objDataContext.employees.Add(objEmp);
            objDataContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            int empId = Convert.ToInt32(id);
            var emp = objDataContext.employees.Find(empId);
            return View(emp);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            int empId = Convert.ToInt32(id);
            var emp = objDataContext.employees.Find(empId);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(Employee objEmp)
        {
            var data = objDataContext.employees.Find(objEmp.EmpId);
            if (data != null)
            {
                data.Name = objEmp.Name;
                data.Address = objEmp.Address;
                data.Email = objEmp.Email;
                data.MobileNo = objEmp.MobileNo;
            }
            objDataContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            int empId = Convert.ToInt32(id);
            var emp = objDataContext.employees.Find(empId);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(Employee objEmp)
        {
            var emp = objDataContext.employees.Find(objEmp.EmpId);
            if (emp != null)
            {
                objDataContext.employees.Remove(emp);
                objDataContext.SaveChanges();
            }
            return View();
        }
    }
}