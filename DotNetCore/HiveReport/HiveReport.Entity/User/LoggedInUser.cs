using HiveReport.Entity.Common;
using System;

namespace HiveReport.Entity.User
{
    public class LoggedInUserEntity : BaseResultEntity
    {
        public int RecID { get; set; }
        public int UserType { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Prefix { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string BU { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime AddDate { get; set; }
        public string EmpID { get; set; }
        public string PwdStatus { get; set; }
        public string LockReason { get; set; }
        public DateTime LockDate { get; set; }
        public string CreatedBy { get; set; }
        public string LocalUser { get; set; }
        public int DeptID { get; set; }
        public int ClientID { get; set; }
        public string LOBID { get; set; }
        public string CreatorId { get; set; }
        public string Adminid { get; set; }
        public string Company { get; set; }
        public string Mobile { get; set; }
        public string UserAdminCheck { get; set; }
        public string RegisterationStatus { get; set; }
    }
}
