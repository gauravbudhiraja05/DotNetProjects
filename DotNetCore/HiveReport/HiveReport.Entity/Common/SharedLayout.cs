using System.Collections.Generic;

namespace HiveReport.Entity.Common
{
    public class SharedLayoutEntity
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAdminCheck { get; set; }
        public string UserType { get; set; }
        public DepartmentEntityList DepartmentEntityList { get; set; }
        public ClientEntityList ClientEntityList { get; set; }
        public LOBEntityList LOBEntityList { get; set; }
        public string ParentNodeName { get; set; }
        public LeftMenuEntityList LeftMenuEntityList { get; set; }
        public bool IsDashboard { get; set; }
        public TopMenuEntityList TopMenuEntityList { get; set; }

    }

    public class DepartmentEntity
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Message { get; set; }
    }

    public class DepartmentEntityList : List<DepartmentEntity>
    {

    }

    public class ClientEntity
    {
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string ClientName { get; set; }
        public string Message { get; set; }
    }

    public class ClientEntityList : List<ClientEntity>
    {

    }

    public class LOBEntity
    {
        public int LOBId { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string LOBName { get; set; }
        public string Message { get; set; }
    }

    public class LOBEntityList : List<LOBEntity>
    {

    }

    public class LeftMenuEntity
    {
        public string MenuDescription { get; set; }
        public string UrlLink { get; set; }
        public string ToolTip { get; set; }
    }

    public class LeftMenuEntityList : List<LeftMenuEntity>
    {

    }

    public class TopMenuEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ToolTip { get; set; }
    }

    public class TopMenuEntityList : List<TopMenuEntity>
    {

    }
}
