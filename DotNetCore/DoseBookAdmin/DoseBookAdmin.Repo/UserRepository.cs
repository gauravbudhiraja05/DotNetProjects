using Dapper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public IEnumerable<User> GetAllUsers(string query)
        {
            try
            {
                List<User> userList = Connection.Query<User>(query, commandType: CommandType.StoredProcedure).ToList();
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
