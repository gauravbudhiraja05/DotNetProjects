using Dapper;
using Dream14.Core.Helper;
using Dream14.Core.Repositories;
using Dream14.ViewModels.Global;
using Dream14.ViewModels.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dream14.Repo
{
    public class AdminRepository : Repository, IAdminRepository
    {

        /// <summary>
        /// AdminRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public AdminRepository(IDbConnection connection) : base(connection)
        {

        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        #region Admin Users

        public IEnumerable<AdminUser> GetAllAdminUsers(string query)
        {
            try
            {
                var adminUserList = Connection.Query<AdminUser>(query, commandType: CommandType.StoredProcedure).ToList();
                return adminUserList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult CheckEmailExist(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveAdminUser(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdminUser GetAdminUserById(string query, object param)
        {
            try
            {
                var adminUser = ExecuteQuery<AdminUser>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault(); ;
                return adminUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateAdminUser(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult DeleteAdminUsersByIds(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
