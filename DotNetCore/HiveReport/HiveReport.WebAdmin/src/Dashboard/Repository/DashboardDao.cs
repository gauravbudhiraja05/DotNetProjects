using HiveReport.Entity.User;
using HiveReport.WebAdmin.Configuration;
using HiveReport.WebAdmin.Infrastructure.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;

namespace HiveReport.WebAdmin.Dashboard.Repository
{
    public class DashboardDao : IDashboardDao
    {
        private readonly ConnectionString _connectionStrings;
        private readonly ILogger<DashboardDao> _logger;
        private readonly ISqlServerHelper _sqlServerHelper;


        public DashboardDao(IOptions<ConnectionString> connectionStrings, ILogger<DashboardDao> logger,
            ISqlServerHelper sqlServerHelper)
        {
            _connectionStrings = connectionStrings.Value;
            _logger = logger;
            _sqlServerHelper = sqlServerHelper;
        }

        public string GetDemoUserProductCode(string userId)
        {
            string rights;
            string sql = @$"SELECT ProductCode from  {TableDB.ProductDemo} where UserID= '%userId%'  ";

            try
            {
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                rights = _sqlServerHelper.ExecuteScalarString(sql, _connectionStrings.HiveConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get demo user product code.");
                throw;
            }

            return rights;
        }

        public string GetDemoUserRights(string productCode)
        {
            string rights;
            string sql = @$"SELECT Rights from {TableDB.ProductMaster} where ProductCode= '%productCode%'  ";

            try
            {
                sql = SQLUtils.SQLInject(sql, "%productCode%", productCode);
                rights = _sqlServerHelper.ExecuteScalarString(sql, _connectionStrings.HiveConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get demo user rights  product code.");
                throw;
            }

            return rights;
        }

        public ReportMasterEntityList GetReportMasterList(string userId)
        {
            ReportMasterEntityList reportMasterEntityList = new ReportMasterEntityList();
            string sql = @$"Select Top(5) Queryname,SavedBy,DepartmentID,ClientID,UnderLOB,TableName from {TableDB.ReportMaster}  where Recordid<=(select MAX(Recordid) from {TableDB.ReportMaster}) and Savedby = '%userid%' order by recordid desc  ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();


            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        reportMasterEntityList.Add(new ReportMasterEntity
                        {
                            QueryName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            SavedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            DepartmentID = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            ClientID = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            UnderLOB = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            TableName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get report list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return reportMasterEntityList;
        }

        public TableMasterEntityList GetTableMasterList(string userId)
        {
            TableMasterEntityList tableMasterEntityList = new TableMasterEntityList();
            string sql = @$"Select Tableid,tablename,CreatedBy from {TableDB.TableMaster} where Tableid<=(select MAX(Tableid) from {TableDB.TableMaster}) and CreatedBy='%userid%' order by tableid desc ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        tableMasterEntityList.Add(new TableMasterEntity
                        {
                            TableId = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            TableName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            CreatedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get table list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return tableMasterEntityList;
        }

        public ViewMasterEntityList GetViewMasterList(string userId)
        {
            ViewMasterEntityList viewMasterEntityList = new ViewMasterEntityList();
            string sql = @$"select Top(5) ViewID,Viewname,CreatedBy  from {TableDB.ViewMaster} where Viewid<=(select MAX(Viewid) from {TableDB.ViewMaster}) and CreatedBy='%userId%' order by viewid desc";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        viewMasterEntityList.Add(new ViewMasterEntity
                        {
                            ViewID = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            ViewName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            CreatedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get view list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return viewMasterEntityList;
        }

        public HTMLFileEntityList GetHtmlFileMasterList(string userId)
        {
            HTMLFileEntityList htmlFileMasterEntityList = new HTMLFileEntityList();
            string sql = @$"select Top(5) Autoid,SavedFilename,SavedBy  from {TableDB.HtmlFileMaster} where Autoid<=(select MAX(Autoid) from {TableDB.HtmlFileMaster}) and SavedBy='%userid%'  order by Autoid desc ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        htmlFileMasterEntityList.Add(new HTMLFileEntity
                        {
                            AutoId = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            SavedFileName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            SavedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get html file list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return htmlFileMasterEntityList;
        }

        public GraphMasterEntityList GetGraphMasterList(string userId)
        {
            GraphMasterEntityList graphMasterEntityList = new GraphMasterEntityList();
            string sql = @$"select Top(5) Recordid,Graphname,SavedBy  from {TableDB.GraphMaster} where Recordid<=(select MAX(Recordid) from {TableDB.ReportMaster}) and SavedBy='%userId%'  order by Recordid desc ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        graphMasterEntityList.Add(new GraphMasterEntity
                        {
                            RecordId = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            GraphName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            SavedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get graph name list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return graphMasterEntityList;
        }

        public QueryMasterEntityList GetQueryMasterList(string userId)
        {
            QueryMasterEntityList queryMasterEntityList = new QueryMasterEntityList();
            string sql = @$"select Top(5) Queryname,SavedBy,convert(varchar(6),DepartmentId) as DepartmentId,convert(varchar(6),ClientId) as ClientId,lobyName  from {TableDB.QueryMaster} where Recordid<=(select MAX(Recordid) from {TableDB.QueryMaster}) and SavedBy='%userId%'  order by Recordid desc ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        queryMasterEntityList.Add(new QueryMasterEntity
                        {
                            QueryName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            SavedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            DepartmentId = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            ClientId = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            LobyName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get query name list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return queryMasterEntityList;
        }

        
    }
}
