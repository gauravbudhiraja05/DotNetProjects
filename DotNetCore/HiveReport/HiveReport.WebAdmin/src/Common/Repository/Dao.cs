using Dapper;
using HiveReport.WebAdmin.Utility;
using System.Collections.Generic;
using System.Data;

namespace HiveReport.WebAdmin.Global.Repository
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
                return commandType switch
                {
                    SqlCommandType.StoredProcedure => Connection.Query<TEntity>(commandText, parameters, commandType: CommandType.StoredProcedure),
                    SqlCommandType.Text => Connection.Query<TEntity>(commandText, parameters, commandType: CommandType.StoredProcedure),
                    SqlCommandType.View => Connection.Query<TEntity>(commandText, parameters, commandType: CommandType.StoredProcedure),
                    _ => default,
                };
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDataTable(string commandText, SqlCommandType commandType, object parameters = null)
        {
            DataTable dt = new DataTable();
            switch(commandType)
            {
                case SqlCommandType.StoredProcedure:
                    dt.Load(SqlMapper.ExecuteReader(Connection, commandText, parameters, null, null, CommandType.StoredProcedure));
                break;
            }

            return dt;
        }
    }
}
