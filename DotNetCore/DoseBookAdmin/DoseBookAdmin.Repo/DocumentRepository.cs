using Dapper;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Document;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class DocumentRepository : Repository, IDocumentRepository
    {
        public DocumentRepository(IDbConnection connection) : base(connection)
        {
        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public DocumentListGridItemVM GetDocumentByDoctorWise(string query, object param)
        {
            try
            {
                DocumentListGridItemVM rs = new DocumentListGridItemVM();
                rs.AllDocuments = Connection.Query<Document>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Document GetDocumentDataToCreateDocument(string query)
        {
            Document rs = new Document();
            try
            {
                using (var multi = Connection.QueryMultiple(query, null, null, null, CommandType.StoredProcedure))
                {
                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.AllClassificationResult = multi.Read<ClassificationResult>().AsList();
                    rs.AllClassificationResult.Insert(0, new ClassificationResult { ClassificationResultId = 0, ClassificationResultName = "Select Classification Result" });
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return rs;
        }

        public BaseResult DeleteDocumentByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { DocumentIds = IdsWithDelimitedPipeline }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Document GetDocumentById(string query, object param)
        {
            try
            {
                Document rs = new Document();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    rs = multi.Read<Document>().SingleOrDefault();

                    rs.AllDoctors = multi.Read<Doctor>().AsList();
                    rs.AllDoctors.Insert(0, new Doctor { DoctorId = 0, DoctorName = "Select Doctor" });

                    rs.AllClassificationResult = multi.Read<ClassificationResult>().AsList();
                    rs.AllClassificationResult.Insert(0, new ClassificationResult { ClassificationResultId = 0, ClassificationResultName = "Select Classification Result" });
                }

                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveDocument(string query, object param)
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

        public BaseResult UpdateDocument(string query, object param)
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
