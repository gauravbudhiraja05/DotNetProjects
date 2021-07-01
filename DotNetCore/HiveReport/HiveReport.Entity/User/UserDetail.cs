using HiveReport.Entity.Common;
using System.Collections.Generic;

namespace HiveReport.Entity.User
{
    public class UserDetailEntity
    {
        public string RightsName { get; set; }
        public bool HasQuery { get; set; }
        public bool HasReport { get; set; }
        public ReportMasterEntityList ReportMasterEntityList { get; set; }
        public TableMasterEntityList TableMasterEntityList { get; set; }
        public ViewMasterEntityList ViewMasterEntityList { get; set; }
        public HTMLFileEntityList HTMLFileEntityList { get; set; }
        public GraphMasterEntityList GraphMasterEntityList { get; set; }
        public QueryMasterEntityList QueryMasterEntityList { get; set; }
        public string PasswordExpirationMessage { get; set; }
        public SharedLayoutEntity SharedLayoutEntity { get; set; }

    }

    public class ReportMasterEntity
    {
        public string QueryName { get; set; }
        public string SavedBy { get; set; }
        public string DepartmentID { get; set; }
        public string ClientID { get; set; }
        public string UnderLOB { get; set; }
        public string TableName { get; set; }
    }

    public class ReportMasterEntityList : List<ReportMasterEntity>
    {
        
    }

    public class TableMasterEntity
    {
        public string TableId { get; set; }
        public string TableName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class TableMasterEntityList : List<TableMasterEntity>
    {

    }

    public class ViewMasterEntity
    {
        public string ViewID { get; set; }
        public string ViewName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ViewMasterEntityList : List<ViewMasterEntity>
    {

    }

    public class HTMLFileEntity
    {
        public string AutoId { get; set; }
        public string SavedFileName { get; set; }
        public string SavedBy { get; set; }
    }

    public class HTMLFileEntityList : List<HTMLFileEntity>
    {

    }

    public class GraphMasterEntity
    {
        public string RecordId { get; set; }
        public string GraphName { get; set; }
        public string SavedBy { get; set; }
    }

    public class GraphMasterEntityList : List<GraphMasterEntity>
    {

    }

    public class QueryMasterEntity
    {
        public string QueryName { get; set; }
        public string SavedBy { get; set; }
        public string DepartmentId { get; set; }
        public string ClientId { get; set; }
        public string LobyName { get; set; }
    }

    public class QueryMasterEntityList : List<QueryMasterEntity>
    {

    }

    

   
}
