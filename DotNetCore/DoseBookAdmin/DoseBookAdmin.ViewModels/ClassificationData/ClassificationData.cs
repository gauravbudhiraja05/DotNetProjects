using DoseBookAdmin.ViewModels.Doctors;
using System.Collections.Generic;

namespace DoseBookAdmin.ViewModels.ClassificationData
{
   
    public class ClassificationTypeListGridItemVM
    {
        public List<ClassificationType> AllClassificationTypes { get; set; }

        public List<DiagnosticTest> AllDiagnosticTests { get; set; }

        public List<MedicineDose> AllMedicineDoses { get; set; }

        public List<MiscSuggestion> AllMiscSuggestions { get; set; }

        public ClassificationType SelectedClassificationType { get; set; }

    }

    public class ClassificationResultListGridItemVM
    {
        public List<Doctor> AllDoctors { get; set; }

        public List<ClassificationResult> AllClassificationResults { get; set; }

        public Doctor SelectedDoctor { get; set; }

        public string ClassificationTypeName { get; set; }
    }

    public class ClassificationResult
    {
        public int ClassificationResultId { get; set; }

        public string ClassificationResultName { get; set; }

        public int ClassificationTypeId { get; set; }

        public string ClassificationTypeName { get; set; }

        public string TypeName { get; set; }

        public int ClassificationId { get; set; }

        public string ClassificationName { get; set; }

        public Doctor SelectedDoctor { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string CreatedDate { get; set; }

        public List<Doctor> AllDoctors { get; set; }

        public List<ClassificationType> ClassificationTypeList { get; set; }

        public List<DiagnosticTest> DiagnosticTestList { get; set; }

        public List<MiscSuggestion> MiscSuggestionList { get; set; }

        public List<MedicineDose> MedicineDoseList { get; set; }
    }

    public class ClassificationType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class MedicineDoseListGridItemVM
    {
        public List<Doctor> AllDoctors { get; set; }

        public List<MedicineDose> AllMedicineDoses { get; set; }

        public Doctor SelectedDoctor { get; set; }

        public List<string> FrequencyList { get; set; }

        public List<string> DirectionList { get; set; }

        public List<string> DurationList { get; set; }

        public List<string> DoseUnitList { get; set; }
    }

    public class DiagnosticTestListGridItemVM
    {
        public List<Doctor> AllDoctors { get; set; }

        public List<DiagnosticTest> AllDiagnosticTests { get; set; }

        public Doctor SelectedDoctor { get; set; }
    }

    public class MiscSuggestionListGridItemVM
    {
        public List<Doctor> AllDoctors { get; set; }

        public List<MiscSuggestion> AllMiscSuggestions { get; set; }

        public Doctor SelectedDoctor { get; set; }
    }

    public class DiagnosticTest
    {
        public int TestId { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string TestName { get; set; }

        public string TestCreatedDate { get; set; }

        public List<Doctor> AllDoctors { get; set; }
    }

    public class MedicineDose
    {
        public int MedicineId { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string MedicineName { get; set; }

        public string Frequency { get; set; }

        public string Directions { get; set; }

        public string Label { get; set; }

        public string Duration { get; set; }

        public int Dose { get; set; }                   // -1 for N/A, 1, 2, 3

        public string DoseUnit { get; set; }            // tablet, spoon, drops, points,

        public bool IsActive { get; set; }

        public string DoseCreatedDate { get; set; }

        public List<Doctor> AllDoctors { get; set; }

        public List<string> FrequencyList { get; set; }

        public List<string> DirectionList { get; set; }

        public List<string> DurationList { get; set; }

        public List<string> DoseUnitList { get; set; }

    }

    public class MiscSuggestion
    {
        public int TestId { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string TestName { get; set; }

        public string Description { get; set; }

        public string MiscCreatedDate { get; set; }

        public List<Doctor> AllDoctors { get; set; }
    }
}
