using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.FAQS;
using PickfordsIntranet.ViewModels.FAQs;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// FaqRepository implements the IFaqRepository 
    /// </summary>
    public class FaqRepository : Repository, IFaqRepository
    {

        /// <summary>
        /// FaqRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public FaqRepository(IDbConnection connection) : base(connection)
        {

        }
        public IEnumerable<FaqsGridItemVM> GetAllFaqs(string query, string roleName, int loggedInUserId)
        {
            var user = new FaqsGridItemVM();
            try
            {
                var result = Connection.Query<FaqsGridItemVM>(query, commandType: CommandType.StoredProcedure).AsList();
                if (result.Count > 0 && roleName == "DA")
                    foreach (var faq in result)
                        if (faq.CreatedBy == loggedInUserId)
                            faq.ISEditable_Deletable = true;
                        else
                            faq.ISEditable_Deletable = false;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public AddFaqVM GetAllDocuments(string query)
        {
            try
            {
                AddFaqVM addFaq = new AddFaqVM();
                using (var multi = Connection.QueryMultiple(query))
                {
                    addFaq.AttachDocuments = multi.Read<DocumentDetail>().AsList();

                }
                addFaq.CreationDate = DateTime.Now.ToString("MM/dd/yyyy");
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
                var result = this.ExecuteQuery<BaseResult>("usp_AddFaqs", SqlCommandType.StoredProcedure,
                    new
                    {
                        QuestionText = faq.QuestionText,
                        AnswerText = faq.Area,
                        AuthorName = faq.AuthorName,
                        PublishDate = faq.PublishDateString,
                        IsActive = 1,
                        CreatedBy = faq.CreatedBy,
                        DocumentIds = faq.DocumentIds
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AddFaqVM GetFaqById(string query, object param, string roleName, int loggedInUserId)
        {
            try
            {
                AddFaqVM faq = new AddFaqVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    faq = multi.Read<AddFaqVM>().SingleOrDefault();
                    faq.ListOfDocumentId = multi.Read<DocumentId>().AsList();
                    faq.AttachDocuments = multi.Read<DocumentDetail>().AsList();

                }

                if (faq != null && roleName == "DA")
                {
                    if (faq.CreatedBy != loggedInUserId)
                        faq.IsReadableOnly = true;
                    else
                        faq.IsReadableOnly = false;
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
                var result = this.ExecuteQuery<BaseResult>("usp_EditFaqs", SqlCommandType.StoredProcedure,
                    new
                    {
                        FaqId = faq.QuestionId,
                        QuestionText = faq.QuestionText,
                        AnswerText = faq.Area,
                        AuthorName = faq.AuthorName,
                        PublishDate = faq.PublishDateString,
                        IsActive = 1,
                        ModifiedBy = faq.ModifiedBy,
                        DocumentIds = faq.DocumentIds
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteFaqsByIds(string query, DeleteFaqVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { FaqIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}