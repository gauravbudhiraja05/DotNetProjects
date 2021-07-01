using System.Collections.Generic;
using System.Web.Http;

namespace DeptEmpApi.Controllers
{
    public class ManageDepartmentController : ApiController
    {
        DepartmentDbEntities context = new DepartmentDbEntities();
        // GET: api/ManageDepartment
        public IEnumerable<Department> Get()
        {
            return context.Departments;
        }
        // GET: api/ManageDepartment/5
        public Department Get(int id)
        {
            return context.Departments.Find(id);
        }
        // POST: api/ManageDepartment
        public IEnumerable<Department> AddDepartment([FromBody]Department dept)
        {
            context.Departments.Add(dept);
            context.SaveChanges();
            return Get();
        }

        // PUT: api/ManageDepartment/5
        public IEnumerable<Department> Put([FromBody]Department dept)
        {
            Department oldDept = context.Departments.Find(dept.DeptId);
            context.Entry(oldDept).CurrentValues.SetValues(dept);
            context.SaveChanges();
            return Get();
        }
        // DELETE: api/ManageDepartment/5
        public IEnumerable<Department> Delete(int id)
        {
            context.Departments.Remove(Get(id));
            context.SaveChanges();
            return Get();
        }
    }
}
