namespace DoseBookAdmin.WebAdmin.Utility
{
    public static class StaticResource
    {
        public static string InternalServerMessage
        {
            get { return "Internal server error, contact to system administrator!!!"; }

        }

        public static string SmtpServerErrorMessage
        {
            get { return "Unable to send mail due to SMTP Server failure!!!"; }

        }
    }
}
