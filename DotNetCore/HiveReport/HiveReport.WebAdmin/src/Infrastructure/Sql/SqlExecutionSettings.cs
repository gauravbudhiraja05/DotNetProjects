using System.Data.SqlClient;

namespace HiveReport.WebAdmin.Infrastructure.Sql
{
    public class SqlExecutionSettings
    {
        public SqlConnection Connection { get; set; }
        public SqlTransaction Transaction { get; set; }
        public int Timeout { get; set; }
    }

    public class HiveReportSqlExecutionSettings : SqlExecutionSettings
    {
        public string UserName { get; set; }
    }
}
