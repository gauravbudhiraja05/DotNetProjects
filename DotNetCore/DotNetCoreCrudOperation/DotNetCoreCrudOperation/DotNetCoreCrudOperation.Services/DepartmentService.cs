using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Departments;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Services
{
    public class DepartmentService : IDepartmentService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        private IPathProvider _pathProvider;

        /// <summary>
        /// NewsService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public DepartmentService(IUnitOfWork unitOfWork, IPathProvider pathProvider)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _pathProvider = pathProvider;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public BaseResult DeleteDepartmentsByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.DeptRepo.DeleteDepartmentsByIds("usp_DeleteDepartmentsByIds", targetIds);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _unitOfWork.DeptRepo.GetAllDepartments("usp_GetAllDepartments_New");
        }

        public Department GetDepartmentById(int id)
        {
            return _unitOfWork.DeptRepo.GetDepartmentById("usp_GetDepartmentById", new { deptId = id });
        }

        public bool IsDepartmentExist(string departmentName, string departmentId)
        {
            return _unitOfWork.DeptRepo.IsDepartmentExist("usp_IsDepartmentExist", new { departmentName = departmentName, departmentId = departmentId });
        }

        public BaseResult SaveDepartment(Department department)
        {
            return _unitOfWork.DeptRepo.SaveDepartment("usp_SaveDepartment", new { departmentname = department.DepartmentName, isActive= department.IsActive, image= department.ImageName , createdBy = department.CreatedBy, telephoneNumber = department.TelephoneNumber, departmentHead = department.DepartmentHead});
        }

        public BaseResult UpdateDepartment(Department department)
        {
            return _unitOfWork.DeptRepo.UpdateDepartment("usp_UpdateDepartment", new { departmentname = department.DepartmentName, departmenId = department.DepartmentId, isActive = department.IsActive, image=department.ImageName, modifiedBy = department.ModifiedBy, telephoneNumber = department.TelephoneNumber, departmentHead = department.DepartmentHead });
        }
    }
}
