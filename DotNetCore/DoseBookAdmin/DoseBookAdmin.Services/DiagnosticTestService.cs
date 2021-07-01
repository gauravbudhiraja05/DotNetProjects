using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class DiagnosticTestService : IDiagnosticTestService
    {
        /// <summary>
        /// IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// MedicineDoseService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public DiagnosticTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DiagnosticTestListGridItemVM GetAllDoctors()
        {
            DiagnosticTestListGridItemVM diagnosticTestListGridItemVM = new DiagnosticTestListGridItemVM();
            diagnosticTestListGridItemVM.AllDoctors = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
            return diagnosticTestListGridItemVM;
        }


        public DiagnosticTestListGridItemVM GetDiagnosticTestByDoctorWise(int doctorId)
        {
            try
            {
                var result = _unitOfWork.DiagnosticTestRepo.GetDiagnosticTestByDoctorWise("usp_GetDiagnosticTestByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
                Doctor selectedDoctorDetail = new Doctor();
                selectedDoctorDetail.DoctorId = doctorId;
                var doctorList = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
                if (doctorList.Count > 0 && doctorId > 0)
                {
                    selectedDoctorDetail.DoctorName = doctorList.Where(doctor => doctor.DoctorId == doctorId).FirstOrDefault().DoctorName;
                    result.SelectedDoctor = selectedDoctorDetail;
                }

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DiagnosticTest GetDiagnosticTestDataToCreateDiagnosticTest()
        {
            return _unitOfWork.DiagnosticTestRepo.GetDiagnosticTestDataToCreateDiagnosticTest("usp_GetDiagnosticTestDataToCreateDiagnosticTest");
        }

        public BaseResult DeleteDiagnosticTestByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.DiagnosticTestRepo.DeleteDiagnosticTestByIds("usp_DeleteDiagnosticTestByIds", targetIds);
        }

        public DiagnosticTest GetDiagnosticTestById(int testId)
        {
            return _unitOfWork.DiagnosticTestRepo.GetDiagnosticTestById("usp_GetDiagnosticTestById", new { TestId = testId });
        }

        public BaseResult SaveDiagnosticTest(DiagnosticTest diagnosticTest)
        {
            return _unitOfWork.DiagnosticTestRepo.SaveDiagnosticTest("usp_SaveDiagnosticTest", new { diagnosticTest.TestName , diagnosticTest.DoctorId});
        }

        public BaseResult UpdateDiagnosticTest(DiagnosticTest diagnosticTest)
        {
            return _unitOfWork.DiagnosticTestRepo.UpdateDiagnosticTest("usp_UpdateDiagnosticTest", new { diagnosticTest.TestId, diagnosticTest.TestName, diagnosticTest.DoctorId });
        }
    }
}
