using System.Collections.Generic;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.FAQS;
using PickfordsIntranet.ViewModels.FAQs;

namespace PickfordsIntranet.Core.Repositories
{
    /// <summary>
    /// IAdminRepository describes the implementation required for only SuperAdmin data/object access
    /// </summary>
    public interface IFaqRepository
    {
        IEnumerable<FaqsGridItemVM> GetAllFaqs(string query, string roleName, int loggedInUserId);
        AddFaqVM GetAllDocuments(string query);
        BaseResult SaveFaqs(AddFaqVM faq);
        AddFaqVM GetFaqById(string query, object param, string roleName, int loggedInUserId);
        BaseResult EditFaqs(AddFaqVM faq);
        BaseResult DeleteFaqsByIds(string query, DeleteFaqVM deleteItems);
    }
}