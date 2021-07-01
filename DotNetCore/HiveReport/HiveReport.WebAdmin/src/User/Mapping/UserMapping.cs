using HiveReport.Dto.User;
using HiveReport.Entity.User;

namespace HiveReport.WebAdmin.User.Mapping
{
    public class UserMapping
    {
        public AuthUserEntity AuthUserDto2AuthUserEntity(AuthUserDto authUserDto)
        {
            return new AuthUserEntity
            {
                UserName = authUserDto.UserName,
                Password = authUserDto.Password
            };
        }

        public LoggedInUserDto LoggedInUserEntity2LoggedInUserDto(LoggedInUserEntity loggedInUserEntity)
        {
            return new LoggedInUserDto
            {
                RecID = loggedInUserEntity.RecID,
                UserType = loggedInUserEntity.UserType,
                UserID = loggedInUserEntity.UserID,
                Password = loggedInUserEntity.Password,
                Prefix = loggedInUserEntity.Prefix,
                UserName = loggedInUserEntity.UserName,
                Designation = loggedInUserEntity.Designation,
                BU = loggedInUserEntity.BU,
                Email = loggedInUserEntity.Email,
                Status = loggedInUserEntity.Status,
                AddDate = loggedInUserEntity.AddDate,
                EmpID = loggedInUserEntity.EmpID,
                PwdStatus = loggedInUserEntity.PwdStatus,
                LockReason = loggedInUserEntity.LockReason,
                LockDate = loggedInUserEntity.LockDate,
                CreatedBy = loggedInUserEntity.CreatedBy,
                LocalUser = loggedInUserEntity.LocalUser,
                DeptID = loggedInUserEntity.DeptID,
                ClientID = loggedInUserEntity.ClientID,
                LOBID = loggedInUserEntity.LOBID,
                CreatorId = loggedInUserEntity.CreatorId,
                Adminid = loggedInUserEntity.Adminid,
                Company = loggedInUserEntity.Company,
                Mobile = loggedInUserEntity.Mobile,
                UserAdminCheck = loggedInUserEntity.UserAdminCheck,
                RegisterationStatus = loggedInUserEntity.RegisterationStatus,
                IsSuccess = loggedInUserEntity.IsSuccess,
                Message = loggedInUserEntity.Message
            };
        }

        public LoggedInUserEntity LoggedInUserDto2LoggedInUserEntity(LoggedInUserDto loggedInUserDto)
        {
            return new LoggedInUserEntity
            {
                RecID = loggedInUserDto.RecID,
                UserType = loggedInUserDto.UserType,
                UserID = loggedInUserDto.UserID,
                Password = loggedInUserDto.Password,
                Prefix = loggedInUserDto.Prefix,
                UserName = loggedInUserDto.UserName,
                Designation = loggedInUserDto.Designation,
                BU = loggedInUserDto.BU,
                Email = loggedInUserDto.Email,
                Status = loggedInUserDto.Status,
                AddDate = loggedInUserDto.AddDate,
                EmpID = loggedInUserDto.EmpID,
                PwdStatus = loggedInUserDto.PwdStatus,
                LockReason = loggedInUserDto.LockReason,
                LockDate = loggedInUserDto.LockDate,
                CreatedBy = loggedInUserDto.CreatedBy,
                LocalUser = loggedInUserDto.LocalUser,
                DeptID = loggedInUserDto.DeptID,
                ClientID = loggedInUserDto.ClientID,
                LOBID = loggedInUserDto.LOBID,
                CreatorId = loggedInUserDto.CreatorId,
                Adminid = loggedInUserDto.Adminid,
                Company = loggedInUserDto.Company,
                Mobile = loggedInUserDto.Mobile,
                UserAdminCheck = loggedInUserDto.UserAdminCheck,
                RegisterationStatus = loggedInUserDto.RegisterationStatus,
                IsSuccess = loggedInUserDto.IsSuccess,
                Message = loggedInUserDto.Message
            };
        }

        public RegisteredUserEntity RegisteredUserDto2RegisteredUserEntity(RegisteredUserDto registeredUserDto)
        {
            return new RegisteredUserEntity
            {
                // External Registeration
                CompanyName = registeredUserDto.CompanyName,
                MobileNumber = registeredUserDto.MobileNumber,
                ProductType = registeredUserDto.ProductType,
                DatabaseType = registeredUserDto.DatabaseType,
                ProductCode = registeredUserDto.ProductCode,
                Database = registeredUserDto.Database,

                // Internal Registeration
                Prefix = registeredUserDto.Prefix,
                CreatorId = registeredUserDto.CreatorId,
                DepartmentName = registeredUserDto.DepartmentName,
                ClientName = registeredUserDto.ClientName,
                LOBName = registeredUserDto.LOBName,

                //Common
                Name = registeredUserDto.Name,
                EmployeeId = registeredUserDto.EmployeeId,
                Designation = registeredUserDto.Designation,
                BU = registeredUserDto.BU,
                IsNewDesignation = registeredUserDto.IsNewDesignation,
                DepartmentId = registeredUserDto.DepartmentId,
                ClientId = registeredUserDto.ClientId,
                LOBId = registeredUserDto.LOBId,
                EmailAddress = registeredUserDto.EmailAddress,
                Scope = registeredUserDto.Scope,
                UserId = registeredUserDto.UserId,
                Password = registeredUserDto.Password

            };
        }
    }
}
