using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PickfordsIntranet.ViewModels.Vacancy;
using System.IO;

namespace PickfordsIntranet.Services
{
    public class VacancyService : IVacancyService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        private IPathProvider _pathProvider;

        /// <summary>
        /// NewsService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public VacancyService(IUnitOfWork unitOfWork, IPathProvider pathProvider)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _pathProvider = pathProvider;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public VacancyListGridItemVM GetAllDepartments()
        {
            try
            {
                var result = _unitOfWork.VacancyRepo.GetAllDepartments("usp_GetAllDepartments");
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacancyListGridItemVM GetUserDepartmentDetailbyUserId(int userId)
        {
            try
            {
                var result = _unitOfWork.VacancyRepo.GetUserDepartmentDetailbyUserId("usp_GetUserDepartmentDetailbyUserId", new { userId = userId });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacancyListGridItemVM GetVacancyByDepartmentWise(int departmentId)
        {
            try
            {
                var result = _unitOfWork.VacancyRepo.GetVacancyByDepartmentWise("usp_GetVacancyByDepartmentId", new { deptid = Convert.ToInt32(departmentId) });
                DepartmentVM seledtedDepartmentDetail = new DepartmentVM();
                seledtedDepartmentDetail.DepartmentId = departmentId;
                var result1 = _unitOfWork.VacancyRepo.GetAllDepartments("usp_GetAllDepartments");
                if (result1.AllDepartments.Count > 0)
                {
                    seledtedDepartmentDetail.DepartmentName = result1.AllDepartments.Where(dept => dept.DepartmentId == departmentId).FirstOrDefault().DepartmentName;
                    result.SelectedDepartmentDetails = seledtedDepartmentDetail;
                }

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacancyVM GetVacancyDataToCreateVacancy(UserActionLoggingDetails userActionLoggingDetails)
        {
            try
            {
                VacancyVM vacancyModal = _unitOfWork.VacancyRepo.GetPreRequisitesDataToCreateVacancy("usp_GetPreRequisitesDataToCreateVacancy", new { UserId = Convert.ToInt32(userActionLoggingDetails.UserId) });

                return vacancyModal;
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
                var result = _unitOfWork.VacancyRepo.SaveVacancy(vacancy);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VacancyVM GetVacancyById(int id)
        {
            try
            {
                VacancyVM vacancy = _unitOfWork.VacancyRepo.GetVacancyById("usp_GetVacancyById", new { Id = id });
                return vacancy;
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
                var result = _unitOfWork.VacancyRepo.UpdateVacancy(vacancy);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult DeleteVacancyByIds(DeleteItemVM targetIds)
        {
            try
            {
                DeleteImagesForEachVacancyDeleted(targetIds);
                BaseResult result = _unitOfWork.VacancyRepo.DeleteVacancyByIds("usp_DeleteVacancyByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteImagesForEachVacancyDeleted(DeleteItemVM targetIds)
        {
            try
            {
                var result = _unitOfWork.VacancyRepo.DeleteImagesForEachVacancyDeleted("usp_GetImageNamesForVacancy", targetIds);

                foreach (var item in result)
                {
                    var thumbNailImage = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + item.Thumbnailimage;
                    var mainImage = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + item.MainImage;
                    var additionalImage1 = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + item.AdditionalImage1;
                    var additionalImage2 = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + item.AdditionalImage2;

                    if (File.Exists(thumbNailImage))
                    {
                        File.Delete(thumbNailImage);
                    }

                    if (File.Exists(mainImage))
                    {
                        File.Delete(mainImage);
                    }

                    if (File.Exists(additionalImage1))
                    {
                        File.Delete(additionalImage1);
                    }

                    if (File.Exists(additionalImage2))
                    {
                        File.Delete(additionalImage2);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
