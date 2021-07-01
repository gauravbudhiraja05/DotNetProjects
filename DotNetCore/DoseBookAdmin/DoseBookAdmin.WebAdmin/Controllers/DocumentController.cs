using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.Document;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class DocumentController : Controller
    {
        /// <summary>
        /// IDocumentService data member
        /// </summary>
        private IDocumentService _documentService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DocumentController> _logger;

        /// <summary>
        /// MedicineController Constructor
        /// </summary>
        /// <param name="DocumentService">IDocumentService object reference</param>
        public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _documentService = documentService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_documentService.GetAllDoctors());
        }

        public ActionResult GetDocumentByDoctorWise(int id)
        {
            DocumentListGridItemVM DocumentListGridItemVM = _documentService.GetDocumentByDoctorWise(id);
            return PartialView("_DocumentList", DocumentListGridItemVM);
        }

        /// <summary>
        /// Get Add Medicine view 
        /// </summary>
        /// <returns>Add Document view</returns>
        public IActionResult AddDocument(int DoctorId)
        {
            try
            {
                ViewBag.DoctorId = DoctorId;
                Document document = _documentService.GetDocumentDataToCreateDocument();
                document.DoctorId = DoctorId;
                return View(document);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Post Add document to save it
        /// </summary>
        /// <param name="Document">AddDocumentVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        public IActionResult AddDocument([Bind] Document Document)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = _documentService.SaveDocument(Document);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }

        /// <summary>
        /// Edit Medicine Dose Record
        /// </summary>
        /// <param name="id">Medicine Dose Id</param>
        /// <returns>View of Edit Medicine Dose</returns>
        public IActionResult EditDocument(int documentId, int SelectedDoctorId)
        {
            try
            {               
                Document Document = _documentService.GetDocumentById(documentId);
                ViewBag.DoctorId = Document.DoctorId;
                return View(Document);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Update Document
        /// </summary>
        /// <param name="document">Document that will be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditDocument(Document document)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    result = _documentService.UpdateDocument(document);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }

        /// <summary>
        /// Delete Document 
        /// </summary>
        /// <param name="targetIds">List of End User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        public IActionResult DeleteDocument(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            try
            {
                result = _documentService.DeleteDocumentByIds(targetIds);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }
    }
}