using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Global
{
    public class FolderDetails
    {
        public int FolderId { get; set; }

        public int FolderParentId { get; set; }

        public string FolderName { get; set; }

        public DateTime CreationDate { get; set; }

        public string FolderFirstName { get; set; }

        public string FolderSurName { get; set; }

        public int DepartMentId { get; set; }
    }
}
