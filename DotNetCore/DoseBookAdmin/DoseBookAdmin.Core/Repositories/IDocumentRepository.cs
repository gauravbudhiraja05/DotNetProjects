using DoseBookAdmin.ViewModels.Document;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IDocumentRepository
    {
        DocumentListGridItemVM GetDocumentByDoctorWise(string query, object param);

        Document GetDocumentDataToCreateDocument(string query);

        BaseResult DeleteDocumentByIds(string query, DeleteItemVM targetIds);

        Document GetDocumentById(string query, object param);

        BaseResult SaveDocument(string query, object param);

        BaseResult UpdateDocument(string query, object param);
    }
}
