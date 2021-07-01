using System;
using System.Collections.Generic;

namespace PickfordsIntranet.ViewModels.Global
{
    /// <summary>
    /// User Action Loggin Details data structure
    /// </summary>
    public class UserActionLoggingDetails
    {
        public string ServerId { get; set; }
        public string LocalIp { get; set; }
        public string AppId { get; set; }
        public string SubComponent { get; set; }
        public int ActionId { get; set; }
        public string Description { get; set; }
        public DateTime ServerDateTime { get; set; }
        public string SourceIP { get; set; }
        public string RemoteMachineInfo { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public EventLevel Level { get; set; }
        public string Url { get; set; }
        public string RoleName { get; set; }
        public List<string> FunctionCodeList { get; set; }
        public string FunctionCode { get; set; }
    }
 
}
