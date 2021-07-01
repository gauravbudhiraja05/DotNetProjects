using Dapper;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class DoctorRepository : Repository, IDoctorRepository
    {
        public DoctorRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public IEnumerable<Doctor> GetAllDoctors(string query)
        {
            try
            {
                List<Doctor> doctorList = Connection.Query<Doctor>(query, commandType: CommandType.StoredProcedure).ToList();
                return doctorList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveDoctor(string query, object param)
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

        public BaseResult DeleteDoctorsByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { DoctorIds = IdsWithDelimitedPipeline}, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Doctor GetDoctorById(string query, object param)
        {
            try
            {
                Doctor doctor = Connection.Query<Doctor>(query, param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return doctor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateDoctor(string query, object param)
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
