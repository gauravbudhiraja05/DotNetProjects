using System;

namespace HiveReport.WebAdmin.Infrastructure.Sql.Exceptions
{
    [Serializable]
    class UpdateSqlException : Exception
    {
        public UpdateSqlException()
            : base("Error to Update record into Table DB")
        {
        }

        public UpdateSqlException(string tableName)
            : base($"Error to Update record into Table [{tableName}]")
        {
        }

        public UpdateSqlException(string tableName, int affectedRows)
            : base($"Error to Update records into Table [{tableName}] - Update Rows = {affectedRows}")
        {
        }

        public UpdateSqlException(string tableName, int affectedRows, int totalRows)
            : base($"Error to Update records into Table [{tableName}] - Update Rows = {affectedRows}/{totalRows}")
        {
        }
    }
}
