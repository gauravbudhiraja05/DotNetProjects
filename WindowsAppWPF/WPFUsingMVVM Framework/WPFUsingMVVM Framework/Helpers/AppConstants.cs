using System.Configuration;

namespace WPFUsingMVVM_Framework.Helpers
{
    public class AppConstants
    {
        public static string getConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MemberCDACConnectionString"].ConnectionString;

            return connectionString;
        }
    }
}
