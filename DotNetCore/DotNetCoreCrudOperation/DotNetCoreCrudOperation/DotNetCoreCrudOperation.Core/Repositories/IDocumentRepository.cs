using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace PickfordsIntranet.Core.Repositories
{
    public interface IDocumentRepository
    {
        DocumentGridItemVM GetAllDocuments(string query, object param);
        BaseResult SaveFolder(FolderVM folder);
        BaseResult SaveDocument(string spName, DocumentGridItemVM docs);
        DocumentGridItemVM GetDocumentById(string query, int param);
        BaseResult DeleteDocOrFolderByIds(string query, object param);
        List<FolderDetails> GetAllFoldersByDeptId(int id);
        bool UpdateFoldersOrder(XElement foldersXml);
        bool UpdateDocumentsOrder(XElement documentsXml);
    }
}
