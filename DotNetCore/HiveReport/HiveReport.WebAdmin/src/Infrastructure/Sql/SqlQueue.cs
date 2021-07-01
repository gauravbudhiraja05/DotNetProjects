using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Text;

namespace HiveReport.WebAdmin.Infrastructure.Sql
{
    public class SqlQueue
    {
        private const string CLASS_NAME = "SqlQueue";
        private ILogger _log;
        private ISqlServerHelper _sqlServerHelper;

        private StringBuilder _sql;
        private int _sentencesInQueue = 0;
        private int _maxSentences = 10000;

        public SqlConnection Connection { get; }

        public SqlQueue(string connectionString, ILogger log, ISqlServerHelper sqlServerHelp, bool useQueue, int maxSentences = 10000)
        {
            _sqlServerHelper = sqlServerHelp;
            _log = log;
            Connection = _sqlServerHelper.ConnectionOpen(connectionString);
            _sql = new StringBuilder();
            if (useQueue)
                _maxSentences = maxSentences;
            else
                _maxSentences = 1;
        }

        public int Execute(string sql)
        {
            int recordsAffected = 0;
            _sql.Append(sql).AppendLine(";");
            _sentencesInQueue += 1;
            if (_sentencesInQueue >= _maxSentences)
            {
                try
                {
                    recordsAffected = _sqlServerHelper.ExecuteSQL(_sql.ToString(), Connection);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, CLASS_NAME, "Execute");
                    throw;
                }
                _sql.Clear();
                _sentencesInQueue = 0;
            }
            return recordsAffected;
        }

        public int ForceExecute()
        {
            int recordsAffected = 0;
            if (_sql.Length > 0)
                recordsAffected = _sqlServerHelper.ExecuteSQL(_sql.ToString(), Connection);
            _sql.Clear();
            _sentencesInQueue = 0;
            return recordsAffected;
        }

        public int ExecuteAndClose()
        {
            int recordsAffected = ForceExecute();
            _sqlServerHelper.ConnectionClose(Connection);
            return recordsAffected;
        }
    }
}
