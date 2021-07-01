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
    public class MiscSuggestionRepository : Repository, IMiscSuggestionRepository
    {
        public MiscSuggestionRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public MiscSuggestionListGridItemVM GetMiscSuggestionByDoctorWise(string query, object param)
        {
            try
            {
                MiscSuggestionListGridItemVM rs = new MiscSuggestionListGridItemVM();
                rs.AllMiscSuggestions = Connection.Query<MiscSuggestion>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MiscSuggestion GetMiscSuggestionDataToCreateMiscSuggestion(string query)
        {
            MiscSuggestion rs = new MiscSuggestion();
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

        public BaseResult DeleteMiscSuggestionByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { MiscSuggestionIds = IdsWithDelimitedPipeline}, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MiscSuggestion GetMiscSuggestionById(string query, object param)
        {
            try
            {
                MiscSuggestion rs = new MiscSuggestion();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs = multi.Read<MiscSuggestion>().SingleOrDefault();

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

        public BaseResult SaveMiscSuggestion(string query, object param)
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

        public BaseResult UpdateMiscSuggestion(string query, object param)
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
