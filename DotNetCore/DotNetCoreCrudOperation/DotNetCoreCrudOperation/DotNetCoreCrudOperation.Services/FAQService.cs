using System;
using System.Collections.Generic;
using System.Linq;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.FAQS;
using PickfordsIntranet.ViewModels.FAQs;

namespace PickfordsIntranet.Services
{
    /// <summary>
    /// FaqService implemented IFaqService
    /// </summary>
    public class FaqService : IFaqService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AdminService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public FaqService(IUnitOfWork unitOfWork)
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
        /// Get All Faq from unitOfWork context
        /// </summary>
        /// <returns>List of AdminUserGridItemVM</returns>
        public IEnumerable<FaqsGridItemVM> GetAllFaqs(string roleName, int loggedInUserId)
        {
            try
            {
                var result = _unitOfWork.FaqRepo.GetAllFaqs("usp_GetAllFaqs", roleName, loggedInUserId);
                return result;
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
        public AddFaqVM GetAllDocuments(string DefaultAuthorName)
        {
            try
            {
                AddFaqVM addFaq = _unitOfWork.FaqRepo.GetAllDocuments("usp_GetAllDocuments");
                addFaq.AuthorName = DefaultAuthorName;
                return addFaq;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveFaqs(AddFaqVM faq)
        {
            try
            {

                return _unitOfWork.FaqRepo.SaveFaqs(faq);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AddFaqVM GetFaqById(int id, string roleName, int loggedInUserId)
        {
            try
            {

                var faq = _unitOfWork.FaqRepo.GetFaqById("usp_GetFaqById", new { FaqId = id }, roleName, loggedInUserId);
                foreach (var doc in faq.ListOfDocumentId)
                {
                    if (string.IsNullOrEmpty(faq.DocumentIds))
                    {
                        faq.DocumentIds = doc.IntegerDocumentId.ToString();
                    }
                    else
                    {
                        faq.DocumentIds = faq.DocumentIds + ","+ doc.IntegerDocumentId.ToString();
                    }


                }

                return faq;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult EditFaqs(AddFaqVM faq)
        {
            try
            {

                return _unitOfWork.FaqRepo.EditFaqs(faq);
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        public BaseResult DeleteFaqsByIds(DeleteFaqVM targetIds)
        {
            try
            {
                BaseResult result = _unitOfWork.FaqRepo.DeleteFaqsByIds("usp_DeleteFaqsByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}