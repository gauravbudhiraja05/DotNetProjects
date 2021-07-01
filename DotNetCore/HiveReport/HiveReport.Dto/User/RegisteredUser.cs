namespace HiveReport.Dto.User
{
    public class RegisteredUserDto
    {
        // External Registeration
        public string CompanyName { get; set; }
        public int MobileNumber { get; set; }
        public string ProductType { get; set; }
        public string DatabaseType { get; set; }
        public string ProductCode { get; set; }
        public string Database { get; set; }


        // Internal Registeration
        public string Prefix { get; set; }
        public int CreatorId { get; set; }
        public string DepartmentName { get; set; }
        public string ClientName { get; set; }
        public string LOBName { get; set; }


        // Common
        public int UserType { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Designation { get; set; }
        public string BU { get; set; }
        public bool IsNewDesignation { get; set; }
        public int DepartmentId { get; set; }
        public int ClientId { get; set; }
        public int LOBId { get; set; }
        public string EmailAddress { get; set; }
        public string Scope { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }

    }
}
