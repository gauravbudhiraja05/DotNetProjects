using Dapper;
using PickfordsIntranet.Core.Repositories.FrontEnd;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.FrontEnd;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.EndUser;
using Microsoft.AspNetCore.Http;
using PickfordsIntranet.Core.Repositories;

namespace PickfordsIntranet.Repo.FrontEnd
{
    public class FrontEndRepository : Repository, IFrontEndRepository
    {
        /// <summary>
        /// FrontEndRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public FrontEndRepository(IDbConnection connection) : base(connection)
        {

        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }


        public FrontEndUser FrontEndUserAuthenticate(AuthUserVM user)
        {

            return Connection.Query<FrontEndUser>("usp_AuthenticateFrontEndUser", new { WindowsUserId = user.WindowsUserId }, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
        }

        public FrontEndUser GetFrontEndUserById(string query, object param)
        {
            return Connection.Query<FrontEndUser>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
        }
        static readonly string[] SizeSuffixes =
                   { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
        public Registration GetDepartmentListForLogin()
        {
            Registration obj = new Registration();
            obj.EndUserDepartmentList = GetAllEndUserDepartmentList("usp_GetAllDepartments");
            return obj;
        }
        public FrontEndVM GetFrontEndVMDetails(int id, string type)
        {
            var rs = new FrontEndVM();

            rs.PostalCodesList = GetAllLocationList("usp_GetAllLocations");
            rs.DepartmentsList = GetAllDepartmentList("usp_GetAllDepartments");
            rs.NewsList = GetAllNews("usp_GetAllNewsList_Modified");
            rs.FeatureMessagesDetail = GetFeatureMessageDetails("usp_GetFeatureMessageDetails");
            rs.MonthStarList = GeetMonthStarList("usp_GetLatestAwardeeDetails"); //GeetMonthStarList("usp_GetMonthStarDetailsByMonthStarID");
            rs.VacancyList = GetAllVacancies("usp_GetAllVacancies_Modified");
            rs.OurValuesDetail = GetOurValues("usp_GetOurValues");
            rs.EndUserDepartmentList = GetAllEndUserDepartmentList("usp_GetAllDepartments");
            rs.VacancyDepartmentsList = GetAllVacancyDepartmentList("usp_GetAllVacancyDepartments");
            rs.NewsDepartmentsList = GetAllNewsDepartmentList("usp_GetAllNewsDepartments");
            rs.FrontEndNewsList = GetAllFrontEndNewsList("usp_GetAllFrontEndFeaturedNews");
            //rs.FeaturedDocumentList = GetFeaturedDocumentList("usp_GetFeaturedDocumentList");
            if (type == "VacancyId")
            {
                rs.VacancyDetail = GetVacancyById("usp_GetVacancyById_FrontEnd", new { vacancyId = id });
            }
            else if (type == "NewsId")
            {
                rs.NewsDetail = GetNewsById("usp_GetNewsById_FrontEnd", new { newsId = id });
            }
            else if (type.Contains("DepartmentId"))
            {
                int loggedinUserId = Convert.ToInt32(type.Split('~', StringSplitOptions.RemoveEmptyEntries)[1]);
                rs.DocumentsByDepartment = GetDocumentListByDepartmentId("usp_GetAllDocumentAndFolderForFrontend", new { DepartmentId = id });

                foreach (var item in rs.DocumentsByDepartment.Documents)
                {
                    item.Filesize = SizeSuffix(Convert.ToInt64(item.Filesize), 2);
                }

                rs.FavouriteDocumentsList = GetFavouriteDocumentList("usp_GetFavouriteDocumentList", new { userId = loggedinUserId });

                foreach (var doc in rs.DocumentsByDepartment.Documents)
                    foreach (var FavDoc in rs.FavouriteDocumentsList)
                        if (doc.DocumentId == FavDoc.DocumentId)
                        {
                            doc.IsFavouriteDocumentForUser = true;
                            break;
                        }
                        else
                            if (!doc.IsFavouriteDocumentForUser)
                            doc.IsFavouriteDocumentForUser = false;
            }
            else if (type == "NewsByDepartmentWise")
            {
                List<News> departmentNewsList = GetNewsByDepartmentWise("usp_GetNewsListbyDepartmentId_Modified", new { deptid = id });
                if (departmentNewsList.Count < 4)
                {
                    int othersNewsCount = 4 - (departmentNewsList.Count);
                    List<News> otherDepartmentNewsList = GetAllNewsListExceptSelectedDepartmentById("usp_GetAllNewsListExceptSelectedDepartmentById", new { deptId = id, othersNewsCount = othersNewsCount });
                    departmentNewsList.AddRange(otherDepartmentNewsList);
                }
                rs.NewsListByDepartmentWise = departmentNewsList;
            }

            return rs;
        }

        private List<NewsDepartment> GetAllNewsDepartmentList(string query)
        {
            try
            {
                List<NewsDepartment> newsDepartmentsList = new List<NewsDepartment>();
                newsDepartmentsList = Connection.Query<NewsDepartment>(query, commandType: CommandType.StoredProcedure).ToList<NewsDepartment>();
                if (newsDepartmentsList.Count > 0)
                {
                    NewsDepartment newsDepartment = new NewsDepartment();
                    newsDepartment.DepartmentId = 0;
                    newsDepartment.DepartmentName = "Show: All News";
                    newsDepartmentsList.Insert(0, newsDepartment);
                }
                return newsDepartmentsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private List<VacancyDepartment> GetAllVacancyDepartmentList(string query)
        {
            try
            {
                List<VacancyDepartment> vacancyDepartmentsList = new List<VacancyDepartment>();
                vacancyDepartmentsList = Connection.Query<VacancyDepartment>(query, commandType: CommandType.StoredProcedure).ToList<VacancyDepartment>();
                if (vacancyDepartmentsList.Count > 0)
                {
                    VacancyDepartment vacancyDepartment = new VacancyDepartment();
                    vacancyDepartment.DepartmentId = 0;
                    vacancyDepartment.DepartmentName = "Show: All Vacancies";
                    vacancyDepartmentsList.Insert(0, vacancyDepartment);
                }
                return vacancyDepartmentsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<FavouriteDocument> GetFavouriteDocumentList(string query, object param)
        {
            try
            {
                List<FavouriteDocument> rs = new List<FavouriteDocument>();
                rs = Connection.Query<FavouriteDocument>(query, param, commandType: CommandType.StoredProcedure).ToList<FavouriteDocument>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndVM GetAllDepartmentForNews()
        {
            var rs = new FrontEndVM();
            rs.EndUserDepartmentList = GetAllEndUserDepartmentList("usp_GetAllDepartments");
            rs.PostalCodesList = GetAllLocationList("usp_GetAllLocations");
            rs.DepartmentsList = GetAllDepartmentList("usp_GetAllDepartments");
            rs.NewsDepartmentsList = GetAllNewsDepartmentList("usp_GetAllNewsDepartments");
            //if (rs.DepartmentsList.Count > 0)
            //{
            //    DepartmentVM dept = new DepartmentVM();
            //    dept.DepartmentId = 0;
            //    dept.DepartmentName = "Show: All News";
            //    rs.DepartmentsList.Insert(0, dept);
            //}
            return rs;
        }

        public FrontEndVM GetAllDepartmentsForVacancies()
        {
            var rs = new FrontEndVM();
            rs.EndUserDepartmentList = GetAllEndUserDepartmentList("usp_GetAllDepartments");
            rs.PostalCodesList = GetAllLocationList("usp_GetAllLocations");
            rs.DepartmentsList = GetAllDepartmentList("usp_GetAllDepartments");
            rs.VacancyDepartmentsList = GetAllVacancyDepartmentList("usp_GetAllVacancyDepartments");
            //if (rs.DepartmentsList.Count > 0)
            //{
            //    DepartmentVM dept = new DepartmentVM();
            //    dept.DepartmentId = 0;
            //    dept.DepartmentName = "Show: All Vacancies";
            //    rs.DepartmentsList.Insert(0, dept);
            //}
            return rs;
        }

        public FrontEndVM GetNewsListBydepartmentId(int departmentId)
        {
            var rs = new FrontEndVM();

            rs.EndUserDepartmentList = GetAllEndUserDepartmentList("usp_GetAllDepartments");

            object param = new { departmentId = departmentId };

            rs.NewsList = Connection.Query<News>("usp_GetNewsBydepartmentIdFrontEnd_Modified", param, null, false, null, CommandType.StoredProcedure).AsList();

            return rs;
        }

        public FrontEndVM GetVacancyListBydepartmentId(int departmentId)
        {
            var rs = new FrontEndVM();

            rs.EndUserDepartmentList = GetAllEndUserDepartmentList("usp_GetAllDepartments");

            object param = new { departmentId = departmentId };

            rs.VacancyList = Connection.Query<Vacancy>("usp_GetVacanciesBydepartmentIdFrontEnd", param, null, false, null, CommandType.StoredProcedure).AsList();

            return rs;
        }


        public List<PostalCode> GetAllLocationList(string query)
        {
            try
            {
                List<PostalCode> PostalCodesList = new List<PostalCode>();
                PostalCodesList = Connection.Query<PostalCode>(query, commandType: CommandType.StoredProcedure).ToList<PostalCode>();
                PostalCode postalCode = new PostalCode();
                postalCode.Code = "Select...";
                PostalCodesList.Insert(0, postalCode);
                return PostalCodesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateEndUserProfile(FrontEndUser user)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateEndUserProfile", SqlCommandType.StoredProcedure,
                    new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        SurName = user.SurName,
                        JobTitle = user.JobTitle,
                        Location = user.Location,
                        TelephoneNumber = user.TelephoneNumber,
                        Mobile = user.Mobile,
                        Photo = user.Photo,
                        MyDepartmentName = user.MyDepartmentName,
                        MyDepartmentId = user.MyDepartmentId,
                        EmailAddress = user.EmailAddress,
                        WindowsUserId = user.WindowsUserId
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult CreateEndUserProfile(FrontEndUser user)
        {
            
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateEndUserProfile", SqlCommandType.StoredProcedure,
                    new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        SurName = user.SurName,
                        JobTitle = user.JobTitle,
                        Location = user.Location,
                        TelephoneNumber = user.TelephoneNumber,
                        Mobile = user.Mobile,
                        Photo = user.Photo,
                        MyDepartmentName = user.MyDepartmentName,
                        MyDepartmentId = user.MyDepartmentId,
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int CreateEndUserProfile_New(FrontEndUser user)
        {

            try
            {
                int result = this.ExecuteQuery<int>("usp_CreateEndUserProfile", SqlCommandType.StoredProcedure,
                    new
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        SurName = user.SurName,
                        JobTitle = user.JobTitle,
                        Location = user.Location,
                        TelephoneNumber = user.TelephoneNumber,
                        Mobile = user.Mobile,
                        Photo = user.Photo,
                        MyDepartmentName = user.MyDepartmentName,
                        MyDepartmentId = user.MyDepartmentId,
                        EmailAddress = user.EmailAddress,
                        WindowsUserId = user.WindowsUserId,
                        EmployeeId = user.EmployeeId
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EndUserDepartment> GetAllEndUserDepartmentList(string query)
        {
            try
            {
                List<EndUserDepartment> endUserDepartmentList = new List<EndUserDepartment>();
                endUserDepartmentList = Connection.Query<EndUserDepartment>(query, commandType: CommandType.StoredProcedure).ToList<EndUserDepartment>();
                if (endUserDepartmentList.Count > 0)
                {
                    EndUserDepartment endUserDepartment = new EndUserDepartment();
                    endUserDepartment.DepartmentId = 0;
                    endUserDepartment.DepartmentName = "Select Department";
                    endUserDepartmentList.Insert(0, endUserDepartment);
                }
                return endUserDepartmentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DepartmentVM> GetAllDepartmentList(string query)
        {
            try
            {
                List<DepartmentVM> DepartmentVMList = new List<DepartmentVM>();
                DepartmentVMList = Connection.Query<DepartmentVM>(query, commandType: CommandType.StoredProcedure).ToList<DepartmentVM>();

                return DepartmentVMList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DocumentGridFrontEnd> GetFeaturedDocumentList(string query, object param)
        {
            try
            {
                List<DocumentGridFrontEnd> rs = new List<DocumentGridFrontEnd>();
                rs = Connection.Query<DocumentGridFrontEnd>(query, param, null, false, null, CommandType.StoredProcedure).ToList<DocumentGridFrontEnd>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public BaseResult SaveMyDepartment(int deptId, int userId)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_SaveMyDepartment", SqlCommandType.StoredProcedure,
                    new
                    {
                        DeptId = deptId,
                        UserId = userId,

                    }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult AddMyFavouriteDocument(int docId, int userId)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddMyFavouriteDocument", SqlCommandType.StoredProcedure,
                    new
                    {
                        DocId = docId,
                        UserId = userId,

                    }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public GazetersDetail GetSelectedPostalCodeDetail(string query, object param)
        {
            try
            {
                GazetersDetail rs = new GazetersDetail();
                rs = Connection.Query<GazetersDetail>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<News> GetAllNews(string query)
        {
            try
            {
                List<News> rs = new List<News>();
                rs = Connection.Query<News>(query, commandType: CommandType.StoredProcedure).ToList<News>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<News> GetAllFrontEndNewsList(string query)
        {
            try
            {
                List<News> rs = new List<News>();
                rs = Connection.Query<News>(query, commandType: CommandType.StoredProcedure).ToList<News>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public News GetNewsById(string query, object param)
        {
            try
            {
                News rs = new News();
                rs = Connection.Query<News>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Vacancy> GetAllVacancies(string query)
        {
            try
            {
                List<Vacancy> rs = new List<Vacancy>();
                rs = Connection.Query<Vacancy>(query, commandType: CommandType.StoredProcedure).ToList<Vacancy>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Vacancy GetVacancyById(string query, object param)
        {
            try
            {
                Vacancy rs = new Vacancy();
                rs = Connection.Query<Vacancy>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DepartmentVM GetDepartmentValuesById(string query, object param)
        {
            try
            {
                DepartmentVM rs = new DepartmentVM();
                rs = Connection.Query<DepartmentVM>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OurValues GetOurValues(string query)
        {
            try
            {
                OurValues rs = new OurValues();
                rs = Connection.Query<OurValues>(query, null, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FeatureMessages GetFeatureMessageDetails(string query)
        {
            try
            {
                FeatureMessages rs = new FeatureMessages();
                rs = Connection.Query<FeatureMessages>(query, null, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<News> GetNewsByDepartmentWise(string query, object param)
        {
            try
            {
                var rs = Connection.Query<News>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<News> GetAllNewsListExceptSelectedDepartmentById(string query, object param)
        {
            try
            {
                var rs = Connection.Query<News>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public FrontEndVM GetFrontEndViewHomePageDetails(string query)
        //{
        //    try
        //    {
        //        FrontEndVM rs = new FrontEndVM();
        //        //using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
        //        //{
        //        //    //rs.PostalCodesList = multi.Read<PostalCode>().ToList<PostalCode>();
        //        //    //rs.NewsList = multi.Read<News>().ToList<News>();
        //        //    //rs.FeatureMessagesDetail = multi.Read<FeatureMessages>().FirstOrDefault();
        //        //    //rs.DepartmentsList = multi.Read<DepartmentVM>().ToList<DepartmentVM>();

        //        //}

        //        //PostalCode postalCode = new PostalCode();
        //        //postalCode.Code = "Select...";
        //        //rs.PostalCodesList.Insert(0, postalCode);

        //        return rs;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<MonthStarDetails> GeetMonthStarList(string query)
        {
            try
            {
                List<MonthStarDetails> rs = new List<MonthStarDetails>();
                //int currentMonthNumber = System.DateTime.Now.Month;
                //rs = Connection.Query<MonthStarDetails>(query, new { MonthNumber = currentMonthNumber }, null, false, null, CommandType.StoredProcedure).ToList<MonthStarDetails>();
                rs = Connection.Query<MonthStarDetails>(query, null, null, false, null, CommandType.StoredProcedure).ToList<MonthStarDetails>();


                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentGridFrontEnd> GetFavouriteDocument(string query, object param)
        {
            try
            {
                List<DocumentGridFrontEnd> docs = new List<DocumentGridFrontEnd>();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    docs = multi.Read<DocumentGridFrontEnd>().AsList();

                }

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveFromFavouriteList(string query, object param)
        {
            try
            {

                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {


                }

                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Documents
        public List<DocumentGridFrontEnd> GetAllDocuments(string query, object param)
        {
            try
            {
                List<DocumentGridFrontEnd> docs = new List<DocumentGridFrontEnd>();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    docs = multi.Read<DocumentGridFrontEnd>().AsList();

                }

                foreach (var item in docs)
                {
                    item.FileSize = SizeSuffix(Convert.ToInt64(item.FileSize), 2);
                }

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentGridFrontEnd> GetAllDocumentsTitle(string query)
        {
            try
            {
                List<DocumentGridFrontEnd> docs = new List<DocumentGridFrontEnd>();
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    docs = multi.Read<DocumentGridFrontEnd>().AsList();

                }

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FAQS
        public List<DocumentGridFrontEnd> GetSearchFAQsAttachedDocument(string query, object param)
        {
            try
            {
                List<DocumentGridFrontEnd> docs = new List<DocumentGridFrontEnd>();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    docs = multi.Read<DocumentGridFrontEnd>().AsList();

                }
                foreach (var item in docs)
                {
                    item.FileSize = SizeSuffix(Convert.ToInt64(item.FileSize), 2);
                }
                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<FAQGridVM> GetSearchFAQs(string query, object param)
        {
            try
            {
                List<FAQGridVM> faqs = new List<FAQGridVM>();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    faqs = multi.Read<FAQGridVM>().AsList();

                }

                return faqs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FAQGridVM> GetFaqsQuestions(string query)
        {
            try
            {
                List<FAQGridVM> faqs = new List<FAQGridVM>();
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    faqs = multi.Read<FAQGridVM>().AsList();

                }

                return faqs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Departments

        public DocumentGridItemVM GetDocumentListByDepartmentId(string query, object param)
        {
            try
            {
                DocumentGridItemVM docs = new DocumentGridItemVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    docs.Documents = multi.Read<DocumentDetail>().AsList();
                    docs.Folders = multi.Read<FolderDetails>().AsList();
                }

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<News> GetAllNewsForDocuments(string query, object param)
        {
            try
            {
                FrontEndVM rs = new FrontEndVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs.NewsList = multi.Read<News>().ToList<News>();
                }

                return rs.NewsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DepartmentVM> GetAllDepartMentsForMenu(string query)
        {
            try
            {
                FrontEndVM rs = new FrontEndVM();
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    rs.DepartmentsList = multi.Read<DepartmentVM>().ToList<DepartmentVM>();
                }

                return rs.DepartmentsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region search by Person

        public List<FrontEndUser> GetPersonsList(string query, object param)
        {
            try
            {
                List<FrontEndUser> userList = new List<FrontEndUser>();

                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    userList = multi.Read<FrontEndUser>().AsList();

                }


                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

        #region Leave Request
        public BaseResult SaveLeaveRequest(LeaveRequest leavevm)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddLeaveRequest", SqlCommandType.StoredProcedure,
                    new
                    {
                        StartDate = leavevm.LeaveStartDate,
                        StartTime = leavevm.StartTime,
                        EndDate = leavevm.LeaveEndDate,
                        EndTime = leavevm.EndTime,
                        ReturnBackDate = leavevm.ReturnBackDate,
                        FileName = leavevm.Filename,
                        UserID = leavevm.EndUserId,
                        LeaveTypeID = leavevm.SelectedLeaveTypeId,
                        LeaveID = leavevm.LeaveId,
                        Description = leavevm.Description,
                        Status = leavevm.Status,
                        Quantity = leavevm.Quantity,
                        Department = leavevm.Department
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsLeaveBalanceExist(string query, object param)
        {
            try
            {
                bool isExist = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsExistBankHolidayByDate(string query, object param)
        {
            try
            {
                bool isExist = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        List<LeaveRequestType> IFrontEndRepository.GetAllLeavetypes(string query)
        {
            List<LeaveRequestType> obj = new List<LeaveRequestType>();
            try
            {
                obj = Connection.Query<LeaveRequestType>(query, commandType: CommandType.StoredProcedure).ToList<LeaveRequestType>();
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        List<LeaveRequestType> IFrontEndRepository.GetAllBankHolidays(string query)
        {
            List<LeaveRequestType> obj = new List<LeaveRequestType>();
            try
            {
                obj = Connection.Query<LeaveRequestType>(query, commandType: CommandType.StoredProcedure).ToList<LeaveRequestType>();
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public LeaveRequest GetALeaveDetailByID(string query,object param)
        {
            LeaveRequest obj = new LeaveRequest();
            try
            {
                obj = Connection.Query<LeaveRequest>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       

        List<LeaveRequest> IFrontEndRepository.GetAllBookedLeaves(string query, object param)
        {
            List<LeaveRequest> obj = new List<LeaveRequest>();
            try
            {
                obj = Connection.Query<LeaveRequest>(query,param, null, false, null, CommandType.StoredProcedure).ToList<LeaveRequest>();
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public decimal CountBankHolidays(string query, object param)
        {
            decimal obj = 0;
            try
            {
                obj = Connection.Query<decimal>(query,param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Manoj
        public List<LeaveManagement> GetPendingLeaveRequests(string query, object param)
        {
            var pendingLeaveReuests = new List<LeaveManagement>();
            try
            {
                pendingLeaveReuests = Connection.Query<LeaveManagement>(query, param, null, false, null, CommandType.StoredProcedure).AsList();
                return pendingLeaveReuests;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<LeaveManagement> GetApprovedLeaveRequests(string query, object param)
        {
            var pendingLeaveReuests = new List<LeaveManagement>();
            try
            {
                pendingLeaveReuests = Connection.Query<LeaveManagement>(query, param, null, false, null, CommandType.StoredProcedure).AsList();
                return pendingLeaveReuests;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public BaseResult CancelePendingLeaveRequest(string query, int leaveId)
        {
            var result = this.ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure,
                   new
                   {
                       LeaveId = leaveId,
                       LeaveStatus = "Cancelled",
                   }).FirstOrDefault();

            return result;
        }

        public LeaveCount GetHolidayEntitlement(string query, int userId)
        {
            LeaveCount result = this.ExecuteQuery<LeaveCount>(query, SqlCommandType.StoredProcedure, new { UserId = userId }).FirstOrDefault();
            return result;
        }
#endregion
    }
}
