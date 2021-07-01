using Dapper;
using DoseBookAdmin.Entity.Doctor;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.Doctor.Repository
{
    public class DoctorDao : Dao, IDoctorDao
    {
        public DoctorDao(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public DoctorEntityList GetAllDoctors(string query)
        {
            try
            {
                List<DoctorEntity> doctorList = Connection.Query<DoctorEntity>(query, commandType: CommandType.StoredProcedure).ToList();
                DoctorEntityList doctorEntityList = new DoctorEntityList();
                doctorEntityList.AddRange(doctorList);
                return doctorEntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedDoctorNameList(string query, object param)
        {
            try
            {
                var result = Connection.Query<string>(query, param, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDoctorNameExist(string query, object param)
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

        public BaseResultEntity SaveDoctor(string query, object param)
        {
            try
            {
                var result = Connection.Query<BaseResultEntity>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity UpdateDoctor(string query, object param)
        {
            try
            {
                var result = Connection.Query<BaseResultEntity>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity DeleteDoctorById(string query, object param)
        {
            try
            {
                var result = Connection.Query<BaseResultEntity>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
