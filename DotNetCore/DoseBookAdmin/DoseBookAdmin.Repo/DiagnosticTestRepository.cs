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
    public class DiagnosticTestRepository : Repository, IDiagnosticTestRepository
    {
        public DiagnosticTestRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public DiagnosticTestListGridItemVM GetDiagnosticTestByDoctorWise(string query, object param)
        {
            try
            {
                DiagnosticTestListGridItemVM rs = new DiagnosticTestListGridItemVM();
                rs.AllDiagnosticTests = Connection.Query<DiagnosticTest>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DiagnosticTest GetDiagnosticTestDataToCreateDiagnosticTest(string query)
        {
            DiagnosticTest rs = new DiagnosticTest();
            try
            {
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return rs;
        }

        public BaseResult DeleteDiagnosticTestByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { DiagnosticTestIds = IdsWithDelimitedPipeline}, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DiagnosticTest GetDiagnosticTestById(string query, object param)
        {
            try
            {
                DiagnosticTest rs = new DiagnosticTest();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs = multi.Read<DiagnosticTest>().SingleOrDefault();

                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                }
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveDiagnosticTest(string query, object param)
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

        public BaseResult UpdateDiagnosticTest(string query, object param)
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
    }
}
