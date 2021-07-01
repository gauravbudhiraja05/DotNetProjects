using Dapper;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class MedicineDoseRepository : Repository, IMedicineDoseRepository
    {
        public MedicineDoseRepository(IDbConnection connection) : base(connection)
        {
        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public MedicineDoseListGridItemVM GetMedicineDoseByDoctorWise(string query, object param)
        {
            try
            {
                MedicineDoseListGridItemVM rs = new MedicineDoseListGridItemVM();
                rs.AllMedicineDoses = Connection.Query<MedicineDose>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDose GetMedicineDoseDataToCreateMedicineDose(string query)
        {
            MedicineDose rs = new MedicineDose();
            try
            {
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.DoseUnitList = multi.Read<string>().AsList();
                    rs.DoseUnitList.Insert(0, "Select Dose Unit");

                    rs.FrequencyList = multi.Read<string>().AsList();
                    rs.FrequencyList.Insert(0, "Select Frequency");

                    rs.DirectionList = multi.Read<string>().AsList();
                    rs.DirectionList.Insert(0, "Select Direction");

                    rs.DurationList = multi.Read<string>().AsList();
                    rs.DurationList.Insert(0, "Select Duration");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return rs;
        }

        public BaseResult DeleteMedicineDoseByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { MedicinesIds = IdsWithDelimitedPipeline }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDose GetMedicineDoseById(string query, object param)
        {
            try
            {
                MedicineDose rs = new MedicineDose();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs = multi.Read<MedicineDose>().SingleOrDefault();

                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.DoseUnitList = multi.Read<string>().AsList();
                    rs.DoseUnitList.Insert(0, "Select Dose Unit");

                    rs.FrequencyList = multi.Read<string>().AsList();
                    rs.FrequencyList.Insert(0, "Select Frequency");

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

        public BaseResult SaveMedicineDose(string query, object param)
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

        public BaseResult UpdateMedicineDose(string query, object param)
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
