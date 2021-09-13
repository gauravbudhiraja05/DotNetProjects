using HiveReport.Entity.User;
using HiveReport.WebAdmin.Configuration;
using HiveReport.WebAdmin.Infrastructure.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HiveReport.WebAdmin.Account.Repository
{
    public class AccountDao : IAccountDao
    {
        private readonly ConnectionString _connectionStrings;
        private readonly ILogger<AccountDao> _logger;
        private readonly ISqlServerHelper _sqlServerHelper;

        public AccountDao(IOptions<ConnectionString> connectionStrings, ILogger<AccountDao> logger,
            ISqlServerHelper sqlServerHelper)
        {
            _connectionStrings = connectionStrings.Value;
            _logger = logger;
            _sqlServerHelper = sqlServerHelper;
        }

        public Dictionary<int, string> GetDesignationList()
        {
            Dictionary<int, string> designationList = new Dictionary<int, string>();
            string sql = @$"Select DesignationId, Designationname from {TableDB.DesignationMaster}";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        designationList.Add
                            (
                                _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                                _sqlServerHelper.ReaderGetString(reader, fieldIdx++)
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get designation list.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return designationList;
        }

        public Dictionary<int, string> GetDepartmentList(string savedBy)
        {
            Dictionary<int, string> departmentList = new Dictionary<int, string>();
            string sql = @$"Select AutoID, DepartmentName from {TableDB.Department} where SavedBy= '%SavedBy%'";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%SavedBy%", savedBy);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        departmentList.Add
                            (
                                _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                                _sqlServerHelper.ReaderGetString(reader, fieldIdx++)
                            );
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

            return departmentList;
        }

        public Dictionary<int, string> GetClientList(int departmentId)
        {
            Dictionary<int, string> clientList = new Dictionary<int, string>();
            string sql = @$"Select AutoID, ClientName from {TableDB.Client} where DeptID= %DeptId%";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%DeptId%", departmentId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        clientList.Add
                            (
                                _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                                _sqlServerHelper.ReaderGetString(reader, fieldIdx++)
                            );
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

            return clientList;
        }

        public Dictionary<int, string> GetLOBList(int departmentId, int clientId)
        {
            Dictionary<int, string> lobList = new Dictionary<int, string>();
            string sql = @$"Select AutoID, LOBName from {TableDB.LOB} where DeptID= %DeptId% And  ClientId = %ClientId%";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%DeptId%", departmentId);
                sql = SQLUtils.SQLInject(sql, "%ClientId%", clientId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        lobList.Add
                            (
                                _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                                _sqlServerHelper.ReaderGetString(reader, fieldIdx++)
                            );
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

            return lobList;
        }

        public bool CheckAvailableEmployeeId(int employeeId)
        {
            bool isAvailable;
            string sql = @$"Select Count(*) from {TableDB.Registration} where empId = %employeeId%";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                sql = SQLUtils.SQLInject(sql, "%employeeId%", employeeId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                int count = Convert.ToInt32(_sqlServerHelper.ExecuteScalar(sql, _connectionStrings.HiveConnectionString));

                if (count > 0)
                    isAvailable = false;
                else
                    isAvailable = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not check CheckAvailableEmployeeId.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return isAvailable;
        }

        public bool CheckAvailableUserId(string emailAddress)
        {
            bool isAvailable;
            string sql = @$"Select Count(*) from {TableDB.Registration} where EMail = '%email%'";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                sql = SQLUtils.SQLInject(sql, "%email%", emailAddress);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                int count = Convert.ToInt32(_sqlServerHelper.ExecuteScalar(sql, _connectionStrings.HiveConnectionString));

                if (count > 0)
                    isAvailable = false;
                else
                    isAvailable = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not check CheckAvailableUserId.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return isAvailable;
        }

        public bool AddNewDesignation(string designation)
        {
            bool isAdded;
            string sql = @$"Insert Into {TableDB.DesignationMaster} Values('%Designation%')";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                sql = SQLUtils.SQLInject(sql, "%Designation%", designation);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                int count = Convert.ToInt32(_sqlServerHelper.ExecuteSQL(sql, _connectionStrings.HiveConnectionString));

                if (count > 0)
                    isAdded = true;
                else
                    isAdded = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not add AddNewDesignation.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return isAdded;
        }

        public void AddAccountUTypeDetails(string userType, string userId)
        {
            string sql = @$"Insert Into {TableDB.AccountUType} Select MAX(Autoid), %UserType% from  {TableDB.LogAccountMaster} where EntityName= '%UserId%' AND Action='Create'";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                sql = SQLUtils.SQLInject(sql, "%UserType%", userType);
                sql = SQLUtils.SQLInject(sql, "%UserId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                int count = Convert.ToInt32(_sqlServerHelper.ExecuteSQL(sql, _connectionStrings.HiveConnectionString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not add AddAccountUTypeDetails.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }
        }

        public void AddWarsCountLogin(string userId)
        {
            string sql = @$"Insert Into {TableDB.CountLogin} Values('%UserId%',0)";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                sql = SQLUtils.SQLInject(sql, "%UserId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                int count = Convert.ToInt32(_sqlServerHelper.ExecuteSQL(sql, _connectionStrings.HiveConnectionString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not add AddWarsCountLogin.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }
        }

        public void AddBuddyDetails(RegisteredUserEntity registeredUser)
        {
            string sql = @$"Insert Into {TableDB.Buddy} (UserId, Username, buddystatus, Departmentname, Clientname, LOBname, DepartmentID, ClientId, lobid, Comments, UserBuddy )
                            Values( %UserId%, %Username%, %buddystatus%, %Departmentname%, %Clientname, %LOBname%, %DepartmentID%, %ClientId%, %lobid%, %Comments%, %UserBuddy%)";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                sql = SQLUtils.SQLInject(sql, "%UserId%", registeredUser.UserId);
                sql = SQLUtils.SQLInject(sql, "%Username%", registeredUser.EmailAddress);
                sql = SQLUtils.SQLInject(sql, "%buddystatus%", registeredUser.Scope);
                sql = SQLUtils.SQLInject(sql, "%Departmentname%", registeredUser.DepartmentName);
                sql = SQLUtils.SQLInject(sql, "%Clientname%", registeredUser.ClientName);
                sql = SQLUtils.SQLInject(sql, "%LOBname%", registeredUser.LOBName);
                sql = SQLUtils.SQLInject(sql, "%DepartmentID%", registeredUser.DepartmentId);
                sql = SQLUtils.SQLInject(sql, "%ClientId%", registeredUser.ClientId);
                sql = SQLUtils.SQLInject(sql, "%lobid%", registeredUser.LOBId);
                sql = SQLUtils.SQLInject(sql, "%Comments%", string.Empty);
                sql = SQLUtils.SQLInject(sql, "%UserBuddy%", registeredUser.UserId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                int count = Convert.ToInt32(_sqlServerHelper.ExecuteSQL(sql, _connectionStrings.HiveConnectionString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not add AddBuddyDetails.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }
        }
    }
}
