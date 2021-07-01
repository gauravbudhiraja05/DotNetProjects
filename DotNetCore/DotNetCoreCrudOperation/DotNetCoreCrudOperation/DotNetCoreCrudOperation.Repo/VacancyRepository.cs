using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.ViewModels.Vacancy;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// AdminRepository implements the IAdminRepository and Extends the generic behabiour of User Repository
    /// </summary>
    public class VacancyRepository : Repository, IVacancyRepository
    {
        /// <summary>
        /// AdminRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public VacancyRepository(IDbConnection connection) : base(connection)
        {

        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public VacancyListGridItemVM GetAllDepartments(string query)
        {
            try
            {
                VacancyListGridItemVM rs = new VacancyListGridItemVM();
                rs.AllDepartments = Connection.Query<DepartmentVM>(query, commandType: CommandType.StoredProcedure).ToList<DepartmentVM>();
                if (rs.AllDepartments.Count > 0)
                {
                    DepartmentVM dept = new DepartmentVM();
                    dept.DepartmentId = 0;
                    dept.DepartmentName = "All";
                    rs.AllDepartments.Insert(0, dept);
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public VacancyListGridItemVM GetVacancyByDepartmentWise(string query, object param)
        {
            try
            {
                VacancyListGridItemVM rs = new VacancyListGridItemVM();
                rs.AllVacancyListGridItems = Connection.Query<VacancyListGridItems>(query, param, null, false, null, CommandType.StoredProcedure).ToList<VacancyListGridItems>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacancyListGridItemVM GetUserDepartmentDetailbyUserId(string query, object param)
        {
            try
            {
                VacancyListGridItemVM rs = new VacancyListGridItemVM();
                rs.SelectedDepartmentDetails = Connection.Query<DepartmentVM>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public VacancyVM GetPreRequisitesDataToCreateVacancy(string query, object param)
        {
            try
            {
                VacancyVM vacancy = new VacancyVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    vacancy.AllDepartments = multi.Read<DepartmentVM>().AsList();
                    vacancy.AuthorName = multi.Read<string>().SingleOrDefault();
                }

                vacancy.CreationDate = DateTime.Now.ToString("MM/dd/yyyy");
                //vacancy.PublishDateDisplay = DateTime.Now.ToString("MM/dd/yyyy");
                return vacancy;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveVacancy(VacancyVM vacancy)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddVacancy", SqlCommandType.StoredProcedure,
                    new
                    {
                        Title = vacancy.Title,
                        TeaserText = vacancy.TeaserText,
                        Content1 = vacancy.Content1,
                        Content2 = vacancy.Content2,
                        DepartmentId = vacancy.DepartmentId,
                        //IsFeatureOnHomePage = vacancy.IsFeatureOnHomePage,
                        ThumbnailImage = vacancy.ThumbnailImage,
                        MainImage = vacancy.MainImage,
                        AdditionalImage1 = vacancy.AdditionalImage1,
                        AdditionalImage2 = vacancy.AdditionalImage2,
                        PublishDate = vacancy.PublishDateDisplay,
                        AuthorName = vacancy.AuthorName,
                        IsActive = vacancy.IsActive,
                        CreatedBy = vacancy.CreatedBy
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateVacancy(VacancyVM vacancy)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateVacancy", SqlCommandType.StoredProcedure,
                    new
                    {
                        Id = vacancy.Id,
                        Title = vacancy.Title,
                        TeaserText = vacancy.TeaserText,
                        Content1 = vacancy.Content1,
                        Content2 = vacancy.Content2,
                        DepartmentId = vacancy.DepartmentId,
                        //IsFeatureOnHomePage = vacancy.IsFeatureOnHomePage,
                        ThumbnailImage = vacancy.ThumbnailImage,
                        MainImage = vacancy.MainImage,
                        AdditionalImage1 = vacancy.AdditionalImage1,
                        AdditionalImage2 = vacancy.AdditionalImage2,
                        PublishDate = vacancy.PublishDateDisplay,
                        AuthorName = vacancy.AuthorName,
                        IsActive = vacancy.IsActive,
                        CreatedBy = vacancy.CreatedBy,
                        ModifiedBy = vacancy.ModifiedBy,
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public VacancyVM GetVacancyById(string query, object param)
        {
            try
            {
                VacancyVM vacancy = new VacancyVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    vacancy = multi.Read<VacancyVM>().SingleOrDefault();
                    vacancy.AllDepartments = multi.Read<DepartmentVM>().AsList();
                }

                return vacancy;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteVacancyByIds(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { VacancyIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ImageNamesVM> DeleteImagesForEachVacancyDeleted(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                List<ImageNamesVM> vacancy = new List<ImageNamesVM>();
                vacancy = Connection.Query<ImageNamesVM>(query, new { VacancyIds = IdsWithDelimitedPipeline }, null, false, null, CommandType.StoredProcedure).ToList<ImageNamesVM>();
                return vacancy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
