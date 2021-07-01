using PickfordsIntranet.ViewModels.FrontEnd;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.EndUser
{
    public class Registration
    {
        public FrontEndUser User { get; set; }
        public List<EndUserDepartment> EndUserDepartmentList { get; set; }
    }
}
