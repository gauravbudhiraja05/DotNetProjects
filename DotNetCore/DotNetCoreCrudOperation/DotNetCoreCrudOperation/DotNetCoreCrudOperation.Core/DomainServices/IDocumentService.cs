using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PickfordsIntranet.Core.DomainServices
{
    public interface IDocumentService
    {
        DocumentGridItemVM GetAllDocuments(int id);

        BaseResult SaveFolder(FolderVM folder);

        BaseResult SaveDocument(DocumentGridItemVM docs);

        DocumentGridItemVM GetDocumentById(int id);

        BaseResult DeleteDocOrFolderByIds(DocumentDeleteItemVM documentDeleteItemVM);

        DocumentGridItemVM GetAllDocuments_Search(Int32 DepartmentId, string SeachValue);

        bool IsFolderDeletable(int folderId);

        bool IsParentFolderExist(int DepartmentId, string FolderName, int ParentFolderId, int EditFolderId);

        bool IsDocumentTitleExist(int DepartmentId, string FolderName, int SubfolderId, int DocumentId);

        List<FolderDetails> GetAllFolders(int departmentId);

        bool UpdateFoldersOrder(XElement foldersXml);

        bool UpdateDocumentsOrder(XElement documentsXml);
    }
}
