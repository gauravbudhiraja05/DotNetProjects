using HiveReport.Entity.Common;
using HiveReport.WebAdmin.Configuration;
using HiveReport.WebAdmin.Infrastructure.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace HiveReport.WebAdmin.Common.Repository
{
    public class SharedLayoutDao : ISharedLayoutDao
    {
        private readonly ConnectionString _connectionStrings;
        private readonly ILogger<SharedLayoutDao> _logger;
        private readonly ISqlServerHelper _sqlServerHelper;

        public SharedLayoutDao(IOptions<ConnectionString> connectionStrings, ILogger<SharedLayoutDao> logger,
            ISqlServerHelper sqlServerHelper)
        {
            _connectionStrings = connectionStrings.Value;
            _logger = logger;
            _sqlServerHelper = sqlServerHelper;
        }

        public DepartmentEntityList GetDepartmentList(string userId)
        {
            DepartmentEntityList departmentEntityList = new DepartmentEntityList();
            string sql = @$"select autoid,isnull(departmentname,'') as departmentname,isnull(message,'') as message  from {TableDB.Department} where SavedBy='%userId%' order by departmentname";

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
                        departmentEntityList.Add(new DepartmentEntity
                        {
                            DepartmentId = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            DepartmentName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            Message = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get department list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return departmentEntityList;
        }

        public ClientEntityList GetClientList(string userId, List<int> departmentIdList)
        {
            ClientEntityList clientEntityList = new ClientEntityList();
            string sql = @$"select autoid,DeptId,isnull(clientname,'') as clientname,isnull(message,'') as message  from {TableDB.Client} where DeptId in  (" + string.Join(",", departmentIdList.Select(n => n.ToString()).ToArray()) + ") AND SavedBy='%userId%' order by clientname";

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
                        clientEntityList.Add(new ClientEntity
                        {
                            ClientId = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            DepartmentId = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            ClientName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            Message = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get client list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return clientEntityList;
        }

        public LOBEntityList GetLobList(string userId, List<int> departmentIdList, List<int> clientIdList)
        {
            LOBEntityList lobEntityList = new LOBEntityList();
            string sql = @$"select autoid,DeptId,ClientId,isnull(LOBName,'') as LOB,isnull(description,'') as message from {TableDB.LOB} where DeptId in  (" + string.Join(",", departmentIdList.Select(n => n.ToString()).ToArray()) + ") AND ClientId in  (" + string.Join(",", clientIdList.Select(n => n.ToString()).ToArray()) + ") AND  SavedBy='%userId%' order by LOB";

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
                        lobEntityList.Add(new LOBEntity
                        {
                            LOBId = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            DepartmentId = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            ClientId = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            LOBName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            Message = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get lob list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return lobEntityList;
        }

        public LeftMenuEntityList GetLeftMenuList(string userId, string requestVal, string userType)
        {
            LeftMenuEntityList leftMenuEntityList = new LeftMenuEntityList();

            string sql;
            if (userType == "Admin")
                sql = @$"select distinct menudescription,NewURLLink,orderby from {TableDB.MenuRights} mr,{TableDB.UserMenuRights} umr 
                         where mr.menuid=umr.menuid and umr.parentid ='%requestVal%' order by mr.orderby";
            else
                sql = $@"select distinct menudescription,NewURLLink,orderby from {TableDB.MenuRights} mr,{TableDB.UserMenuRights} umr 
                         where mr.menuid=umr.menuid and umr.parentid ='%requestVal%' and umr.userid='%userId%' order by mr.orderby";


            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%requestVal%", requestVal);
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        leftMenuEntityList.Add(new LeftMenuEntity
                        {
                            MenuDescription = _sqlServerHelper.ReaderGetString(reader, fieldIdx),
                            ToolTip = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            UrlLink = _sqlServerHelper.ReaderGetString(reader, fieldIdx++) + "/" + requestVal,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get left menu list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return leftMenuEntityList;
        }

        public TopMenuEntityList GetTopMenuList(string userId)
        {
            TopMenuEntityList topMenuEntityList = new TopMenuEntityList();
            string sql = @$"select distinct(a.MenuDescription) as menuname, a.NewURLLink as menureff,a.menuid  from {TableDB.MenuRights} as a, {TableDB.UserMenuRights} as b where b.userid='%userId%' and b.MenuId=a.MenuId and a.parentid='0' and b.usertype='1'  order by a.MenuDescription";

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
                        topMenuEntityList.Add(new TopMenuEntity
                        {
                            Name = _sqlServerHelper.ReaderGetString(reader, fieldIdx),
                            ToolTip = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            Url = _sqlServerHelper.ReaderGetString(reader, fieldIdx++) + "/" + _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get menu name list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return topMenuEntityList;

        }
    }
}
