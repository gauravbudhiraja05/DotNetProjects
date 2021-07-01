namespace PickfordsIntranet.Core.Repositories
{
    /// <summary>
    /// IAuthRepository describes the implementation required for authenticate 
    /// and authorize the user from target data source
    /// </summary>
    public interface IAuthRepository:IRepository
    {
        /// <summary>
        /// Get role name by user id :emailId from data source
        /// </summary>
        /// <param name="userId">Email Id of User</param>
        /// <returns>Role Name of User</returns>
        string GetRoleNameByUserId(string userId);
    }
}
