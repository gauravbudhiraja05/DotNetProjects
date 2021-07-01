using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.Global;

namespace PickfordsIntranet.Services
{
    /// <summary>
    /// DocumentService implemented IDocumentService
    /// </summary>
    public class DocumentService : IDocumentService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AdminService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public DocumentService(IUnitOfWork unitOfWork)
        {
            try
            {
                _unitOfWork = unitOfWork;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get All documents 
        /// </summary>
        /// <returns>List of DocumentDetail</returns>
        public DocumentGridItemVM GetAllDocuments(int id)
        {
            try
            {
                DocumentGridItemVM docs = _unitOfWork.DocuRepo.GetAllDocuments("usp_GetAllDocumentAndFolder", new { DepartmentId = id });

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save folders
        /// </summary>
        /// <returns>success or failure</returns>
        public BaseResult SaveFolder(FolderVM folder)
        {
            try
            {

                return _unitOfWork.DocuRepo.SaveFolder(folder);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BaseResult SaveDocument(DocumentGridItemVM docs)
        {
            try
            {
                return _unitOfWork.DocuRepo.SaveDocument("usp_AddDocument", docs);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DocumentGridItemVM GetDocumentById(int id)
        {


            return _unitOfWork.DocuRepo.GetDocumentById("usp_GetDocumentById", id);

        }

        public BaseResult DeleteDocOrFolderByIds(DocumentDeleteItemVM documentDeleteItemVM)
        {
            int id = documentDeleteItemVM.ItemIds[0];
            string type = documentDeleteItemVM.ItemTypes[0];
            return _unitOfWork.DocuRepo.DeleteDocOrFolderByIds("usp_DeleteDocOrFolderByIds", new { id = id, type = type });
        }

        public DocumentGridItemVM GetAllDocuments_Search(Int32 DepartmentId, string SeachValue)
        {
            try
            {
                DocumentGridItemVM docs = _unitOfWork.DocuRepo.GetAllDocuments("usp_GetAllDocumentAndFolder_Search", new { DepartmentId = DepartmentId , SeachValue= SeachValue });

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public bool IsFolderDeletable(int folderId)
        {
            try
            {
                bool docs = _unitOfWork.Repo.ExecuteQuery<bool>("usp_IsFolderDeletable", SqlCommandType.StoredProcedure, new { FolderId = folderId }).FirstOrDefault();
                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsParentFolderExist(int DepartmentId,string FolderName, int ParentFolderId, int EditFolderId)
        {
            try
            {
                bool docs = _unitOfWork.Repo.ExecuteQuery<bool>("usp_IsParentFolderExist", SqlCommandType.StoredProcedure, new { DepartmentId = DepartmentId, FolderName = FolderName, ParentFolderId = ParentFolderId, EditFolderId = EditFolderId }).FirstOrDefault();
                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDocumentTitleExist(int DepartmentId, string FolderName, int SubfolderId, int DocumentId)
        {
            try
            {
                bool docs = _unitOfWork.Repo.ExecuteQuery<bool>("usp_IsDocumentTitleExist", SqlCommandType.StoredProcedure, new { DepartmentId = DepartmentId, FolderName = FolderName, SubfolderId = SubfolderId, DocumentId = DocumentId }).FirstOrDefault();
                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FolderDetails> GetAllFolders(int departmentId)
        {
            try
            {
                var docs = _unitOfWork.DocuRepo.GetAllFoldersByDeptId(departmentId);
              
                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateFoldersOrder(XElement foldersXml)
        {
            try
            {
                var result = _unitOfWork.DocuRepo.UpdateFoldersOrder(foldersXml);

                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateDocumentsOrder(XElement documentsXml)
        {
            try
            {
                var result = _unitOfWork.DocuRepo.UpdateDocumentsOrder(documentsXml);

                return true;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
