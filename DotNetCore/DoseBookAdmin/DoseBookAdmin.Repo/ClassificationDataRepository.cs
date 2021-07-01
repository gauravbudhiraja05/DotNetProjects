using Dapper;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using System;
using System.Data;
using System.Linq;

namespace DoseBookAdmin.Repo
{
    public class ClassificationDataRepository : Repository, IClassificationDataRepository
    {

        public ClassificationDataRepository(IDbConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection; }
        }

        public ClassificationTypeListGridItemVM GetAllClassificationTypes(string query)
        {
            try
            {
                ClassificationTypeListGridItemVM rs = new ClassificationTypeListGridItemVM();
                rs.AllClassificationTypes = ExecuteQuery<ClassificationType>(query, SqlCommandType.StoredProcedure).ToList();
                if (rs.AllClassificationTypes.Count > 0)
                {
                    ClassificationType classificationType = new ClassificationType();
                    classificationType.Id = 0;
                    classificationType.Type = "Select Classification Type";
                    rs.AllClassificationTypes.Insert(0, classificationType);
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClassificationTypeListGridItemVM GetClassificationDataByClassificationTypeWise(string query, object param, int classificationTypeId)
        {
            try
            {
                ClassificationTypeListGridItemVM rs = new ClassificationTypeListGridItemVM();
                
                if (classificationTypeId == 1) // Diagnostic Test
                {
                    rs.AllDiagnosticTests = Connection.Query<DiagnosticTest>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                }
                else if (classificationTypeId == 2) // MedicineDose
                {
                    rs.AllMedicineDoses = Connection.Query<MedicineDose>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                }
                else if (classificationTypeId == 3) // Misc SUggestion
                {
                    rs.AllMiscSuggestions = Connection.Query<MiscSuggestion>(query, param, null, false, null, CommandType.StoredProcedure).ToList();
                }
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
