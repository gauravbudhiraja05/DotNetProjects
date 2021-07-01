using Dapper;
using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.Test;
using DoseBookAdmin.WebAdmin.Common.Repository;
using DoseBookAdmin.WebAdmin.Test.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.Test.Repository
{
    public class TestDao : Dao, ITestDao
    {
        /// <summary>
        /// Private TestMapping Data Member
        /// </summary>
        private TestMapping _testMapping;

        public TestDao(IDbConnection connection) : base(connection)
        {
            _testMapping = new TestMapping();
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public TestEntityList GetTestListByDoctorWise(string query, object param)
        {
            try
            {
                List<TestEntity> testEntityList = Connection.Query<TestEntity>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                TestEntityList TestEntityList = _testMapping.ListOfEntity2EntityList(testEntityList);
                return TestEntityList;
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

        public List<string> GetSearchedTestList(string query, object param)
        {
            try
            {
                var result = ExecuteQuery<string>(query, SqlCommandType.StoredProcedure, param).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsTestExist(string query, object param)
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

        public BaseResultEntity SaveTest(string query, object param)
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

        public BaseResultEntity UpdateTest(string query, object param)
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

        public BaseResultEntity DeleteTestById(string query, object param)
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
