using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SatyaAngularjsWebApi.Controllers
{
    public class EmployeeAPIController : ApiController
    {
        // Get All The Employee  
        [HttpGet]
        public List<Employee> Get()
        {
            List<Employee> emplist = new List<Employee>();
            using (Database1Entities db = new Database1Entities())
            {
                var results = db.sp_InsUpdDelEmployee(0, "", "", "", "", "", "Get").ToList();
                foreach (var result in results)
                {
                    var employee = new Employee()
                    {
                        Id = result.Id,
                        Name = result.Name,
                        Address = result.Address,
                        Country = result.Country,
                        City = result.City,
                        Mobile = result.Mobile
                    };
                    emplist.Add(employee);
                }
                return emplist;
            }
        }
        // Get Employee By Id  
        public Employee Get(int id)
        {
            using (Database1Entities db = new Database1Entities())
            {
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return employee;
            }
        }
        // Insert Employee  
        public HttpResponseMessage Post(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (Database1Entities db = new Database1Entities())
                {
                    var emplist = db.sp_InsUpdDelEmployee(0, employee.Name, employee.Address, employee.Country, employee.City, employee.Mobile, "Ins").ToList();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, emplist);
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
        // Update Employee  
        public HttpResponseMessage Put(Employee employee)
        {
            List<sp_InsUpdDelEmployee_Result> emplist = new List<sp_InsUpdDelEmployee_Result>();
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            using (Database1Entities db = new Database1Entities())
            {
                try
                {
                    emplist = db.sp_InsUpdDelEmployee(employee.Id, employee.Name, employee.Address, employee.Country, employee.City, employee.Mobile, "Upd").ToList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, emplist);
        }
        // Delete employee By Id  
        public HttpResponseMessage Delete(int id)
        {
            using (Database1Entities db = new Database1Entities())
            {
                List<sp_InsUpdDelEmployee_Result> emplist = new List<sp_InsUpdDelEmployee_Result>();
                var results = db.sp_InsUpdDelEmployee(id, "", "", "", "", "", "GetById").ToList();
                if (results.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                try
                {
                    emplist = db.sp_InsUpdDelEmployee(id, "", "", "", "", "", "Del").ToList();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
                return Request.CreateResponse(HttpStatusCode.OK, emplist);
            }
        }
        // Prevent Memory Leak  
        protected override void Dispose(bool disposing)
        {
            using (Database1Entities db = new Database1Entities())
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
