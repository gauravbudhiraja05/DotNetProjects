using DoseBookAdmin.Dto.Global;

namespace DoseBookAdmin.Dto.User
{
    /// <summary>
    /// Logged-In User Information Data Structure
    /// </summary>
    public class LoggedInUserDto : BaseResultDto
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
    }
}
