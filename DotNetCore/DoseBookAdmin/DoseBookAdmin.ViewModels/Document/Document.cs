using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using System.Collections.Generic;

namespace DoseBookAdmin.ViewModels.Document
{
    public class DocumentListGridItemVM
    {
        public List<Doctor> AllDoctors { get; set; }

        public List<Document> AllDocuments { get; set; }

        public Doctor SelectedDoctor { get; set; }
    }

    public class Document
    {
        public int DocumentId { get; set; }

        public string Label { get; set; }

        public string ClassificationResultName { get; set; }

        public int ClassificationResultId { get; set; }

        public string Description { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string CreatedDate { get; set; }

        public List<Doctor> AllDoctors { get; set; }

        public List<ClassificationResult> AllClassificationResult { get; set; }
    }
}
