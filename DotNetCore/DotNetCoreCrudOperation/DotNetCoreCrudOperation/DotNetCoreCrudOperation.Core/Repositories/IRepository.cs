using System.Collections.Generic;

namespace PickfordsIntranet.Core.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Execute query of any type like SQl Text, Stored Procedure and View
        /// </summary>
        /// <param name="commandText">Command text: SQL Text Command, Stored Procedure Name, View name</param>
        /// <param name="commandType">CommandType: Text, StoredProcedure, View</param>
        /// <param name="parameters">Required Sql Parameters object</param>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string commandText, SqlCommandType commandType, object parameters = null);
        
    }
}
