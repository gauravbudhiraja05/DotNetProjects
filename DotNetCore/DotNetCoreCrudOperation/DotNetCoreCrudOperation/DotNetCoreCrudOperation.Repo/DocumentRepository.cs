using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Documents;
using System.Xml.Linq;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// DocumentRepository implements the IDocumentRepository 
    /// </summary>
    public class DocumentRepository : Repository, IDocumentRepository
    {
        /// <summary>
        /// DocumentRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public DocumentRepository(IDbConnection connection) : base(connection)
        {

        }

        public DocumentGridItemVM GetAllDocuments(string query, object param)
        {
            try
            {
                DocumentGridItemVM docs = new DocumentGridItemVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    docs.Documents = multi.Read<DocumentDetail>().AsList();
                    docs.Folders = multi.Read<FolderDetails>().AsList();
                }

                return docs;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get All folder list to display on dropdown
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FolderDetails> GetAllFoldersByDeptId(int id)
        {
            try
            {
                return this.ExecuteQuery<FolderDetails>("usp_GetAllFolderListByDeptId", SqlCommandType.StoredProcedure, new { @DeptId = id }).AsList();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public BaseResult SaveFolder(FolderVM folder)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddFolder", SqlCommandType.StoredProcedure,
                    new
                    {
                        FolderName = folder.FolderName,
                        CreatedBy = folder.Createdby,
                        FolderParentId = folder.ParentFolderId,
                        DepartmentId = folder.DepartmentId,
                        FolderId = folder.FolderId,

                    }).FirstOrDefault();
                return result;
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
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateFoldersOrder", SqlCommandType.StoredProcedure,
                    new
                    {
                        xmlData = foldersXml

                    }).FirstOrDefault();

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
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateDocumentsOrder", SqlCommandType.StoredProcedure,
                    new
                    {
                        xmlData = documentsXml

                    }).FirstOrDefault();

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult SaveDocument(string spName, DocumentGridItemVM docs)
        {
            try
            {
                return this.ExecuteQuery<BaseResult>(spName, SqlCommandType.StoredProcedure, new
                {
                    FileName = docs.FileName,
                    DocumentTitle = docs.DocumentTitle,
                    DocumentDescription = docs.DocumentDescription,
                    DepartmentId = docs.DepartmentId,
                    FolderId = docs.FolderId,
                    IsFeatured = docs.IsFeatureDocument,
                    FileSize = docs.FileSize,
                    CreatedBy = docs.CreatedBy,
                    DocumentId = docs.DocumentId,
                    FileTypeExtension = docs.FileTypeExtension,
                    PublishDate = docs.PublishDateDisplay,
                    AuthorName = docs.DefaultDocumentAddAuthorName,
                    IsPublished = docs.IsPublishDocument,
                    Type = docs.Type,
                    LinkDestination = docs.LinkDestination
                }).SingleOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DocumentGridItemVM GetDocumentById(string query, int param)
        {
            try
            {
                DocumentGridItemVM featuredMessage = this.ExecuteQuery<DocumentGridItemVM>(query, SqlCommandType.StoredProcedure, new { DocumentId = param }).SingleOrDefault();
                return featuredMessage;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteDocOrFolderByIds(string query, object param)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>(query, SqlCommandType.StoredProcedure, param).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
