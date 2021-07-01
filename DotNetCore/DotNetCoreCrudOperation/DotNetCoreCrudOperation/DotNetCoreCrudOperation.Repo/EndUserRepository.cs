using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PickfordsIntranet.Repo
{
    public class EndUserRepository : Repository, IEndUserRepository
    {
        /// <summary>
        /// EndUserRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public EndUserRepository(IDbConnection connection) : base(connection)
        {

        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public IEnumerable<EndUserGridItemVM> GetAllEndUsers(string query)
        {
            try
            {
                return  Connection.Query<EndUserGridItemVM>(query, commandType: CommandType.StoredProcedure).AsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEmailExist(string emailAddress, int userId)
        {
            try
            {
                bool isExist = Connection.Query<bool>("usp_IsEndUserEmailExist", new { EmailId = emailAddress, UserId = userId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult SaveUser(EndUserVM user)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddEndUser", SqlCommandType.StoredProcedure,
                    new
                    {
                        FirstName = user.FirstName,
                        Surname = user.Surname,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EndUserVM GetEndUserById(string query, object param)
        {
            try
            {
                return Connection.Query<EndUserVM>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateEndUser(EndUserVM user)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateEndUser", SqlCommandType.StoredProcedure,
                    new
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        Surname = user.Surname,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteEndUserByIds(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { UserIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
