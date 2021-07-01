using EmpCRUDApi.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace EmpCRUDApi.Controllers
{
    [RoutePrefix("api/EmployeesApi")]
    public class EmployeeApiController : ApiController
    {
        private string _connectionString;
        private SqlHelper _sqlHelper;
        public EmployeeApiController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["EmpConnectionString"].ConnectionString;
            _sqlHelper = new SqlHelper(_connectionString);
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public IHttpActionResult GetAllEmployees()
        {
            IList<Employee> employees = _sqlHelper.GetEmployeeList();
            return Ok(employees);
        }

        [HttpPost]
        [Route("SaveEmployee")]
        public IHttpActionResult SaveEmployee(Employee employee)
        {
            string status = _sqlHelper.SaveEmployee(employee);
            return Ok(status);
        }

        [HttpGet]
        [Route("GetEmployeebyId")]
        public IHttpActionResult GetEmployeebyId(int id)
        {
            Employee employee = _sqlHelper.GetEmployeebyId(id);
            return Ok(employee);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IHttpActionResult UpdateEmployee(Employee employee)
        {
            string status = _sqlHelper.UpdateEmployee(employee);
            return Ok(status);
        }

        [HttpGet]
        [Route("DeleteEmployeebyId")]
        public IHttpActionResult DeleteEmployeebyId(int id)
        {
            string result = _sqlHelper.DeleteEmployeebyId(id);
            return Ok(result);
        }
    }
}
