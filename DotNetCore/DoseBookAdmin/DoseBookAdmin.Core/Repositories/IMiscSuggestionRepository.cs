using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IMiscSuggestionRepository
    {
        MiscSuggestionListGridItemVM GetMiscSuggestionByDoctorWise(string query, object param);

        MiscSuggestion GetMiscSuggestionDataToCreateMiscSuggestion(string query);

        BaseResult DeleteMiscSuggestionByIds(string query, DeleteItemVM targetIds);

        MiscSuggestion GetMiscSuggestionById(string query, object param);

        BaseResult SaveMiscSuggestion(string query, object param);

        BaseResult UpdateMiscSuggestion(string query, object param);
    }
}
