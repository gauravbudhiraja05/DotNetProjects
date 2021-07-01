using Dapper;
using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.PrescriptionMeta;
using DoseBookAdmin.WebAdmin.Common.Repository;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Repository
{
    public class PrescriptionMetaDao : Dao, IPrescriptionMetaDao
    {
        /// <summary>
        /// Private PrescriptionMetaMapping Data Member
        /// </summary>
        private PrescriptionMetaMapping _prescriptionMetaMapping;

        public PrescriptionMetaDao(IDbConnection connection) : base(connection)
        {
            _prescriptionMetaMapping = new PrescriptionMetaMapping();
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public PrescriptionMetaTypeEntityList GetAllPrescriptionMetaTypes(string query)
        {
            try
            {

                List<PrescriptionMetaTypeEntity> prescriptionMetaTypeEntityList = ExecuteQuery<PrescriptionMetaTypeEntity>(query, SqlCommandType.StoredProcedure).ToList();
                if (prescriptionMetaTypeEntityList.Count > 0)
                {
                    PrescriptionMetaTypeEntity prescriptionMetaTypeEntity = new PrescriptionMetaTypeEntity()
                    {
                        Id = 0,
                        Type = "All"
                    };
                    prescriptionMetaTypeEntityList.Insert(0, prescriptionMetaTypeEntity);
                }

                PrescriptionMetaTypeEntityList PrescriptionMetaTypeEntityList = _prescriptionMetaMapping.ListOfTypeEntity2TypeEntityList(prescriptionMetaTypeEntityList);
                return PrescriptionMetaTypeEntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrescriptionMetaDataEntityList GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string query, object param)
        {
            try
            {
                List<PrescriptionMetaDataEntity> prescriptionMetaDataEntityList = Connection.Query<PrescriptionMetaDataEntity>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                PrescriptionMetaDataEntityList PrescriptionMetaDataEntityList = _prescriptionMetaMapping.ListOfDataEntity2DataEntityList(prescriptionMetaDataEntityList);
                return PrescriptionMetaDataEntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity SavePrescriptionMetaData(string query, object param)
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

        public BaseResultEntity UpdatePrescriptionMetaData(string query, object param)
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

        public BaseResultEntity DeletePrescriptionMetaDataById(string query, object param)
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

        public List<string> GetSearchedTypeList(string query, object param)
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

        public bool IsTypeExist(string query, object param)
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

        public BaseResultEntity SavePrescriptionMetaType(string query, object param)
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

        public BaseResultEntity UpdatePrescriptionMetaType(string query, object param)
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

        public BaseResultEntity DeletePrescriptionMetaTypeById(string query, object param)
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
