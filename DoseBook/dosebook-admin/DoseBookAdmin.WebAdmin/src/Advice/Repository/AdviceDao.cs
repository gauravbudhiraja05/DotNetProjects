using Dapper;
using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Advice;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.WebAdmin.Advice.Mapping;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.Advice.Repository
{
    public class AdviceDao : Dao, IAdviceDao
    {
        /// <summary>
        /// Private AdviceMapping Data Member
        /// </summary>
        private AdviceMapping _adviceMapping;

        public AdviceDao(IDbConnection connection) : base(connection)
        {
            _adviceMapping = new AdviceMapping();
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public AdviceEntityList GetAdviceListByDoctorWise(string query, object param)
        {
            try
            {
                List<AdviceEntity> adviceEntityList = Connection.Query<AdviceEntity>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                AdviceEntityList AdviceEntityList = _adviceMapping.ListOfEntity2EntityList(adviceEntityList);
                return AdviceEntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSearchedDoctorProblemTagsList(string query)
        {
            try
            {
                var result = Connection.Query<string>(query, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedAdviceList(string query, object param)
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

        public bool IsAdviceExist(string query, object param)
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

        public BaseResultEntity SaveAdvice(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResultEntity>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity UpdateAdvice(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResultEntity>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity DeleteAdviceById(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<BaseResultEntity>(query, SqlCommandType.StoredProcedure, param).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
