using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.ViewModels.Departments;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    [Authorize(Roles = "SA")]
    public class DepartmentController : Controller
    {
        /// <summary>
        /// IAdminService data member
        /// </summary>
        private IDepartmentService _departmentService;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DepartmentController> _logger;

        /// <summary>
        /// EndUserController constructor
        /// </summary>
        /// <param name="enduserService"></param>
        public DepartmentController(IDepartmentService departmentService, IPathProvider pathProvider, ILogger<DepartmentController> logger)
        {
            _pathProvider = pathProvider;
            _departmentService = departmentService;
            _logger = logger;
        }

        /// <summary>
        /// Get End Users grid view 
        /// </summary>
        /// <returns>End Users grid view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult Index()
        {
            return View(_departmentService.GetAllDepartments());
        }

        /// <summary>
        /// Get Add Admin users view 
        /// </summary>
        /// <returns>Add Admin user view</returns>
        public IActionResult AddDepartment()
        {
            try
            {
                Department dept = new Department();
                //dept.CreationDate = DateTime.Now.ToString("MM/dd/yyyy");
                return View(dept);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }

        }

        /// <summary>
        /// To Check the Email Address already exist or not
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <param name="id">User Id</param>
        /// <returns>Json data :true or string message</returns>
        [AllowAnonymous]
        public IActionResult ValidateDepartmentExistOrNot(string DepartmentName, string DepartmentId)
        {
            bool isExist = _departmentService.IsDepartmentExist(DepartmentName, DepartmentId);
            if (isExist)
                return Json(data: "The department name already exists, please enter an alternative.");

            return Json(data: true);
        }

        /// <summary>
        /// Post Add admin user to save it
        /// </summary>
        /// <param name="user">AddAdminUserVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddDepartment([Bind] Department department, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    if (department.HeaderImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            department.HeaderImage.CopyTo(memoryStream);
                            string imageName = Guid.NewGuid() + Path.GetFileName(department.HeaderImage.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\Department\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, memoryStream.ToArray());
                            department.ImageName = imageName;
                        }
                    }
                    department.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _departmentService.SaveDepartment(department);
                }


                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }

        /// <summary>
        /// Edit End User Record
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>View of Edit Admin User</returns>
        public IActionResult EditDepartment(int id)
        {
            try
            {
                Department department = _departmentService.GetDepartmentById(id);
                return View(department);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }

        }

        /// <summary>
        /// Update Admin user
        /// </summary>
        /// <param name="user">Admin user that will be updated</param>
        /// <param name="loggingDetails">logged-in User Info</param>
        /// <returns></returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult EditDepartment(Department department, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {

                if (ModelState.IsValid)
                {
                    if (department.HeaderImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            department.HeaderImage.CopyTo(memoryStream);
                            string imageName = Guid.NewGuid() + Path.GetFileName(department.HeaderImage.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\Department\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, memoryStream.ToArray());
                            department.ImageName = imageName;
                        }
                    }

                    department.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _departmentService.UpdateDepartment(department);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }
        /// <summary>
        /// Delete Admin Users 
        /// </summary>
        /// <param name="allUserIds">List of End User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteDepartment(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _departmentService.DeleteDepartmentsByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }

    }
}