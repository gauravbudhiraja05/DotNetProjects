using System.Collections.Generic;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.FAQS;
using PickfordsIntranet.ViewModels.FAQs;

namespace PickfordsIntranet.Core.DomainServices
{
    public interface IFaqService
    {
        IEnumerable<FaqsGridItemVM> GetAllFaqs(string roleName, int loggedInUserId);
        AddFaqVM GetAllDocuments(string DefaultAuthorName);
        BaseResult SaveFaqs(AddFaqVM faq);
        AddFaqVM GetFaqById(int id, string roleName, int loggedInUserId);
        BaseResult EditFaqs(AddFaqVM faq);
        BaseResult DeleteFaqsByIds(DeleteFaqVM targetIds);
    }

}