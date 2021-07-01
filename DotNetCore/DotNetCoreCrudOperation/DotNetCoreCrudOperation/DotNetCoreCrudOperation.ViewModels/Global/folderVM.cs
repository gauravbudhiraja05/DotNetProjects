using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Global
{
    public class FolderVM
    {
        public string FolderName { get; set; }
        public int FolderId { get; set; }
        public int ParentFolderId { get; set; }
        public int Createdby { get; set; }
        public int DepartmentId { get; set; }
    }
}
