using DoseBookAdmin.Entity.Global;

namespace DoseBookAdmin.Entity.User
{
    public class LoggedInUserEntity : BaseResultEntity
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
    }
}
