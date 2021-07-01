using System;
using System.Data;
using System.Data.SqlClient;

namespace HiveReport.WebAdmin.Infrastructure.Sql
{
    public interface ISqlServerHelper
    {
        //Connection
        bool OpenConnectionIfClosed(ref SqlConnection connection, string connectionString);
        void ConnectionClose(SqlConnection connection);
        SqlConnection ConnectionOpen(string connectionString);

        //Execute Scalar
        double ExecuteScalar(SqlCommand command, SqlConnection connection, SqlTransaction transaction = null);
        double ExecuteScalar(string sql, SqlConnection connection, SqlTransaction transaction = null);
        double ExecuteScalar(string sql, string connectionString);
        string ExecuteScalarString(string sql, string connectionString);

        //Execute Sql
        int ExecuteSQL(SqlCommand command, SqlConnection connection, int timeOut = -1, SqlTransaction transaction = null);
        int ExecuteSQL(string sql, SqlConnection connection, int timeOut = -1, SqlTransaction transaction = null/* TODO Change to default(_) if this is not a reference type */, bool fixUnicodeException = false);
        int ExecuteSQL(string sql, SqlExecutionSettings sqlSettings);
        int ExecuteSQL(string sql, string connectionString, int timeOut = -1);

        //Get Identity
        int GetIdentity(SqlConnection connection, string colNameId = "ID");

        //DataTable
        DataTable GetDataTable(string sql, SqlConnection connection, int timeOut = -1);

        //Open/close reader
        SqlDataReader GetReader(SqlCommand command, SqlConnection connection, int timeOut = -1);
        SqlDataReader GetReader(string sql, SqlConnection connection, int timeOut = -1, SqlTransaction transaction = null/* TODO Change to default(_) if this is not a reference type */);
        void ReaderClose(SqlDataReader reader);

        //Reader get value
        DateTime ReaderGetDate(SqlDataReader reader, int field);
        double ReaderGetDouble(SqlDataReader reader, int field);
        int ReaderGetInteger(SqlDataReader reader, int field);
        long ReaderGetLong(SqlDataReader reader, int field);
        string ReaderGetString(SqlDataReader reader, int field);
        bool ReaderGetBoolean(SqlDataReader reader, int field);

    }
}
