using Dapper;
using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Doctor;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.MedicineDose;
using DoseBookAdmin.WebAdmin.Common.Repository;
using DoseBookAdmin.WebAdmin.Doctor.Mapping;
using DoseBookAdmin.WebAdmin.MedicineDose.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Repository
{
    public class MedicineDoseDao : Dao, IMedicineDoseDao
    {

        /// <summary>
        /// Private DoctorMapping Data Member
        /// </summary>
        private DoctorMapping _doctorMapping;

        /// <summary>
        /// Private MedicineDoseMapping Data Member
        /// </summary>
        private MedicineDoseMapping _medicineDoseMapping;

        public MedicineDoseDao(IDbConnection connection) : base(connection)
        {
            _doctorMapping = new DoctorMapping();
            _medicineDoseMapping = new MedicineDoseMapping();
        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public MedicineDoseEntityList GetMedicineDoseByDoctorWise(string query, object param)
        {
            try
            {
                List<MedicineDoseEntity> medicineDoseEntityList = Connection.Query<MedicineDoseEntity>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                MedicineDoseEntityList MedicineDoseEntityList = _medicineDoseMapping.ListOfEntity2EntityList(medicineDoseEntityList);
                return MedicineDoseEntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseSimulationEntity GetMedicineDoseDataToCreateMedicineDose(string query)
        {
            MedicineDoseSimulationEntity rs = new MedicineDoseSimulationEntity();
            try
            {
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    rs.MedicineDoseEntity = new MedicineDoseEntity();

                    List<DoctorEntity> doctorEntityList = multi.Read<DoctorEntity>().AsList();
                    doctorEntityList.Insert(0, new DoctorEntity { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.DoctorEntityList = _doctorMapping.ListOfEntity2EntityList(doctorEntityList);

                    rs.FrequencyList = multi.Read<string>().AsList();
                    rs.FrequencyList.Insert(0, "Select Frequency");

                    rs.DoseList = multi.Read<string>().AsList();
                    rs.DoseList.Insert(0, "Select Dose");

                    rs.DoseUnitList = multi.Read<string>().AsList();
                    rs.DoseUnitList.Insert(0, "Select Dose Unit");

                    rs.DirectionList = multi.Read<string>().AsList();
                    rs.DirectionList.Insert(0, "Select Direction");

                    rs.DurationList = multi.Read<string>().AsList();
                    rs.DurationList.Insert(0, "Select Duration");



                    //List<MedicineDoseEntity> medicineDoseEntityList = multi.Read<MedicineDoseEntity>().AsList();
                    //medicineDoseEntityList.Insert(0, new MedicineDoseEntity() { MedicineId = 0, MedicineName = "Select Medicine Name" });

                    //rs.MedicineDoseEntityList = _medicineDoseMapping.MedicineDoseEntityList2MedicineDoseEntitiesList(medicineDoseEntityList);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return rs;
        }

        public List<string> GetSearchedMedicineDoseList(string query, object param)
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

        public bool IsMedicineExist(string query, object param)
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

        public MedicineDoseEntity GetMedicineDetailByMedicineName(string query, object param)
        {
            try
            {
                MedicineDoseEntity medicineDose = Connection.Query<MedicineDoseEntity>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return medicineDose;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity SaveMedicineDose(string query, object param)
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

        public MedicineDoseSimulationEntity GetMedicineDoseById(string query, object param)
        {
            try
            {
                MedicineDoseSimulationEntity rs = new MedicineDoseSimulationEntity();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs.MedicineDoseEntity = multi.Read<MedicineDoseEntity>().SingleOrDefault();

                    List<DoctorEntity> doctorEntityList = multi.Read<DoctorEntity>().AsList();
                    doctorEntityList.Insert(0, new DoctorEntity { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.DoctorEntityList = _doctorMapping.ListOfEntity2EntityList(doctorEntityList);

                    rs.FrequencyList = multi.Read<string>().AsList();
                    rs.FrequencyList.Insert(0, "Select Frequency");

                    rs.DoseList = multi.Read<string>().AsList();
                    rs.DoseList.Insert(0, "Select Dose");

                    rs.DoseUnitList = multi.Read<string>().AsList();
                    rs.DoseUnitList.Insert(0, "Select Dose Unit");

                    rs.DirectionList = multi.Read<string>().AsList();
                    rs.DirectionList.Insert(0, "Select Direction");

                    rs.DurationList = multi.Read<string>().AsList();
                    rs.DurationList.Insert(0, "Select Duration");
                }

                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultEntity UpdateMedicineDose(string query, object param)
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

        public BaseResultEntity DeleteMedicineDoseById(string query, object param)
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

        public string GetSearchedDoctorProblemTagsList(string query)
        {
            try
            {
                string result = Connection.Query<string>(query, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
