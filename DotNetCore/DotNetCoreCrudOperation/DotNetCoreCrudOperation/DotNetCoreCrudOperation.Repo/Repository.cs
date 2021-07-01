using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using static Dapper.SqlMapper;

namespace PickfordsIntranet.Repo
{

    public class Repository : IRepository
    {
        protected readonly IDbConnection Connection;

        public Repository(IDbConnection connection)
        {
            Connection = connection;
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string commandText, SqlCommandType commandType, object parameters=null)
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

                    default: return default(IEnumerable<TEntity>);
                }

               
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
