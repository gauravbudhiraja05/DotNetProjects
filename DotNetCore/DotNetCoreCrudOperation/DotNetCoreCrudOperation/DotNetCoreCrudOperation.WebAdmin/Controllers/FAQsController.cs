using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.FAQS;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;
using PickfordsIntranet.ViewModels.FAQs;
using Microsoft.AspNetCore.Http;
using System.Linq;

/// <summary>
/// FAQs Controller for FAQs management
/// </summary>
namespace PickfordsIntranet.WebAdmin.Controllers
{
    [Authorize(Roles = "SA,DA")]
    public class FAQsController : Controller
    {

        /// <summary>
        /// IFaqService data member
        /// </summary>
        private IFaqService _faqService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<FAQsController> _logger;


        /// <summary>
        /// FAQsController constructor
        /// </summary>
        /// <param name="faqService"></param>
        public FAQsController(IFaqService faqService, ILogger<FAQsController> logger)
        {
            _faqService = faqService;
            _logger = logger;
        }

        /// <summary>
        /// Get Faq grid view action
        /// </summary>
        /// <returns>Faq grid view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult Index(UserActionLoggingDetails loggingDetails)
        {
            return View(_faqService.GetAllFaqs(loggingDetails.RoleName, Convert.ToInt32(loggingDetails.UserId)));
            //return View();
        }

        /// <summary>
        /// Get Add Faq view action
        /// </summary>
        /// <returns>Add Faq View</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult AddFaqs(UserActionLoggingDetails loggingDetails)
        {
            try
            {
                return View(_faqService.GetAllDocuments(loggingDetails.FullName));
            }
            catch(Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Post Add Faq to save it
        /// </summary>
        /// <param name="faq">AddFaqVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddFaqs([Bind] AddFaqVM faq, UserActionLoggingDetails loggingDetails, string TxtArea)
        {

            BaseResult result = new BaseResult();

            try
            {
                if (ModelState.IsValid)
                {
                    faq.Area = TxtArea;
                    faq.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _faqService.SaveFaqs(faq);
                }
                else
                {

                    result.Message = ModelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .FirstOrDefault()[0].ErrorMessage;
                }


                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = ex.Message;
                return Ok(result);
            }
        }

        /// <summary>
        /// Edit Faq Record
        /// </summary>
        /// <param name="id">faq Id</param>
        /// <returns>View of Edit Faq</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult EditFaqs(int id, UserActionLoggingDetails loggingDetails)
        {
            try
            {
                AddFaqVM faq = _faqService.GetFaqById(id, loggingDetails.RoleName, Convert.ToInt32(loggingDetails.UserId));
                faq.QuestionId = id;
                return View(faq);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }

        }

        /// <summary>
        /// Post Edit Faq to save it
        /// </summary>
        /// <param name="faq">AddFaqVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult EditFaqs([Bind] AddFaqVM faq, UserActionLoggingDetails loggingDetails, string TxtArea)
        {

            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    faq.Area = TxtArea;
                    faq.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _faqService.EditFaqs(faq);
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
        /// Delete Faqs 
        /// </summary>
        /// <param name="allUserIds">List of Faq IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteFaqs(DeleteFaqVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _faqService.DeleteFaqsByIds(targetIds);
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