namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    /// <summary>
    /// Admin user viewmodel for Admin Grid Row item
    /// </summary>
    public class AdminUserGridItemVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string RoleType { get; set; }
        public string Status { get; set; }
    }
}
