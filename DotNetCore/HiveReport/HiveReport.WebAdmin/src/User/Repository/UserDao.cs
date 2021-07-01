using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using HiveReport.WebAdmin.Configuration;
using HiveReport.WebAdmin.Infrastructure.Sql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HiveReport.WebAdmin.User.Repository
{
    public class UserDao : IUserDao
    {
        private readonly ConnectionString _connectionStrings;
        private readonly ILogger<UserDao> _logger;
        private readonly ISqlServerHelper _sqlServerHelper;

        public UserDao(IOptions<ConnectionString> connectionStrings, ILogger<UserDao> logger,
            ISqlServerHelper sqlServerHelper)
        {
            _connectionStrings = connectionStrings.Value;
            _logger = logger;
            _sqlServerHelper = sqlServerHelper;
        }

        public BaseResultEntity IsEmailExists(string email)
        {
            BaseResultEntity baseResultEntity;
            string sql = $@"SELECT Count(*)  FROM {TableDB.Registration} r where r.EMail = '%email%' ";

            try
            {
                sql = SQLUtils.SQLInject(sql, "%email%", email);
                int records = Convert.ToInt32(_sqlServerHelper.ExecuteScalar(sql, _connectionStrings.HiveConnectionString));

                if (records > 0)
                {
                    baseResultEntity = new BaseResultEntity
                    {
                        IsSuccess = false,
                        Message = "User Already Exists"
                    };
                }
                else
                {
                    baseResultEntity = new BaseResultEntity
                    {
                        IsSuccess = true,
                        Message = "User Not Exists"
                    };
                }
            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

                _logger.LogError(ex, "Can not read rights by product code.");
            }

            return baseResultEntity;
        }

        public BaseResultEntity AddRegisterationDetail(RegisteredUserEntity registeredUserEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            SqlExecutionSettings sqlExecutionSettings = new SqlExecutionSettings();

            try
            {
                string insertTemplate = $@"Insert into {TableDB.Registration} (UserType, UserId, Pwd, Prefix, Username, Designation, BU, EMail,
                                     Status, AddDate, EMPId, CreatedBy, LocalUser, DeptID, ClientId, LOBID, CreatorId, Company, Mobile)
                                    Values ( %UserType% , '%UserId%' , '%Pwd%', %Prefix%' , '%Username%' , %Designation% , %BU% , %EMail% ,
                                     %Status% , %AddDate , %EMPId% , %CreatedBy% , %LocalUser%, %DeptID% , %ClientId% , %LOBID% , %CreatorId% , %Company% , %Mobile%) ";


                sqlExecutionSettings.Connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);

                DateTime timeStamp = DateTime.Now;

                string sql = insertTemplate;
                sql = SQLUtils.SQLInject(sql, "%UserType%", registeredUserEntity.UserType);
                sql = SQLUtils.SQLInject(sql, "%UserId%", registeredUserEntity.EmailAddress);
                sql = SQLUtils.SQLInject(sql, "%Pwd%", registeredUserEntity.Password);
                sql = SQLUtils.SQLInject(sql, "%Prefix%", registeredUserEntity.Prefix);
                sql = SQLUtils.SQLInject(sql, "%Username%", registeredUserEntity.Name);
                sql = SQLUtils.SQLInject(sql, "%Designation%", registeredUserEntity.Designation);
                sql = SQLUtils.SQLInject(sql, "%BU%", registeredUserEntity.BU);
                sql = SQLUtils.SQLInject(sql, "%EMail%", registeredUserEntity.EmailAddress);
                sql = SQLUtils.SQLInject(sql, "%AddDate%", timeStamp);
                sql = SQLUtils.SQLInject(sql, "%EMPId%", registeredUserEntity.EmployeeId);
                sql = SQLUtils.SQLInject(sql, "%CreatedBy%", registeredUserEntity.CreatedBy);
                sql = SQLUtils.SQLInject(sql, "%LocalUser%", registeredUserEntity.Scope);
                sql = SQLUtils.SQLInject(sql, "%DeptID%", registeredUserEntity.DepartmentId);
                sql = SQLUtils.SQLInject(sql, "%ClientId%", registeredUserEntity.ClientId);
                sql = SQLUtils.SQLInject(sql, "%LOBID%", registeredUserEntity.LOBId);
                sql = SQLUtils.SQLInject(sql, "%CreatorId%", registeredUserEntity.CreatorId);
                sql = SQLUtils.SQLInject(sql, "%Company%", registeredUserEntity.CompanyName);
                sql = SQLUtils.SQLInject(sql, "%Mobile%", registeredUserEntity.MobileNumber);
                int recordsAffected = _sqlServerHelper.ExecuteSQL(sql, sqlExecutionSettings);

                if (recordsAffected > 0)
                {
                    baseResultEntity.IsSuccess = true;
                    baseResultEntity.Message = "Record Saved Successfully";
                }
                else
                {
                    baseResultEntity.IsSuccess = false;
                    baseResultEntity.Message = "Record  Not Saved Successfully";
                }

            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                _logger.LogError(ex, "Problems in AddRegisterationDetail");
                throw;
            }
            finally
            {
                _sqlServerHelper.ConnectionClose(sqlExecutionSettings.Connection);
            }

            return baseResultEntity;
        }

        public BaseResultEntity AddProductDemoDetail(RegisteredUserEntity registeredUserEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            SqlExecutionSettings sqlExecutionSettings = new SqlExecutionSettings();
            try
            {
                string insertTemplate = $@"Insert into {TableDB.ProductDemo} ( UserId , ProductCode , InsertDate , EndDate , Status , ProductType , DatabaseType , DatabaseName )
                                    Values ( '%UserId%' , '%ProductCode%' , '%InsertDate%', '%EndDate%' , '%Status%' , '%ProductType% , '%DatabaseType% , '%DatabaseName% ) ";


                sqlExecutionSettings.Connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);

                DateTime timeStamp = DateTime.Now;

                string sql = insertTemplate;
                sql = SQLUtils.SQLInject(sql, "%UserId%", registeredUserEntity.EmailAddress);
                sql = SQLUtils.SQLInject(sql, "%ProductCode%", registeredUserEntity.ProductCode);
                sql = SQLUtils.SQLInject(sql, "%InsertDate%", timeStamp);
                sql = SQLUtils.SQLInject(sql, "%EndDate%", timeStamp.AddDays(30).ToString("d"));
                sql = SQLUtils.SQLInject(sql, "%Status%", "Active");
                sql = SQLUtils.SQLInject(sql, "%ProductType%", registeredUserEntity.ProductType);
                sql = SQLUtils.SQLInject(sql, "%DatabaseType%", registeredUserEntity.DatabaseType);
                sql = SQLUtils.SQLInject(sql, "%DatabaseName%", registeredUserEntity.Database);
                int recordsAffected = _sqlServerHelper.ExecuteSQL(sql, sqlExecutionSettings);

                if (recordsAffected > 0)
                {
                    baseResultEntity.IsSuccess = true;
                    baseResultEntity.Message = "Record Saved Successfully";
                }
                else
                {
                    baseResultEntity.IsSuccess = false;
                    baseResultEntity.Message = "Record  Not Saved Successfully";
                }

            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                _logger.LogError(ex, "Problems in AddProductDemoDetail");
                throw;
            }
            finally
            {
                _sqlServerHelper.ConnectionClose(sqlExecutionSettings.Connection);
            }

            return baseResultEntity;
        }

        public BaseResultEntity AddDemoUserDuration(string emailAddress, string userId)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            SqlExecutionSettings sqlExecutionSettings = new SqlExecutionSettings();
            try
            {
                string insertTemplate = $@"Insert into {TableDB.UserDuration} ( UserId , Duration , CurrDate , UpdBy )
                                    Values ( '%UserId%' , '%Duration%' , '%CurrDate%', '%UpdBy%') ";


                sqlExecutionSettings.Connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);

                DateTime timeStamp = DateTime.Now;

                string sql = insertTemplate;
                sql = SQLUtils.SQLInject(sql, "%UserId%", emailAddress);
                sql = SQLUtils.SQLInject(sql, "%Duration%", 30);
                sql = SQLUtils.SQLInject(sql, "%CurrDate%", timeStamp);
                sql = SQLUtils.SQLInject(sql, "%UpdBy%", userId);
                int recordsAffected = _sqlServerHelper.ExecuteSQL(sql, sqlExecutionSettings);

                if (recordsAffected > 0)
                {
                    baseResultEntity.IsSuccess = true;
                    baseResultEntity.Message = "Record Saved Successfully";
                }
                else
                {
                    baseResultEntity.IsSuccess = false;
                    baseResultEntity.Message = "Record  Not Saved Successfully";
                }

            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                _logger.LogError(ex, "Problems in AddProductDemoDetail");
                throw;
            }
            finally
            {
                _sqlServerHelper.ConnectionClose(sqlExecutionSettings.Connection);
            }

            return baseResultEntity;
        }

        public BaseResultEntity AddPasswordHistory(string emailAddress, string password, string userId)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            SqlExecutionSettings sqlExecutionSettings = new SqlExecutionSettings();
            try
            {
                string insertTemplate = $@"Insert into {TableDB.PasswordHistory} ( userid, pwd, updateDate, updatedby )
                                    Values ( '%userid%' , '%pwd%' , '%updateDate%', '%updatedby%') ";


                sqlExecutionSettings.Connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);

                DateTime timeStamp = DateTime.Now;

                string sql = insertTemplate;
                sql = SQLUtils.SQLInject(sql, "%userid%", emailAddress);
                sql = SQLUtils.SQLInject(sql, "%pwd%", password);
                sql = SQLUtils.SQLInject(sql, "%updateDate%", timeStamp);
                sql = SQLUtils.SQLInject(sql, "%updatedby%", userId);
                int recordsAffected = _sqlServerHelper.ExecuteSQL(sql, sqlExecutionSettings);

                if (recordsAffected > 0)
                {
                    baseResultEntity.IsSuccess = true;
                    baseResultEntity.Message = "Record Saved Successfully";
                }
                else
                {
                    baseResultEntity.IsSuccess = false;
                    baseResultEntity.Message = "Record  Not Saved Successfully";
                }

            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                _logger.LogError(ex, "Problems in AddProductDemoDetail");
                throw;
            }
            finally
            {
                _sqlServerHelper.ConnectionClose(sqlExecutionSettings.Connection);
            }

            return baseResultEntity;
        }

        public string GetRightsOnProductCodeBasis(RegisteredUserEntity registeredUserEntity)
        {
            string whereSql = string.Empty;
            if (registeredUserEntity != null)
            {
                whereSql = CreateWhere(registeredUserEntity);
            }

            return GetRights(whereSql);
        }

        public BaseResultEntity AddParentMenuRights(List<string> rightsList, string emailAddress, string datababase)
        {
            int recordsAffected = 0;
            BaseResultEntity baseResultEntity = new BaseResultEntity();

            SqlQueue sqlQueue = new SqlQueue(_connectionStrings.HiveConnectionString, _logger, _sqlServerHelper, useQueue: true);

            string insertTemplate = $@"Insert into {TableDB.UserMenuRights} ( LOB , MenuId , UserType , Access , Currdate , AssignBy , parentid , userid )
                                    Values ( '%LOB%' , %MenuId% , '%UserType%' , '%Access%' , '%Currdate%' , '%AssignBy%' , %parentid% , '%userid%' ) ";

            DateTime timeStamp = DateTime.Now;
            string sql = string.Empty;
            try
            {
                rightsList.ForEach(right =>
                {
                    sql = insertTemplate;
                    sql = SQLUtils.SQLInject(sql, "%LOB%", 0);
                    sql = SQLUtils.SQLInject(sql, "%MenuId%", right);
                    sql = SQLUtils.SQLInject(sql, "%UserType%", 1);
                    sql = SQLUtils.SQLInject(sql, "%Access%", "");
                    sql = SQLUtils.SQLInject(sql, "%Currdate%", timeStamp);
                    sql = SQLUtils.SQLInject(sql, "%AssignBy%", "Null");
                    sql = SQLUtils.SQLInject(sql, "%parentid%", 0);
                    sql = SQLUtils.SQLInject(sql, "%userid%", emailAddress);

                    recordsAffected += sqlQueue.Execute(sql);

                    AddSubMenuRights(right, emailAddress, datababase);

                });

                recordsAffected += sqlQueue.ExecuteAndClose();

                if (recordsAffected > 0)
                {
                    baseResultEntity.IsSuccess = true;
                    baseResultEntity.Message = "Record Saved Successfully";
                }
                else
                {
                    baseResultEntity.IsSuccess = false;
                    baseResultEntity.Message = "Record  Not Saved Successfully";
                }
            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                _logger.LogError(ex, "Problems in AddParentMenuRights");
                throw;
            }

            return baseResultEntity;
        }

        public LoggedInUserEntity GetLoggedInUserDetails(AuthUserEntity authUserEntity)
        {
            LoggedInUserEntity loggedInUserEntity = null;

            string sql = $@"SELECT Id as RecID, UserID as UserID, usertype as UserType, status as Status,
				            CreatedBy as CreatedBy, username as UserName, pwd as Password, deptid as DeptID,
				            clientid as ClientID, lobid as LOBID FROM {TableDB.Registration} WHERE Email = '%Email%' AND Pwd = '%Pwd%' ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();


            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%Email%", authUserEntity.UserName);
                sql = SQLUtils.SQLInject(sql, "%Pwd%", authUserEntity.Password);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        loggedInUserEntity = new LoggedInUserEntity
                        {
                            RecID = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            UserID = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            UserType = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            Status = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            CreatedBy = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            UserName = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            Password = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            DeptID = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            ClientID = _sqlServerHelper.ReaderGetInteger(reader, fieldIdx++),
                            LOBID = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            IsSuccess = true,
                            Message = "Record Found",
                        };
                    }
                }
                else
                {
                    loggedInUserEntity = new LoggedInUserEntity
                    {
                        IsSuccess = false,
                        Message = "No Record Found",
                    };
                }
            }
            catch (Exception ex)
            {
                loggedInUserEntity = new LoggedInUserEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

                _logger.LogError(ex, "Can not read rights by product code.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return loggedInUserEntity;
        }

        public int CheckMasterAdmin(string userId)
        {
            int count;
            string sql = @$"SELECT Count(*) from  {TableDB.MasterAdmin} where adminid= '%userid%'  ";

            try
            {
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                count = Convert.ToInt32(_sqlServerHelper.ExecuteScalar(sql, _connectionStrings.HiveConnectionString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not check master admin by userid.");
                throw;
            }

            return count;
        }

        public DateTime GetDemoUserEndDate(string userId)
        {
            DateTime endDate;
            string sql = @$"SELECT EndDate from  {TableDB.ProductDemo} where UserID= '%userId%'  ";

            try
            {
                sql = SQLUtils.SQLInject(sql, "%userId%", userId);
                endDate = Convert.ToDateTime(_sqlServerHelper.ExecuteScalarString(sql, _connectionStrings.HiveConnectionString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not get demouser end date by userid.");
                throw;
            }

            return endDate;
        }

        public int CheckUserRegisteration(string userID, string password)
        {
            int count;
            string sql = @$"SELECT Count(*) from  {TableDB.Registration} where USERID= '%userId%'  AND Pwd ='%password%' ";

            try
            {
                sql = SQLUtils.SQLInject(sql, "%userId%", userID);
                sql = SQLUtils.SQLInject(sql, "%password%", password);
                count = Convert.ToInt32(_sqlServerHelper.ExecuteScalar(sql, _connectionStrings.HiveConnectionString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not check master admin by userid.");
                throw;
            }

            return count;
        }

        public RegisteredUserEntity GetProductDemoDetail(string userId)
        {
            RegisteredUserEntity registeredUserEntity = new RegisteredUserEntity();

            string sql = $@"SELECT ProductCode, ProductType, DatabaseType, DatabaseName FROM 
                          {TableDB.ProductDemo} WHERE UserId = '%UserId%'";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();

            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%UserId%", userId);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        registeredUserEntity = new RegisteredUserEntity
                        {
                            ProductCode = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            ProductType = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            DatabaseType = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                            Database = _sqlServerHelper.ReaderGetString(reader, fieldIdx++),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not Get ProductDemoDetail by user id.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return registeredUserEntity;
        }

        public LoggedInUserEntity GetSearchedResult(string dropdownValue, string txtValue, string userid)
        {
            LoggedInUserEntity loggedInUserEntity = new LoggedInUserEntity();

            if (!string.IsNullOrEmpty(txtValue))
            {
                string query;
                if (dropdownValue == "Search All")
                {
                    query = "select recid,usertype,userid,prefix,username,designation,bu,email,status,empid,pwdstatus,isnull(lockreason,'') as lockreason,lockdate,createdby,localuser,deptid,clientid,lobid,creatorid,adminid, convert(varchar,adddate,111) as adddate from registration where usertype<>3   order by ";
                }
                else if (dropdownValue == "DeptId")
                {
                    query = "select recid,usertype,userid,prefix,username,designation,bu,email,status,empid,pwdstatus,isnull(lockreason,'') as lockreason,lockdate,createdby,localuser,deptid,clientid,lobid,creatorid,adminid, convert(varchar,adddate,111) as adddate from registration where  usertype<>3 and " + dropdownValue + " in(select autoid from IDMSDepartment where DepartmentName like '" + txtValue + "%')  and clientid='0' and lobid='0'  order by ";
                }
                else if (dropdownValue == "LOBID")
                {
                    query = "select A.recid,A.usertype,A.userid,A.prefix,A.username,A.designation,A.bu,A.email,A.status,A.empid,A.pwdstatus,isnull(A.lockreason,'') as lockreason,A.lockdate,A.createdby,A.localuser,A.deptid,A.clientid,A.lobid,A.creatorid,A.adminid, convert(varchar,A.adddate,111) as adddate from registration A where  A.usertype<>3 and " + dropdownValue + " in (select autoid from WARSLobMaster where LOBname like '" + txtValue + "%')  order by ";
                }
                else if (dropdownValue == "ClientId")
                {
                    query = "select A.recid,A.usertype,A.userid,A.prefix,A.username,A.designation,A.bu,A.email,A.status,A.empid,A.pwdstatus,isnull(A.lockreason,'') as lockreason,A.lockdate,A.createdby,A.localuser,A.deptid,A.clientid,A.lobid,A.creatorid,A.adminid, convert(varchar,A.adddate,111) as adddate from registration A  where clientid in ( select autoid from idmsclient where clientname like '" + txtValue + "%')  and lobid='0' order by ";
                }
                else if (dropdownValue == "Resigned")
                {
                    query = "select * from registration where lockreason ='$Resigned' and CreatedBy='" + userid + "' order by   ";
                }
                else if (dropdownValue == "Transfered")
                {
                    query = "select * from registration where lockreason = '$Transfer' and CreatedBy='" + userid + "' order by ";
                }
            }

            return loggedInUserEntity;
        }

        #region Private Methods

        private string GetRights(string whereSql, string fromSql = "", string orderBy = "")
        {
            string rights;
            string sql = @"SELECT p.Rights ";

            if (!string.IsNullOrWhiteSpace(fromSql))
            {
                sql += " " + fromSql;
            }
            else
            {
                sql += $" FROM {TableDB.ProductMaster} p";
            }

            if (!string.IsNullOrWhiteSpace(whereSql))
            {
                sql += " " + whereSql;
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                sql += " " + orderBy;
            }

            try
            {
                rights = _sqlServerHelper.ExecuteScalarString(sql, _connectionStrings.HiveConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not read rights by product code.");
                throw;
            }

            return rights;
        }

        private static string CreateWhere(RegisteredUserEntity registeredUserEntity)
        {
            bool isFirst = true;
            string whereSql = "";

            // productCode - string
            if (!string.IsNullOrEmpty(registeredUserEntity.ProductCode))
            {
                whereSql += WhereClause(ref isFirst) + " p.ProductCode ='" + registeredUserEntity.ProductCode + "' ";
            }
            if (!string.IsNullOrEmpty(registeredUserEntity.DatabaseType))
            {
                whereSql += WhereClause(ref isFirst) + " p.DatabaseType ='" + registeredUserEntity.DatabaseType + "' ";
            }
            if (!string.IsNullOrEmpty(registeredUserEntity.ProductType))
            {
                whereSql += WhereClause(ref isFirst) + " p.UserType ='" + registeredUserEntity.ProductType + "' ";
            }

            return whereSql;
        }

        private static string WhereClause(ref bool isFirst)
        {
            string clause;

            if (isFirst)
            {
                clause = " WHERE ";
                isFirst = false;
            }
            else
            {
                clause = "\n   AND ";
            }

            return clause;
        }

        private void AddSubMenuRights(string right, string emailAddress, string datababase)
        {
            List<int> subMenus = new List<int>();

            if (right == "31")
            {
                switch (datababase)
                {
                    case "Excel":
                        subMenus.AddRange(new int[] { 32, 33, 34 });
                        break;
                    case "Oracle":
                        subMenus.AddRange(new int[] { 34, 159 });
                        break;
                    case "MS-SQL":
                        subMenus.AddRange(new int[] { 34, 158 });
                        break;
                    case "Excel, Oracle":
                        subMenus.AddRange(new int[] { 32, 33, 34, 159 });
                        break;
                    case "Excel, MS-SQL":
                        subMenus.AddRange(new int[] { 32, 33, 34, 158 });
                        break;
                    case "MS-SQL,Oracle":
                        subMenus.AddRange(new int[] { 158, 159 });
                        break;
                    case "Excel,MS-SQL,Oracle":
                        subMenus.AddRange(new int[] { 32, 33, 34, 158, 159 });
                        break;
                };

                AddSubMenu(subMenus, emailAddress, datababase, right);
            }
            else if (right == "6")
            {
                if (datababase == "MS-SQL" || datababase == "Oracle" ||
                    datababase == "MySQL" || datababase == "PostgreSQL")
                {
                    subMenus.AddRange(new int[] { 37, 149 });
                }

                AddSubMenu(subMenus, emailAddress, datababase, right);
            }
            else
            {
                if (right == "17")
                {
                    subMenus.AddRange(new int[] { 21 });

                    AddSubMenu(subMenus, emailAddress, datababase, right);
                }
                else
                {
                    subMenus = GetMenuNameList(right);

                    AddSubMenu(subMenus, emailAddress, datababase, right);
                }
            }
        }

        private void AddSubMenu(List<int> subMenus, string emailAddress, string datababase, string right)
        {
            int recordsAffected = 0;
            SqlQueue sqlQueue = new SqlQueue(_connectionStrings.HiveConnectionString, _logger, _sqlServerHelper, useQueue: true);

            string insertTemplate = $@"Insert into {TableDB.UserMenuRights} ( LOB , MenuId , UserType , Access , Currdate , AssignBy , parentid , userid )
                                    Values ( '%LOB%' , %MenuId% , '%UserType%' , '%Access%' , '%Currdate%' , '%AssignBy%' , %parentid% , '%userid%' ) ";

            DateTime timeStamp = DateTime.Now;
            string sql = string.Empty;
            try
            {
                subMenus.ForEach(subMenu =>
                {
                    sql = insertTemplate;
                    sql = SQLUtils.SQLInject(sql, "%LOB%", 0);
                    sql = SQLUtils.SQLInject(sql, "%MenuId%", subMenu);
                    sql = SQLUtils.SQLInject(sql, "%UserType%", 1);
                    sql = SQLUtils.SQLInject(sql, "%Access%", "");
                    sql = SQLUtils.SQLInject(sql, "%Currdate%", timeStamp);
                    sql = SQLUtils.SQLInject(sql, "%AssignBy%", "Null");
                    sql = SQLUtils.SQLInject(sql, "%parentid%", right);
                    sql = SQLUtils.SQLInject(sql, "%userid%", emailAddress);

                    recordsAffected += sqlQueue.Execute(sql);
                });

                recordsAffected += sqlQueue.ExecuteAndClose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problems in AddSubMenu");
                throw;
            }
        }

        private List<int> GetMenuNameList(string right)
        {
            List<int> menuNameList = new List<int>();
            string sql = $@"SELECT m.Rights  FROM {TableDB.MenuRights} m where m.ParentId = '%ParentId%' ";

            SqlDataReader reader = null;
            SqlConnection connection = new SqlConnection();


            try
            {
                int fieldIdx;
                sql = SQLUtils.SQLInject(sql, "%ParentId%", right);
                connection = _sqlServerHelper.ConnectionOpen(_connectionStrings.HiveConnectionString);
                reader = _sqlServerHelper.GetReader(sql, connection, -1);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fieldIdx = 0;
                        menuNameList.Add(_sqlServerHelper.ReaderGetInteger(reader, fieldIdx++));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Can not read rights by product code.");
                throw;
            }
            finally
            {
                _sqlServerHelper.ReaderClose(reader);
                _sqlServerHelper.ConnectionClose(connection);
            }

            return menuNameList;
        }

        #endregion

    }
}
