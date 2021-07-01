using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace AngularJsCrudOperations.Controllers
{
    public class EmployeeController : Controller
    {
        CompanyEntities db = new CompanyEntities(); //DbContext

        //Call index page
        public ActionResult Index()
        {
            return View();
        }

        //Get Employee Record By Employee ID
        [HttpGet]
        public JsonResult GetEmployeeById(int? id)
        {
            if (id.HasValue && id.Value > 0) //Check Id has value or null
            {
                var emp = db.EmployeeDatas.Where(a => a.Id == id).SingleOrDefault();
                if (emp != null)
                {
                    return Json(emp, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Status = "Failure" });
        }

        //Get All Employee Record 
        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            var emp = db.EmployeeDatas.OrderBy(e => e.Id).ToList();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }


        //Return Crete View Page
        public ActionResult Create()
        {
            return View();
        }

        //Add New Employee Record
        [HttpPost]
        public JsonResult Create(EmployeeData e)
        {
            string message = "";
            if (ModelState.IsValid) //check model state
            {
                var employee = db.EmployeeDatas.Where(a => a.Id == e.Id).FirstOrDefault();
                if (employee == null)
                {
                    db.EmployeeDatas.Add(e);
                    db.SaveChanges();
                    message = "Success";
                }
                else
                {
                    message = "This id is already saved";
                }
            }
            else
            {
                message = "Please fill the form clearly";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        //Delete Record
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            var emp = db.EmployeeDatas.Where(a => a.Id == id).FirstOrDefault();
            if (emp != null)
            {
                db.EmployeeDatas.Remove(emp);
                db.SaveChanges();
            }
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        ////Return Employee Edit Record View Page
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        //Update the Employee Record
        [HttpPost]
        public JsonResult UpdateEmployee(EmployeeData employeeID)
        {
            if (employeeID != null && employeeID.Id > 0)
            {
                var CurrentEmployee = db.EmployeeDatas.Where(a => a.Id == employeeID.Id).SingleOrDefault();
                if (CurrentEmployee != null)
                {
                    CurrentEmployee.Id = employeeID.Id;
                    CurrentEmployee.Name = employeeID.Name;
                    CurrentEmployee.FatherName = employeeID.FatherName;
                    CurrentEmployee.EmailID = employeeID.EmailID;
                    CurrentEmployee.PhoneNo = employeeID.PhoneNo;
                    CurrentEmployee.Address = employeeID.Address;

                    db.EmployeeDatas.Attach(CurrentEmployee);
                    db.Entry(CurrentEmployee).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                }
            }
            return Json(new { Status = "Failure" });
        }

        //Return Employee Detail View Page
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }

    }
}