namespace DoseBookAdmin.ViewModels.Global
{
    /// <summary>
    /// Logged-In User Information Data Structure
    /// </summary>
    public class LoggedInUser : BaseResult
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
    }
}
