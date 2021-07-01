using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DeptEmpApi.Controllers
{
    public class ManageEmployeeController : ApiController
    {
        DepartmentDbEntities context = new DepartmentDbEntities();
        public List<Employee> Get()
        {
            List<Employee> lstEmp = context.Employees.ToList();
            return lstEmp;
        }
        public Employee Get(int id)
        {
            return context.Employees.Find(id);
        }
        public List<Employee> AddEmployee(Employee emp)
        {
            context.Employees.Add(emp);
            context.SaveChanges();
            return Get();
        }
        public List<Employee> Put([FromBody]Employee emp)
        {
            Employee oldEmp = Get(emp.EmpId);
            context.Entry(oldEmp).CurrentValues.SetValues(emp);
            context.SaveChanges();
            return Get();
        }
        public List<Employee> Delete(int id)
        {
            context.Employees.Remove(Get(id));
            context.SaveChanges();
            return Get();
        }
    }
}
