using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// AdminRepository implements the IAdminRepository and Extends the generic behabiour of User Repository
    /// </summary>
    public class AdminRepository : Repository, IAdminRepository
    {
        /// <summary>
        /// AdminRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public AdminRepository(IDbConnection connection) : base(connection)
        {

        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public bool CheckValidOldPassword(string query, object param)
        {
            try
            {
                bool result = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult ChangePasswordForLoginUser(string query, object param)
        {
            try
            {
                BaseResult result = Connection.Query<BaseResult>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Admin Users
        public BaseResult DeleteAdminUserByIds(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { UserIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdminUserVM GetAdminUserById(string query, object param)
        {
            try
            {
                AdminUserVM adminUser = new AdminUserVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    adminUser = multi.Read<AdminUserVM>().SingleOrDefault();
                    adminUser.AllRoles = multi.Read<RoleVM>().AsList();
                    adminUser.AllDepartments = multi.Read<DepartmentVM>().AsList();
                    adminUser.AllSelectedRoleTypes = multi.Read<string>().SingleOrDefault();
                }

                return adminUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<AdminUserGridItemVM> GetAllAdminUsers(string query)
        {
            try
            {
                var result = Connection.Query<AdminUserGridItemVM>(query, commandType: CommandType.StoredProcedure).AsList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool ValidateNewMonthName(string query, object param)
        {
            try
            {
                var result = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

        public bool ValidateMonthYear(string query, object param)
        {
            try
            {
                var result = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdminUserVM GetPreRequisitesDataToCreateAdminUser(string query)
        {
            try
            {
                AdminUserVM adminUser = new AdminUserVM();
                using (var multi = Connection.QueryMultiple(query))
                {
                    adminUser.AllRoles = multi.Read<RoleVM>().AsList();
                    adminUser.AllDepartments = multi.Read<DepartmentVM>().AsList();
                }

                return adminUser;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEmailExist(string emailAddress, int userId)
        {
            try
            {
                bool isExist = Connection.Query<bool>("usp_IsEmailExist", new { EmailId = emailAddress, UserId = userId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetPasswordByUserName(string emailAddress)
        {
            try
            {
                return Connection.Query<string>("usp_GetPasswordByUserName", new { EmailId = emailAddress }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult SaveUser(AdminUserVM user)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddAdminUser", SqlCommandType.StoredProcedure,
                    new
                    {
                        FirstName = user.FirstName,
                        Surname = user.Surname,
                        EmailAddress = user.EmailAddress,
                        DepartmentId = user.DepartmentId,
                        RoleId = user.RoleId,
                        Password = user.UserPwd,
                        IsSuperUser = user.IsSuperUser,
                        IsActive = user.IsActive,
                        CreatedBy = user.CreatedBy,
                        IsDepartmentalUser = user.IsDepartmentalUser,
                        IsLineManagerUser = user.IsLineManagerUser,
                        LineManagerCan = user.LineManagerCanSelectOption
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateUser(AdminUserVM user)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateAdminUser", SqlCommandType.StoredProcedure,
                    new
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        Surname = user.Surname,
                        EmailAddress = user.EmailAddress,
                        DepartmentId = user.DepartmentId,
                        RoleId = user.RoleId,
                        IsSuperUser = user.IsSuperUser,
                        IsActive = user.IsActive,
                        ModifyBy = user.ModifiedBy,
                        IsDepartmentalUser = user.IsDepartmentalUser,
                        IsLineManagerUser = user.IsLineManagerUser,
                        LineManagerCan = user.LineManagerCanSelectOption
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Featured Message
        public IEnumerable<FeaturedMessageGridItemVM> GetAllFeaturedMessages(string query)
        {
            try
            {
                return Connection.Query<FeaturedMessageGridItemVM>(query, commandType: CommandType.StoredProcedure).AsList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult SaveFeaturedMessage(string spName, FeaturedMessageVM message)
        {
            try
            {
                return this.ExecuteQuery<BaseResult>(spName, SqlCommandType.StoredProcedure, new
                {
                    Image = message.ImageName,
                    Content = message.Content,
                    IsLive = message.Live,
                    AuthorName = message.AuthorName,
                    IsActive = true,
                    CreatedBy = message.CreatedBy
                }).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public FeaturedMessageVM GetFeaturedMessageById(string query, int param)
        {
            try
            {
                FeaturedMessageVM featuredMessage = this.ExecuteQuery<FeaturedMessageVM>(query, SqlCommandType.StoredProcedure, new { FeaturedMessageId = param }).SingleOrDefault();
                return featuredMessage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateFeaturedMessage(FeaturedMessageVM featuredMessage)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateFeaturedMessage", SqlCommandType.StoredProcedure,
                    new
                    {
                        FeaturedMessageId = featuredMessage.Id,
                        Image = featuredMessage.ImageName,
                        Content = featuredMessage.Content,
                        IsLive = featuredMessage.Live,
                        AuthorName = featuredMessage.AuthorName,
                        ModifyBy = featuredMessage.ModifiedBy
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteFeaturedMessageByIds(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { TargetIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        #endregion

        #region Our Values
        public OurValuesVM GetOurValues(string query)
        {
            try
            {
                OurValuesVM ourValues = Connection.Query<OurValuesVM>(query).SingleOrDefault();
                return ourValues;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateOurValues(OurValuesVM ourValues)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateOurValues", SqlCommandType.StoredProcedure,
                    new
                    {
                        OurValuesId = ourValues.Id,
                        ourValues.ValueTitle,
                        ourValues.ValueBackgroundImage,
                        ourValues.ValueTopLeftText,
                        ourValues.ValueTopRightText,
                        ourValues.CommunicationTitle,
                        ourValues.CommunicationIcon,
                        ourValues.CommunicationImage,
                        ourValues.CommunicationContent,
                        ourValues.DedicationTitle,
                        ourValues.DedicationIcon,
                        ourValues.DedicationImage,
                        ourValues.DedicationContent,
                        ourValues.CareTitle,
                        ourValues.CareIcon,
                        ourValues.CareImage,
                        ourValues.CareContent,
                        ourValues.ExcellentTitle,
                        ourValues.ExcellentIcon,
                        ourValues.ExcellentImage,
                        ourValues.ExcellentContent,
                        ourValues.ModifiedBy
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region Month Star

        public BaseResult AddMonthStars(MonthStarVM monthStars)
        {
            IDbTransaction transaction = null;
            BaseResult result1 = new BaseResult();
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                dynamic result = Connection.Query<dynamic>("usp_AddMonthStar",
                    new
                    {
                        NewMonthName = monthStars.NewMonthName,
                        IsActive=monthStars.IsActive,
                        CreatedBy = monthStars.CreatedBy

                        //MonthName = monthStars.MonthName,
                        //MonthNumber = monthStars.MonthNumber,
                        //Year = monthStars.Year,
                    },
                    transaction, buffered: true, commandTimeout: null, commandType: CommandType.StoredProcedure).SingleOrDefault();

                result1.IsSuccess = (Boolean)((object[])((IDictionary<string, object>)result).Values)[0];
                result1.Message = (String)((object[])((IDictionary<string, object>)result).Values)[1];


                if ((Boolean)((object[])((IDictionary<string, object>)result).Values)[0] == false)
                {
                    transaction.Rollback();
                }

                else
                {
                    dynamic Id = ((object[])((IDictionary<string, object>)result).Values)[2];
                    Int32 monthStarId = Convert.ToInt32(Id);
                    monthStars.Star1.MonthStarId = monthStarId;
                    monthStars.Star2.MonthStarId = monthStarId;
                    monthStars.Star3.MonthStarId = monthStarId;
                    monthStars.Star4.MonthStarId = monthStarId;
                    monthStars.Star5.MonthStarId = monthStarId;
                    monthStars.Star6.MonthStarId = monthStarId;
                    monthStars.Star7.MonthStarId = monthStarId;
                    monthStars.Star8.MonthStarId = monthStarId;
                    monthStars.Star9.MonthStarId = monthStarId;
                    monthStars.Star10.MonthStarId = monthStarId;
                    monthStars.Star11.MonthStarId = monthStarId;
                    monthStars.Star12.MonthStarId = monthStarId;

                    InsertMonthStarDetails(monthStars.Star1, transaction);
                    InsertMonthStarDetails(monthStars.Star2, transaction);
                    InsertMonthStarDetails(monthStars.Star3, transaction);
                    InsertMonthStarDetails(monthStars.Star4, transaction);
                    InsertMonthStarDetails(monthStars.Star5, transaction);
                    InsertMonthStarDetails(monthStars.Star6, transaction);
                    InsertMonthStarDetails(monthStars.Star7, transaction);
                    InsertMonthStarDetails(monthStars.Star8, transaction);
                    InsertMonthStarDetails(monthStars.Star9, transaction);
                    InsertMonthStarDetails(monthStars.Star10, transaction);
                    InsertMonthStarDetails(monthStars.Star11, transaction);
                    InsertMonthStarDetails(monthStars.Star12, transaction);


                }
                transaction.Commit();
                Connection.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Connection.Close();
                throw ex;
            }

            return result1;
        }

        private void InsertMonthStarDetails(MonthStarDetails stardetails, IDbTransaction transaction)
        {
            try
            {
                dynamic result = Connection.Query("usp_AddMonthStarDetails",
                     new
                     {
                         MonthStarId = stardetails.MonthStarId,
                         StarName = stardetails.StarName,
                         StarRole = stardetails.StarRole,
                         StarLocation = stardetails.StarLocation,
                         StarPhoto = stardetails.StarPhoto,
                         StarTestimonial = stardetails.StarTestimonial,
                         StarValueType = stardetails.StarValueType,
                         CreatedBy = stardetails.CreatedBy
                     },
                     transaction, true, null, CommandType.StoredProcedure);

                //if(result.IsSuccess==false)
                //{
                //    transaction.Rollback();
                //    return;
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MonthStarVM GetMonthStarsById(string query, int id)
        {
            try
            {
                MonthStarVM monthStar = new MonthStarVM();

                // list of MonthStarDetails object to hold the list of stars details from data source
                List<MonthStarDetails> monthStarDetailList = new List<MonthStarDetails>();

                using (var multi = Connection.QueryMultiple(query, new { MonthStarId = id }, null, null, CommandType.StoredProcedure))
                {
                    monthStar = multi.Read<MonthStarVM>().SingleOrDefault();
                    monthStarDetailList = multi.Read<MonthStarDetails>().OrderBy(ms => ms.Id).AsList();
                }

                // Set the monthStarDetailList into corresponding Star properties of MonthStarVM using reflection
                int starCounter = 0;
                monthStarDetailList.ForEach(msd =>
                {
                    PropertyInfo prop = monthStar.GetType().GetProperty("Star" + (++starCounter), BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(monthStar, msd, null);
                    }
                });

                return monthStar;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteMonthStarsByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { TargetIds = IdsWithDelimitedPipeline, DeletedBy = targetIds.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<MonthStarGridItemVM> GetAllMonthStars(string query)
        {
            try
            {
                var result = Connection.Query<MonthStarGridItemVM>(query, commandType: CommandType.StoredProcedure).AsList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<GazetteersGridItemVM> GetAllGazetteers(string query)
        {
            try
            {
                var result = Connection.Query<GazetteersGridItemVM>(query, commandType: CommandType.StoredProcedure).AsList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult AddGazetteers(GazetteersVM gazetteers)
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            IDbTransaction trans = Connection.BeginTransaction();

            try
            {

                
                dynamic gazetteersResult = Connection.Query("usp_AddGazetteers", new { gazetteers.FileName, gazetteers.CreatedBy, gazetteers.AuthorName }, trans, true, null, CommandType.StoredProcedure).SingleOrDefault();

                gazetteers.GazetteersData.TerritetoryList.ForEach(territory =>
                {

                    Connection.Query("usp_AddGazetteersPickfordsMoveCentreTerritory",
                        new
                        {
                            territory.PostDistrict,
                            territory.SalesCentre,
                            territory.MoveCentre,
                            territory.Code
                        }, trans, true, null, CommandType.StoredProcedure);
                });

                gazetteers.GazetteersData.SalesCentresList.ForEach(salesCentre =>
                {

                    Connection.Query("usp_AddGazetteersSalesCentres",
                        new
                        {
                            salesCentre.Branch,
                            salesCentre.CustomerNumber,
                            salesCentre.GroupSalesManager,
                            salesCentre.CustomerServiceManager,
                            salesCentre.AreaManager,
                            salesCentre.OpsDept
                        }, trans, true, null, CommandType.StoredProcedure);
                });

                gazetteers.GazetteersData.OperationLocationsList.ForEach(location =>
                {

                    Connection.Query("usp_AddGazetteersOPSLocations",
                        new
                        {
                            location.Location,
                            location.AreaAndAddress1,
                            location.AreaAndAddress2,
                            location.AreaAndAddress3,
                            location.AreaAndAddress4,
                            location.AreaAndAddress5,
                            location.AreaAndAddress6,
                            location.AreaAndAddress7,
                            location.RQM,
                            location.OpsContact,
                            location.OpsDept,
                            location.AreaManager,
                            location.CSM
                        }, trans, true, null, CommandType.StoredProcedure);
                });

                Int32 GazetteersId = Convert.ToInt32(((object[])((System.Collections.Generic.IDictionary<string, object>)gazetteersResult).Values)[2]);

                dynamic gazetteersDeatilsResult = Connection.Query("usp_AddGazetteersDetails", new { GazetteersId }, trans, true, null, CommandType.StoredProcedure).SingleOrDefault();

                trans.Commit();
                return new BaseResult() { IsSuccess = gazetteersResult.IsSuccess, Message = gazetteersResult.Message };
            }
            catch (Exception ex)
            {
                Connection.Close();
                trans.Rollback();
                throw ex;
            }
        }

        public BaseResult UpdateMonthStars(MonthStarVM monthStars)
        {
            IDbTransaction transaction = null;
            BaseResult result1 = new BaseResult();
            try
            {
                Connection.Open();
                transaction = Connection.BeginTransaction();
                dynamic result = Connection.Query<dynamic>("usp_UpdateMonthStar",
                    new
                    {
                        MonthStarId = monthStars.Id,
                        IsActive=monthStars.IsActive,
                        NewMonthName = monthStars.NewMonthName,
                        ModifiedBy = monthStars.ModifiedBy
                    },
                    transaction, buffered: true, commandTimeout: null, commandType: CommandType.StoredProcedure).SingleOrDefault();

                result1.IsSuccess = (Boolean)((object[])((IDictionary<string, object>)result).Values)[0];
                result1.Message = (String)((object[])((IDictionary<string, object>)result).Values)[1];


                if ((Boolean)((object[])((IDictionary<string, object>)result).Values)[0] == false)
                {
                    transaction.Rollback();
                }

                else
                {
                    monthStars.Star1.MonthStarId = monthStars.Id;
                    monthStars.Star2.MonthStarId = monthStars.Id;
                    monthStars.Star3.MonthStarId = monthStars.Id;
                    monthStars.Star4.MonthStarId = monthStars.Id;
                    monthStars.Star5.MonthStarId = monthStars.Id;
                    monthStars.Star6.MonthStarId = monthStars.Id;
                    monthStars.Star7.MonthStarId = monthStars.Id;
                    monthStars.Star8.MonthStarId = monthStars.Id;
                    monthStars.Star9.MonthStarId = monthStars.Id;
                    monthStars.Star10.MonthStarId = monthStars.Id;
                    monthStars.Star11.MonthStarId = monthStars.Id;
                    monthStars.Star12.MonthStarId = monthStars.Id;

                    InsertMonthStarDetails(monthStars.Star1, transaction);
                    InsertMonthStarDetails(monthStars.Star2, transaction);
                    InsertMonthStarDetails(monthStars.Star3, transaction);
                    InsertMonthStarDetails(monthStars.Star4, transaction);
                    InsertMonthStarDetails(monthStars.Star5, transaction);
                    InsertMonthStarDetails(monthStars.Star6, transaction);
                    InsertMonthStarDetails(monthStars.Star7, transaction);
                    InsertMonthStarDetails(monthStars.Star8, transaction);
                    InsertMonthStarDetails(monthStars.Star9, transaction);
                    InsertMonthStarDetails(monthStars.Star10, transaction);
                    InsertMonthStarDetails(monthStars.Star11, transaction);
                    InsertMonthStarDetails(monthStars.Star12, transaction);


                }
                transaction.Commit();
                Connection.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Connection.Close();
                throw ex;
            }

            return result1;
        }

        public BaseResult DeleteGazetteersByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { TargetIds = IdsWithDelimitedPipeline, DeletedBy = targetIds.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsEmailExistInEndUser(string emailAddress, int userId)
        {
            try
            {
                bool isExist = Connection.Query<bool>("usp_IsEmailExistInEndUser", new { EmailId = emailAddress, UserId = userId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion
    }
}
