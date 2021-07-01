using System.Collections.Generic;

namespace HiveReport.Dto.Common
{
    public class SharedLayoutDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAdminCheck { get; set; }
        public string UserType { get; set; }
        public DepartmentDtoList DepartmentDtoList { get; set; }
        public ClientDtoList ClientDtoList { get; set; }
        public LOBDtoList LOBDtoList { get; set; }
        public string ParentNodeName { get; set; }
        public LeftMenuDtoList LeftMenuDtoList { get; set; }
        public bool IsDashboard { get; set; }
        public TopMenuDtoList TopMenuDtoList { get; set; }
    }

    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Message { get; set; }
    }

    public class DepartmentDtoList : List<DepartmentDto>
    {

    }

    public class ClientDto
    {
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string ClientName { get; set; }
        public string Message { get; set; }
    }

    public class ClientDtoList : List<ClientDto>
    {

    }

    public class LOBDto
    {
        public int LOBId { get; set; }
        public int ClientId { get; set; }
        public int DepartmentId { get; set; }
        public string LOBName { get; set; }
        public string Message { get; set; }
    }

    public class LOBDtoList : List<LOBDto>
    {

    }

    public class LeftMenuDto
    {
        public string MenuDescription { get; set; }
        public string UrlLink { get; set; }
        public string ToolTip { get; set; }
    }

    public class LeftMenuDtoList:List<LeftMenuDto>
    {

    }

    public class TopMenuDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ToolTip { get; set; }
    }

    public class TopMenuDtoList : List<TopMenuDto>
    {

    }
}
