using PickfordsIntranet.ViewModels.Departments;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.Repositories
{
    public interface IDepartmentRepository
    {
        BaseResult DeleteDepartmentsByIds(string query, DeleteItemVM targetIds);

        IEnumerable<Department> GetAllDepartments(string query);

        Department GetDepartmentById(string query, object param);

        bool IsDepartmentExist(string query, object param);

        BaseResult SaveDepartment(string query, object param);

        BaseResult UpdateDepartment(string query, object param);
    }
}
