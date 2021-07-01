using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Auth
{
    public class ResetPasswordForEmailTemplateVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ResetPasswordLink { get; set; }
        public Guid Token { get; set; }
    }
}
