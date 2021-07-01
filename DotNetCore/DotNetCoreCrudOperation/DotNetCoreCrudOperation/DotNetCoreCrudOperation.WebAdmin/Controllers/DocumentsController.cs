using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;
using System;
using System.IO;
using Microsoft.AspNetCore;
using System.Diagnostics.Tracing;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    /// <summary>
    /// Document Controller for document management 
    /// </summary>
    [Authorize(Roles = "SA,DA")]
    public class DocumentsController : Controller
    {
        /// <summary>
        /// IFaqService data member
        /// </summary>
        private IDocumentService _docService;

        private INewsService _newsService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DocumentsController> _logger;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;



        /// <summary>
        /// FAQsController constructor
        /// </summary>
        /// <param name="faqService"></param>
        public DocumentsController(IDocumentService docService, INewsService newsService, ILogger<DocumentsController> logger, IPathProvider pathProvider)
        {
            _docService = docService;
            _newsService = newsService;
            _logger = logger;
            _pathProvider = pathProvider;
        }

        [AutoPopulateLoggingDetails]
        public IActionResult Index(UserActionLoggingDetails loggingDetails)
        {
            try
            {
                // return View(_newsService.GetAllDepartments());
                if (loggingDetails.RoleName == "SA")
                {

                    return View(_newsService.GetAllDepartments());
                }
                else
                {

                    return View(_newsService.GetUserDepartmentDetailbyUserId(Convert.ToInt32(loggingDetails.UserId)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public ActionResult GetDocumentsByDepartmentWise(int id, UserActionLoggingDetails loggingDetails)
        {
            DocumentGridItemVM docs = _docService.GetAllDocuments(id);
            docs.CreationDate = DateTime.Now.ToString("MM/DD/YYYY");
            docs.DepartmentId = id;
            docs.DefaultDocumentAddAuthorName = loggingDetails.FullName;

            return PartialView("_DocumentList", docs);
        }

        [AllowAnonymous]
        [AutoPopulateLoggingDetails]
        public ActionResult GetDocumentsByDepartmentWise_Search(Int32 DepartmentId, string SeachValue, UserActionLoggingDetails loggingDetails)
        {
            if (SeachValue == null)
                SeachValue = "";
            DocumentGridItemVM docs = _docService.GetAllDocuments_Search(DepartmentId, SeachValue);
            docs.DepartmentId = DepartmentId;
            docs.CreationDate = DateTime.Now.ToString("MM/DD/YYYY");
            docs.DefaultDocumentAddAuthorName = loggingDetails.FullName;
            return PartialView("_DocumentList", docs);
        }

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public JsonResult AddFolders(FolderVM folder, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {


                folder.Createdby = Convert.ToInt32(loggingDetails.UserId);
                result = _docService.SaveFolder(folder);
                return Json(result);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Json(result);
            }

        }

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult Upload(DocumentGridItemVM uploadedFile, int DepartmentId, int SubfolderId, int DocumentId, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            result.IsSuccess = true;
            result.Message = "Test";

            try
            {
                if (uploadedFile.Type.Equals("Document"))
                {
                    if (uploadedFile.UploadFile != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            uploadedFile.UploadFile.CopyTo(memoryStream);
                            string fileName = Guid.NewGuid() + Path.GetFileName(uploadedFile.UploadFile.FileName).Trim().Replace(" ", String.Empty);
                            string filePath = _pathProvider.MapPath("") + @"\Uploads\Documents\" + fileName;
                            System.IO.File.WriteAllBytes(filePath, memoryStream.ToArray());
                            uploadedFile.FileName = fileName;
                            uploadedFile.FileSize = uploadedFile.UploadFile.Length.ToString();
                            uploadedFile.FolderId = SubfolderId;
                            uploadedFile.DepartmentId = DepartmentId;
                            uploadedFile.DocumentId = DocumentId;
                            uploadedFile.FileTypeExtension = Path.GetExtension(filePath);
                        }
                    }
                    else
                    {
                        uploadedFile.FolderId = SubfolderId;
                        uploadedFile.DocumentId = DocumentId;


                    }
                }

                else
                {
                    uploadedFile.FolderId = SubfolderId;
                    uploadedFile.DepartmentId = DepartmentId;
                    uploadedFile.DocumentId = DocumentId;
                    uploadedFile.FileTypeExtension =".link";
                    uploadedFile.FolderId = SubfolderId;
                    uploadedFile.DocumentId = DocumentId;
                    uploadedFile.FileSize = "0";
                }
                uploadedFile.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                result = _docService.SaveDocument(uploadedFile);
                //}
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
            return Ok(result);

        }

        [AllowAnonymous]
        public JsonResult populateDocumentValue(int id)
        {

            DocumentGridItemVM docs = _docService.GetDocumentById(id);
            return Json(docs);

        }

        //DeleteNews
        /// <summary>
        /// Delete News  
        /// </summary>
        /// <param name="allUserIds">List of Admin User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteDocumentorFolder(DocumentDeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _docService.DeleteDocOrFolderByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Check Folder is deletable or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        [AllowAnonymous]
        public JsonResult IsFolderDeletable(int id)
        {
            bool result = _docService.IsFolderDeletable(folderId: id);

            return Json(result);
        }

        /// <summary>
        /// Check Folder exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        [AllowAnonymous]
        public JsonResult IsParentFolderExist(int id, string name, int paramParentFolderId, int paramEditfolderId)
        {
            bool result = _docService.IsParentFolderExist(DepartmentId: id, FolderName: name, ParentFolderId: paramParentFolderId, EditFolderId: paramEditfolderId);

            return Json(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult IsDocumentTitleExist(DocumentGridItemVM uploadedFile, int DepartmentId, int SubfolderId, int DocumentId)
        {
            bool result = _docService.IsDocumentTitleExist(DepartmentId: DepartmentId, FolderName: uploadedFile.DocumentTitle, SubfolderId: SubfolderId, DocumentId: DocumentId);

            return Json(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult AutoPopulateAllFolders(int id)
        {

            var result = _docService.GetAllFolders(id);


            return Json(result);
        }

        [AllowAnonymous]
        public JsonResult ChangeFolderOrder(List<DocumentOrder> FolderOrderList)
        {

            if (FolderOrderList != null && FolderOrderList.Count > 0)
            {

                XElement foldersXml = new XElement("Folders",
                   FolderOrderList.Select(i => new XElement("Folder",
                  new XAttribute("Id", i.id),
                  new XAttribute("Position", i.position),
                   i.position
                   )));

                var result = _docService.UpdateFoldersOrder(foldersXml);

            }

            return Json(true);
        }

        [AllowAnonymous]
        public JsonResult ChangeDocumentOrder(List<DocumentOrder> DocumentOrderList)
        {

            if (DocumentOrderList != null && DocumentOrderList.Count > 0)
            {

                XElement documentsXml = new XElement("Documents",
                   DocumentOrderList.Select(i => new XElement("Document",
                  new XAttribute("Id", i.id),
                  new XAttribute("Position", i.position),
                   i.position
                   )));

                var result = _docService.UpdateDocumentsOrder(documentsXml);

            }

            return Json(true);
        }

    }
}