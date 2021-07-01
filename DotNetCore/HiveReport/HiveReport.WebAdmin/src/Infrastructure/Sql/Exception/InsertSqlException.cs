using System;

namespace HiveReport.WebAdmin.Infrastructure.Sql.Exceptions
{
    [Serializable]
    class InsertSqlException : Exception
    {
        public InsertSqlException()
            : base("Error to Insert record into Table DB")
        {
        }

        public InsertSqlException(string tableName)
            : base($"Error to Insert record into Table [{tableName}]")
        {
        }

        public InsertSqlException(string tableName, int affectedRows)
            : base($"Error to Insert records into Table [{tableName}] - Insert Rows = {affectedRows}")
        {
        }

        public InsertSqlException(string tableName, int affectedRows, int totalRows)
            : base($"Error to Insert records into Table [{tableName}] - Insert Rows = {affectedRows}/{totalRows}")
        {
        }
    }
}
