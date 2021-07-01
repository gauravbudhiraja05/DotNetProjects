using Dapper;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.DoseData;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class DoseDataRepository : Repository, IDoseDataRepository
    {
        public DoseDataRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public DoseMetaTypeListGridItemVM GetAllDoseMetaTypes(string query)
        {
            try
            {
                DoseMetaTypeListGridItemVM rs = new DoseMetaTypeListGridItemVM();
                rs.AllDoseMetaTypes = ExecuteQuery<DoseMetaType>(query, SqlCommandType.StoredProcedure).ToList<DoseMetaType>();
                if (rs.AllDoseMetaTypes.Count > 0)
                {
                    DoseMetaType doseMetaType = new DoseMetaType();
                    doseMetaType.Id = 0;
                    doseMetaType.Type = "Select";
                    rs.AllDoseMetaTypes.Insert(0, doseMetaType);
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoseMetaTypeListGridItemVM GetDoseDataByDoseMetaTypeWise(string query, object param)
        {
            try
            {
                DoseMetaTypeListGridItemVM rs = new DoseMetaTypeListGridItemVM();
                rs.AllDoseDatas = Connection.Query<DoseData>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoseData GetDoseDataToCreateDoseData(string query)
        {
            try
            {
                DoseData doseData = new DoseData();
                doseData.AllDoseMetaTypes = Connection.Query<DoseMetaType>(query, null, null, false, null, CommandType.StoredProcedure).ToList();
                return doseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveDoseData(DoseData doseData)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>("usp_AddDoseData", SqlCommandType.StoredProcedure,
                    new
                    {
                        doseData.DoseMetaTypeId,
                        doseData.Title,
                        doseData.IsActive,
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoseData GetDoseDataById(string query, object param)
        {
            try
            {
                DoseData doseData = new DoseData();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    doseData = multi.Read<DoseData>().SingleOrDefault();
                    doseData.AllDoseMetaTypes = multi.Read<DoseMetaType>().AsList();
                }

                return doseData;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateDoseData(DoseData doseData)
        {
            try
            {
                var result = ExecuteQuery<BaseResult>("usp_UpdateDoseData", SqlCommandType.StoredProcedure,
                    new
                    {
                        doseData.id,
                        doseData.DoseMetaTypeId,
                        doseData.Title
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult DeleteDoseDataByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { DoseDataIds = IdsWithDelimitedPipeline }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
