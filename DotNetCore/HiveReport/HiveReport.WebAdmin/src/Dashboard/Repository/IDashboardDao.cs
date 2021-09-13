using HiveReport.Entity.User;

namespace HiveReport.WebAdmin.Dashboard.Repository
{
    public interface IDashboardDao
    {

        /// <summary>
        /// GetDemoUserProductCode will get ths user product code from product demo table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>string object</returns>
        string GetDemoUserProductCode(string userId);

        /// <summary>
        /// GetDemoUserRights will get ths user right code from product master table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>string object</returns>
        string GetDemoUserRights(string productCode);

        /// <summary>
        /// GetReportMasterList will get the top 5 reports from ReportMaster table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Report Master List object</returns>
        ReportMasterEntityList GetReportMasterList(string userId);

        /// <summary>
        /// GetTableMasterList will get the top 5 table from TableMaster table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Table Master List object</returns>
        TableMasterEntityList GetTableMasterList(string userId);

        /// <summary>
        /// GetViewMasterList will get the top 5 view name from ViewMaster table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>View Master List object</returns>
        ViewMasterEntityList GetViewMasterList(string userId);

        /// <summary>
        /// GetHtmlFileMasterList will get the top 5 html file name from HtmlFileMaster table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Html File Master List object</returns>
        HTMLFileEntityList GetHtmlFileMasterList(string userId);

        /// <summary>
        /// GetGraphMasterList will get the top 5 graph name from GraphMaster table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Graph Master List object</returns>
        GraphMasterEntityList GetGraphMasterList(string userId);

        /// <summary>
        /// GetQueryMasterList will get the top 5 query name from QueryMaster table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Query Master List object</returns>
        QueryMasterEntityList GetQueryMasterList(string userId);

       
    }
}
