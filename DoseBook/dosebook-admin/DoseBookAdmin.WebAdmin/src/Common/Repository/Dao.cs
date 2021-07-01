using Dapper;
using DoseBookAdmin.Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;

namespace DoseBookAdmin.WebAdmin.Common.Repository
{
    public class Dao : IDao
    {
        /// <summary>
        /// Database Connection
        /// </summary>
        protected readonly IDbConnection Connection;

        public Dao(IDbConnection connection)
        {
            Connection = connection;
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string commandText, SqlCommandType commandType, object parameters = null)
        {
            try
            {
                switch (commandType)
                {
                    case SqlCommandType.StoredProcedure:
                        return Connection.Query<TEntity>(commandText, parameters, commandType: CommandType.StoredProcedure);

                    case SqlCommandType.Text:
                        return Connection.Query<TEntity>(commandText, parameters, commandType: CommandType.StoredProcedure);

                    case SqlCommandType.View:
                        return Connection.Query<TEntity>(commandText, parameters, commandType: CommandType.StoredProcedure);

                    default: return default;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
