using HiveReport.Dto.Common;
using HiveReport.Dto.User;
using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using System;

namespace HiveReport.WebAdmin.Dashboard.Mapping
{
    public class DashboardMapping
    {
        public UserDetailDto Entity2Dto(UserDetailEntity userDetailEntity)
        {
            UserDetailDto userDetailDto = new UserDetailDto
            {
                RightsName = userDetailEntity.RightsName,
                HasQuery = userDetailEntity.HasQuery,
                HasReport = userDetailEntity.HasReport,
                PasswordExpirationMessage = userDetailEntity.PasswordExpirationMessage,
                ReportMasterDtoList = ReportMasterEntityList2DtoList(userDetailEntity.ReportMasterEntityList),
                TableMasterDtoList = TableMasterEntityList2DtoList(userDetailEntity.TableMasterEntityList),
                ViewMasterDtoList = ViewMasterEntityList2DtoList(userDetailEntity.ViewMasterEntityList),
                HTMLFileDtoList = HTMLFileMasterEntityList2DtoList(userDetailEntity.HTMLFileEntityList),
                GraphMasterDtoList = GraphMasterEntityList2DtoList(userDetailEntity.GraphMasterEntityList),
                QueryMasterDtoList = QueryMasterEntityList2DtoList(userDetailEntity.QueryMasterEntityList),
            };

            return userDetailDto;
        }


        #region Private Methods

        private static ReportMasterDtoList ReportMasterEntityList2DtoList(ReportMasterEntityList reportMasterEntityList)
        {
            ReportMasterDtoList reportMasterDtoList = new ReportMasterDtoList();

            reportMasterEntityList.ForEach(reportMasterEntity =>
            {
                reportMasterDtoList.Add(new ReportMasterDto
                {
                    ClientID = reportMasterEntity.ClientID,
                    DepartmentID = reportMasterEntity.DepartmentID,
                    Queryname = reportMasterEntity.QueryName,
                    SavedBy = reportMasterEntity.SavedBy,
                    TableName = reportMasterEntity.TableName,
                    UnderLOB = reportMasterEntity.UnderLOB,
                });
            });

            return reportMasterDtoList;
        }

        private static TableMasterDtoList TableMasterEntityList2DtoList(TableMasterEntityList tableMasterEntityList)
        {
            TableMasterDtoList tableMasterDtoList = new TableMasterDtoList();

            tableMasterEntityList.ForEach(tableMasterEntity =>
            {
                tableMasterDtoList.Add(new TableMasterDto
                {
                    TableId = tableMasterEntity.TableId,
                    TableName = tableMasterEntity.TableName,
                    CreatedBy = tableMasterEntity.CreatedBy,
                });
            });

            return tableMasterDtoList;
        }

        private static ViewMasterDtoList ViewMasterEntityList2DtoList(ViewMasterEntityList viewMasterEntityList)
        {
            ViewMasterDtoList viewMasterDtoList = new ViewMasterDtoList();

            viewMasterEntityList.ForEach(viewMasterEntity =>
            {
                viewMasterDtoList.Add(new ViewMasterDto
                {
                    ViewID = viewMasterEntity.ViewID,
                    ViewName = viewMasterEntity.ViewName,
                    CreatedBy = viewMasterEntity.CreatedBy,
                });
            });

            return viewMasterDtoList;
        }

        private static HTMLFileDtoList HTMLFileMasterEntityList2DtoList(HTMLFileEntityList htmlFileEntityList)
        {
            HTMLFileDtoList htmlFileDtoList = new HTMLFileDtoList();

            htmlFileEntityList.ForEach(htmlFileEntity =>
            {
                htmlFileDtoList.Add(new HTMLFileDto
                {
                    AutoId = htmlFileEntity.AutoId,
                    SavedFileName = htmlFileEntity.SavedFileName,
                    SavedBy = htmlFileEntity.SavedBy,
                });
            });

            return htmlFileDtoList;
        }

        private static GraphMasterDtoList GraphMasterEntityList2DtoList(GraphMasterEntityList graphMasterEntityList)
        {
            GraphMasterDtoList graphMasterDtoList = new GraphMasterDtoList();

            graphMasterEntityList.ForEach((Action<GraphMasterEntity>)(graphMasterEntity =>
            {
                graphMasterDtoList.Add(new GraphMasterDto
                {
                    RecordId = graphMasterEntity.RecordId,
                    GraphName = graphMasterEntity.GraphName,
                    SavedBy = graphMasterEntity.SavedBy,
                });
            }));

            return graphMasterDtoList;
        }

        private static QueryMasterDtoList QueryMasterEntityList2DtoList(QueryMasterEntityList queryMasterEntityList)
        {
            QueryMasterDtoList queryMasterDtoList = new QueryMasterDtoList();

            queryMasterEntityList.ForEach(queryMasterEntity =>
            {
                queryMasterDtoList.Add(new QueryMasterDto
                {
                    QueryName = queryMasterEntity.QueryName,
                    SavedBy = queryMasterEntity.SavedBy,
                    DepartmentId = queryMasterEntity.DepartmentId,
                    ClientId = queryMasterEntity.ClientId,
                    LobyName = queryMasterEntity.LobyName,
                });
            });

            return queryMasterDtoList;
        }

        #endregion

    }
}
