using EmployeeServer.Context;
using EmployeeServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly DataBaseContext _dataBaseContext;
        public EmployeeController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            var data = _dataBaseContext.Employee.ToList();
            return data;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee obj)
        {
            var data = _dataBaseContext.Employee.Add(obj);
            _dataBaseContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee obj)
        {
            var data = _dataBaseContext.Employee.Update(obj);
            _dataBaseContext.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _dataBaseContext.Employee.Where(a => a.EmpId == id).FirstOrDefault();
            _dataBaseContext.Employee.Remove(data);
            _dataBaseContext.SaveChanges();
            return Ok();

        }
    }
}
