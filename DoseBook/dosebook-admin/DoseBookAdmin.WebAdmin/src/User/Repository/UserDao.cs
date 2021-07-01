using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.User;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.User.Repository
{
    public class UserDao : Dao, IUserDao
    {
        public UserDao(IDbConnection connection) : base(connection)
        {
        }

        public LoggedInUserEntity UserAuthenticate(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<LoggedInUserEntity>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
