namespace Dream14.ViewModels.SuperAdmin
{
    
    /// <summary>
    /// AdminUserVM data structure to create admin user
    /// </summary>
    public class AdminUser
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Please enter name.")]
        public string FullName { get; set; }

        //[Required(ErrorMessage = "Please enter email address.")]
        //[MaxLength(80, ErrorMessage = "Email address should not be greater than 100 characters.")]
        //[EmailAddress(ErrorMessage = "The email address is invalid.")]
        //[Remote(action: "ValidateEmailExistOrNot", controller: "SuperAdmin", AdditionalFields = "Id")]
        public string EmailAddress { get; set; }

        //[Required(ErrorMessage = "Please enter password.")]
        //[MaxLength(80, ErrorMessage = "Password should not be greater than 80 characters.")]
        public string Password { get; set; }

        public string CreationDate { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public int DeletedBy { get; set; }

        public string CreatedDateDisplay { get; set; }

    }
}
