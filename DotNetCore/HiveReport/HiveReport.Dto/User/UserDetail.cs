using HiveReport.Dto.Common;
using System.Collections.Generic;

namespace HiveReport.Dto.User
{
    public class UserDetailDto
    {
        public string RightsName { get; set; }
        public bool HasQuery { get; set; }
        public bool HasReport { get; set; }
        public ReportMasterDtoList ReportMasterDtoList { get; set; }
        public TableMasterDtoList TableMasterDtoList { get; set; }
        public ViewMasterDtoList ViewMasterDtoList { get; set; }
        public HTMLFileDtoList HTMLFileDtoList { get; set; }
        public GraphMasterDtoList GraphMasterDtoList { get; set; }
        public QueryMasterDtoList QueryMasterDtoList { get; set; }
        public string PasswordExpirationMessage { get; set; }
        public string UserName { get; set; }
        public SharedLayoutDto SharedLayoutDto { get; set; }
    }

    public class ReportMasterDto
    {
        public string Queryname { get; set; }
        public string SavedBy { get; set; }
        public string DepartmentID { get; set; }
        public string ClientID { get; set; }
        public string UnderLOB { get; set; }
        public string TableName { get; set; }
    }

    public class ReportMasterDtoList : List<ReportMasterDto>
    {

    }

    public class TableMasterDto
    {
        public string TableId { get; set; }
        public string TableName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class TableMasterDtoList : List<TableMasterDto>
    {

    }

    public class ViewMasterDto
    {
        public string ViewID { get; set; }
        public string ViewName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class ViewMasterDtoList : List<ViewMasterDto>
    {

    }

    public class HTMLFileDto
    {
        public string AutoId { get; set; }
        public string SavedFileName { get; set; }
        public string SavedBy { get; set; }
    }

    public class HTMLFileDtoList : List<HTMLFileDto>
    {

    }

    public class GraphMasterDto
    {
        public string RecordId { get; set; }
        public string GraphName { get; set; }
        public string SavedBy { get; set; }
    }

    public class GraphMasterDtoList : List<GraphMasterDto>
    {

    }

    public class QueryMasterDto
    {
        public string QueryName { get; set; }
        public string SavedBy { get; set; }
        public string DepartmentId { get; set; }
        public string ClientId { get; set; }
        public string LobyName { get; set; }
    }

    public class QueryMasterDtoList : List<QueryMasterDto>
    {

    }

    
}
