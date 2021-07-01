using HiveReport.WebAdmin.Infrastructure.Sql.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace HiveReport.WebAdmin.Infrastructure.Sql
{
    public class SqlServerHelper : ISqlServerHelper
    {
        private readonly ILogger _logger;
        private readonly bool _FixUniCodeSql = false;
        private readonly Regex _RegExUnicode;
        private readonly bool mFixUniCodeSql = false;

        public SqlServerHelper(ILogger logger)
        {
            _logger = logger;
            _RegExUnicode = new Regex("(?<=(\\(|,|\\s+|=\\s*))\\\'(?=((\\\'\\\')|[^\']))");
            //_FixUniCodeSql 
            //TODO: fixUniCodeSql should be injected in the constructor also (it should be a section in settings.json)
        }

        #region Connection

        public bool OpenConnectionIfClosed(ref SqlConnection connection, string connectionString)
        {
            bool isOpen = false;
            try
            {
                if (connection == null || connection.State == ConnectionState.Closed)
                {
                    connection = ConnectionOpen(connectionString);
                    isOpen = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OpenConnection Fails");
            }
            return isOpen;
        }
        public void ConnectionClose(SqlConnection connection)
        {
            try
            {
                if (!(connection == null))
                {
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not close connection");
            }
        }
        public SqlConnection ConnectionOpen(string connectionString)
        {
            SqlConnection connection;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ConnectionOpen()");
                throw;
            }

            return connection;
        }

        #endregion

        #region Execute Scalar

        public double ExecuteScalar(SqlCommand command, SqlConnection connection, SqlTransaction transaction = null)
        {
            double value = 0;
            if (mFixUniCodeSql)
            {
                command.CommandText = UnicodeQueryFix(command.CommandText);
            }

            command.Connection = connection;
            if (!(transaction == null))
            {
                command.Transaction = transaction;
            }

            object oValue = command.ExecuteScalar();
            if (!DBNull.Value.Equals(oValue))
            {
                value = Convert.ToDouble(oValue);
            }

            return value;
        }
        public double ExecuteScalar(string sql, SqlConnection connection, SqlTransaction transaction = null)
        {
            SqlCommand cmdSQL;
            if (mFixUniCodeSql)
            {
                sql = UnicodeQueryFix(sql);
            }

            cmdSQL = new SqlCommand(sql);
            if (!(transaction == null))
            {
                cmdSQL.Transaction = transaction;
            }

            double value = ExecuteScalar(cmdSQL, connection);
            return value;
        }
        public double ExecuteScalar(string sql, string connectionString)
        {
            double value = 0;
            string errorText;
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            try
            {
                if (mFixUniCodeSql)
                {
                    sql = UnicodeQueryFix(sql);
                }

                connection = ConnectionOpen(connectionString);
                command = new SqlCommand(sql)
                {
                    Connection = connection
                };
                object oValue = command.ExecuteScalar();
                if (!DBNull.Value.Equals(oValue))
                {
                    value = Convert.ToDouble(oValue);
                }

            }
            catch (Exception ex)
            {
                errorText = $"Error executing scalar SQL. {command.CommandText} \r\n {ex.Message}";
                throw new Exception(errorText, ex);
            }
            finally
            {
                ConnectionClose(connection);
            }

            return value;
        }
        public string ExecuteScalarString(string sql, string connectionString)
        {
            string value = string.Empty;
            string errorText;
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            try
            {
                if (mFixUniCodeSql)
                {
                    sql = UnicodeQueryFix(sql);
                }

                connection = ConnectionOpen(connectionString);
                command = new SqlCommand(sql)
                {
                    Connection = connection
                };
                object oValue = command.ExecuteScalar();
                if (!DBNull.Value.Equals(oValue))
                {
                    value = Convert.ToString(oValue);
                }
            }
            catch (Exception ex)
            {
                errorText = $"Error executing scalar SQL. {command.CommandText} \r\n {ex.Message}";
                throw new Exception(errorText, ex);
            }
            finally
            {
                ConnectionClose(connection);
            }

            return value;
        }

        #endregion

        #region Execute Sql

        public int ExecuteSQL(SqlCommand command, SqlConnection connection, int timeOut = -1, SqlTransaction transaction = null)
        {
            if (mFixUniCodeSql)
            {
                command.CommandText = UnicodeQueryFix(command.CommandText);
            }

            command.Connection = connection;
            if (!(transaction == null))
            {
                command.Transaction = transaction;
            }

            if ((timeOut > -1))
            {
                command.CommandTimeout = timeOut;
            }

            int recordsAffected = command.ExecuteNonQuery();
            return recordsAffected;
        }
        public int ExecuteSQL(string sql, SqlConnection connection, int timeOut = -1, SqlTransaction transaction = null, bool fixUnicodeException = false)
        {
            SqlCommand command;
            if ((mFixUniCodeSql
                        && !fixUnicodeException))
            {
                sql = UnicodeQueryFix(sql);
            }

            command = new SqlCommand(sql, connection);
            if (!(transaction == null))
            {
                command.Transaction = transaction;
            }

            if ((timeOut > -1))
            {
                command.CommandTimeout = timeOut;
            }

            int recordsAffected = command.ExecuteNonQuery();
            return recordsAffected;
        }
        public int ExecuteSQL(string sql, SqlExecutionSettings sqlSettings)
        {
            return ExecuteSQL(sql, sqlSettings.Connection, sqlSettings.Timeout, sqlSettings.Transaction);
        }
        public int ExecuteSQL(string sql, string connectionString, int timeOut = -1)
        {
            SqlConnection connection = null;
            SqlCommand command;
            try
            {
                if (mFixUniCodeSql)
                {
                    sql = UnicodeQueryFix(sql);
                }

                connection = ConnectionOpen(connectionString);
                command = new SqlCommand(sql, connection);
                if ((timeOut > -1))
                {
                    command.CommandTimeout = timeOut;
                }

                int recordsAffected = command.ExecuteNonQuery();
                return recordsAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ConnectionClose(connection);
            }
        }

        #endregion

        #region GetIdentity
        public int GetIdentity(SqlConnection connection, string colNameId = "ID")
        {
            int newId;

            try
            {
                string selectSql = "SELECT CAST(scope_identity() AS int)";
                newId = Convert.ToInt32(ExecuteScalar(selectSql, connection));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error GetIdentity()");
                throw new GetIdentitySqlException(colNameId);
            }

            return newId;
        }
        #endregion

        #region DataTable

        public DataTable GetDataTable(string sql, SqlConnection connection, int timeOut = -1)
        {
            DataTable dt = new DataTable();
            SqlDataReader reader = GetReader(sql, connection, timeOut);
            dt.Load(reader);
            return dt;
        }

        #endregion

        #region Open/close reader

        public SqlDataReader GetReader(SqlCommand command, SqlConnection connection, int timeOut = -1)
        {
            SqlDataReader reader;
            if (_FixUniCodeSql)
            {
                command.CommandText = UnicodeQueryFix(command.CommandText);
            }

            command.Connection = connection;
            if ((timeOut > -1))
            {
                command.CommandTimeout = timeOut;
            }

            reader = command.ExecuteReader();
            return reader;
        }
        public SqlDataReader GetReader(string sql, SqlConnection connection, int timeOut = -1, SqlTransaction transaction = null)
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = sql
            };
            if (!(transaction == null))
            {
                command.Transaction = transaction;
            }

            SqlDataReader reader = GetReader(command, connection, timeOut);
            command.Dispose();
            return reader;
        }
        public void ReaderClose(SqlDataReader reader)
        {
            if (!(reader == null))
            {
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        #endregion

        #region Reader get value

        public DateTime ReaderGetDate(SqlDataReader reader, int field)
        {
            object valueObject;
            valueObject = reader.GetValue(field);
            if (!Convert.IsDBNull(valueObject))
                return Convert.ToDateTime(valueObject);
            else
                return new DateTime(1900, 1, 1);
        }
        public double ReaderGetDouble(SqlDataReader reader, int field)
        {
            object valueObject;
            double valueDouble = 0;
            try
            {
                valueObject = reader.GetValue(field);
                if (!(valueObject is DBNull))
                    valueDouble = Convert.ToDouble(valueObject);
            }
            catch (Exception)
            {
            }

            return valueDouble;
        }
        public int ReaderGetInteger(SqlDataReader reader, int field)
        {
            object valueObject;
            int valueInteger = 0;
            try
            {
                valueObject = reader.GetValue(field);
                if (!(valueObject is DBNull))
                    valueInteger = Convert.ToInt32(valueObject);
            }
            catch (Exception)
            {
            }

            return valueInteger;

        }
        public long ReaderGetLong(SqlDataReader reader, int field)
        {
            object valueObject;
            long valueLong = 0;
            try
            {
                valueObject = reader.GetValue(field);
                if (!(valueObject is DBNull))
                    valueLong = Convert.ToInt64(valueObject);
            }
            catch (Exception)
            {
            }

            return valueLong;

        }
        public string ReaderGetString(SqlDataReader reader, int field)
        {
            object valueObject;
            string valueString = "";
            try
            {
                valueObject = reader.GetValue(field);
                if (!(valueObject is DBNull))
                    valueString = valueObject.ToString().Trim();
            }
            catch (Exception)
            {
                // Do nothing
            }

            return valueString;
        }
        public bool ReaderGetBoolean(SqlDataReader reader, int field)
        {
            object valueObject;
            bool valueBool = false;
            try
            {
                valueObject = reader.GetValue(field);
                if (!(valueObject is DBNull))
                    valueBool = Convert.ToBoolean(valueObject);
            }
            catch (Exception)
            {
                // Do nothing
            }

            return valueBool;
        }

        #endregion

        #region Private Methods

        private string UnicodeQueryFix(string sSql)
        {
            return _RegExUnicode.Replace(sSql, "N\'");
        }

        #endregion
    }
}
