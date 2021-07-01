using System.Data;
using PickfordsIntranet.Core.Repositories;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// AuthRepository implements the IAuthRepository and Extends the generic behabiour of Users Repository
    /// </summary>
    public class AuthRepository : Repository, IAuthRepository
    {
        /// <summary>
        /// AuthRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public AuthRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public string GetRoleNameByUserId(string userId)
        {
           return string.Empty;
        }
        
    }
}
