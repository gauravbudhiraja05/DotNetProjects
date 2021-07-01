using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Departments;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PickfordsIntranet.Repo
{
    public class DepartmentRepository : Repository, IDepartmentRepository
    {
        public DepartmentRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public BaseResult DeleteDepartmentsByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { DepartmentIds = IdsWithDelimitedPipeline, DeletedBy = targetIds.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Department> GetAllDepartments(string query)
        {
            try
            {
                List<Department> departmentsList = new List<Department>();
                departmentsList = Connection.Query<Department>(query, commandType: CommandType.StoredProcedure).ToList<Department>();
                return departmentsList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Department GetDepartmentById(string query, object param)
        {
            try
            {
                Department department = new Department();
                department = Connection.Query<Department>(query, param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDepartmentExist(string query, object param)
        {
            try
            {
                bool isExist = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveDepartment(string query, object param)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateDepartment(string query, object param)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
