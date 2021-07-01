using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PickfordsIntranet.ViewModels.Auth;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    /// <summary>
    /// AdminUserVM data structure to create admin user
    /// </summary>
    public class AdminUserVM
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter first name.")]
        [MaxLength(80,ErrorMessage = "First name should not be greater than 80 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter surname.")]
        [MaxLength(80, ErrorMessage = "Surname should not be greater than 80 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [MaxLength(80,ErrorMessage = "Email address should not be greater than 80 characters.")]
        [EmailAddress(ErrorMessage = "The email address is invalid.")]
        [Remote(action: "ValidateEMailExistOrNot", controller: "SuperAdmin",AdditionalFields = "Id")]
        public string EmailAddress { get; set; }
        public int DepartmentId { get; set; }
        public List<DepartmentVM> AllDepartments { get; set; }
        public int RoleId { get; set; }
        public List<RoleVM> AllRoles { get; set; }
        public int StatusId { get; set; }
        public List<StatusVM> AllStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperUser { get; set; }
        public string UserPwd { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string AllSelectedRoleTypes { get; set; }
        public bool IsDepartmentalUser { get; set; }
        public bool IsLineManagerUser { get; set; }
        public string LineManagerCanSelectOption { get; set; }
        public List<LineManagerCanVM> LineManagerCanOptionList { get; set; }
    }


}
