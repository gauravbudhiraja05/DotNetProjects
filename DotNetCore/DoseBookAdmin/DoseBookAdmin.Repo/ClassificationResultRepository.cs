using Dapper;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class ClassificationResultRepository : Repository, IClassificationResultRepository
    {
        public ClassificationResultRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public ClassificationResultListGridItemVM GetClassificationResultByDoctorWise(string query, object param)
        {
            try
            {
                ClassificationResultListGridItemVM rs = new ClassificationResultListGridItemVM();
                rs.AllClassificationResults = Connection.Query<ClassificationResult>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClassificationResult GetClassificationResultDataToCreateClassificationResult(string query)
        {
            ClassificationResult rs = new ClassificationResult();
            try
            {
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.ClassificationTypeList = multi.Read<ClassificationType>().AsList();
                    rs.ClassificationTypeList.Insert(0, new ClassificationType()
                    { Id = 0, Type = "Select Classification Type" });

                    rs.DiagnosticTestList = multi.Read<DiagnosticTest>().AsList();
                    rs.DiagnosticTestList.Insert(0, new DiagnosticTest()
                    { TestId = 0, TestName = "Select Diagnostic Test" });

                    rs.MiscSuggestionList = multi.Read<MiscSuggestion>().AsList();
                    rs.MiscSuggestionList.Insert(0, new MiscSuggestion()
                    { TestId = 0, TestName = "Select Misc Suggestion" });

                    rs.MedicineDoseList = multi.Read<MedicineDose>().AsList();
                    rs.MedicineDoseList.Insert(0, new MedicineDose()
                    { MedicineId = 0, MedicineName = "Select Medicine Dose" });
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return rs;
        }

        public BaseResult SaveClassificationResult(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClassificationResult GetClassificationResultById(string query, object param)
        {
            try
            {
                ClassificationResult rs = new ClassificationResult();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs = multi.Read<ClassificationResult>().SingleOrDefault();

                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.ClassificationTypeList = multi.Read<ClassificationType>().AsList();
                    rs.ClassificationTypeList.Insert(0, new ClassificationType { Id = 0, Type = "Select Classification Type" });

                    if (rs.ClassificationTypeId == 1)
                    {
                        rs.DiagnosticTestList = multi.Read<DiagnosticTest>().AsList();
                        rs.DiagnosticTestList.Insert(0, new DiagnosticTest { TestId = 0, TestName = "Select Diagnostic Test" });
                    }

                    else if (rs.ClassificationTypeId == 2)
                    {
                        rs.MedicineDoseList = multi.Read<MedicineDose>().AsList();
                        rs.MedicineDoseList.Insert(0, new MedicineDose { MedicineId = 0, MedicineName = "Select Medicine Dose" });
                    }

                    else if (rs.ClassificationTypeId == 3)
                    {
                        rs.MiscSuggestionList = multi.Read<MiscSuggestion>().AsList();
                        rs.MiscSuggestionList.Insert(0, new MiscSuggestion { TestId = 0, TestName = "Select Misc Suggestion" });
                    }
                }

                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateClassificationResult(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult DeleteClassificationResultByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { ClassificationResultIds = IdsWithDelimitedPipeline }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
