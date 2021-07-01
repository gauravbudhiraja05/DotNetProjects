using System;

namespace HiveReport.WebAdmin.Infrastructure.Sql.Exceptions
{
    [Serializable]
    class DeleteSqlException : Exception
    {
        public DeleteSqlException()
            : base("Error to Delete record from Table in DB")
        {
        }

        public DeleteSqlException(string tableName)
            : base($"Error to delete record in Table: [{tableName}]")
        {
        }

        public DeleteSqlException(string tableName, string colIdName, int colIdValue)
           : base($"Error to Delete record in Table: [{tableName}]  {colIdName} = [{colIdValue}]")
        {
        }
        public DeleteSqlException(string tableName, string colName, string colValue)
            : base($"Error to Delete record in Table: [{tableName}]  {colName} = [{colValue}]")
        {
        }
    }
}
