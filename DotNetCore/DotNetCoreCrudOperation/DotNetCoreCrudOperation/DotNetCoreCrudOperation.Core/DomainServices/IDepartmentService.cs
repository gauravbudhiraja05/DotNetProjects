using PickfordsIntranet.ViewModels.Departments;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
    /// <summary>
    /// The IDepartmentService Inteface describes the implementation required for Departments
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// GetAllEndUsers will return all end Users
        /// </summary>
        /// <returns></returns>
        IEnumerable<Department> GetAllDepartments();

        /// <summary>
        /// Check Email Address already exist or not
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <param name="id">User Id</param>
        /// <returns>true/false</returns>
        bool IsDepartmentExist(string departmentName, string departmentId);

        /// <summary>
        /// Save End User
        /// </summary>
        /// <param name="user">EndUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveDepartment(Department department);

        /// <summary>
        /// Get admin user by user Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Info of End User using EndUserVM</returns>
        Department GetDepartmentById(int id);

        /// <summary>
        /// Update End User
        /// </summary>
        /// <param name="user">EndUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateDepartment(Department department);

        /// <summary>
        /// Delete end users by ids
        /// </summary>
        /// <param name="allUserIds">list of user Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteDepartmentsByIds(DeleteItemVM targetIds);
    }
}
