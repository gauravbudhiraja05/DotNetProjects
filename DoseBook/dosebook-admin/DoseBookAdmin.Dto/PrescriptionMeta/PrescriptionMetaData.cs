using System.Collections.Generic;

namespace DoseBookAdmin.Dto.PrescriptionMeta
{
    public class PrescriptionMetaDataDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public int DisplayOrderNumber { get; set; }

    }

    public class PrescriptionMetaDataDtoList : List<PrescriptionMetaDataDto>
    {

    }

    public class PrescriptionMetaDataListGridItemDto
    {
        public PrescriptionMetaDataDtoList PrescriptionMetaDataDtoList { get; set; }

        public PrescriptionMetaTypeDto PrescriptionMetaTypeDto { get; set; }
    }
}


//public class Document
//{
//    public string Label { get; set; }

//    public ClassificationResult Result { get; set; }

//    public string Description { get; set; }
//}

//public class ClassificationResult
//{
//    public int ClassificationResultId { get; set; }

//    public string  Name { get; set; }

//    public ClassificationType Classification { get; set; }

//    public int ClassificationTypeId {get;set;}
//}

//public class ClassificationType
//{
//    public ClassificationType Type { get; set; }

//    public MedicineDose Dose { get; set; }

//    public DiagnosticTest Test { get; set; }

//    public MiscSuggestion Misc { get; set; }
//}

//public enum ClassificationType
//{
//    TEST,
//    MEDICINE,
//    MISC
//}

//public class MedicineDose
//{
//    public int MedicineId { get; set; }

//    public string MedicineName { get; set; }

//    public string Frequency { get; set; }

//    public string Directions { get; set; }

//    public string Label { get; set; }

//    public string Duration { get; set; }

//    public int Dose { get; set; }                   // -1 for N/A, 1, 2, 3

//    public string DoseUnit { get; set; }            // tablet, spoon, drops, points,

//    public bool IsActive { get; set; }

//    public int CreatedBy { get; set; }

//    public string CreatedByUserType { get; set; }

//    public int ModifiedBy { get; set; }

//    public string ModifiedByUserType { get; set; }

//    public int DeletedBy { get; set; }

//    public string DeletedByUserType { get; set; }
//}

//public class DiagnosticTest
//{
//    public int TestId { get; set; }

//    public string TestName { get; set; }
//}

//public class MiscSuggestion
//{
//    public int TestId { get; set; }

//    public string TestName { get; set; }

//    public string Description { get; set; }
//}

//public class DoseMeta
//{
//    public int id { get; set; }

//    public DoseMetaType Type { get; set; }

//    public int DoseMetaTypeId { get; set; }

//    public string Title { get; set; }

//    public bool IsActive { get; set; }
//}

//public enum DoseMetaType
//{
//    UNIT, // 1 tablet or 1 spoon
//    FREQUENCY, // 3 times a day
//    DIRECTION, // after meal
//    DURATION // 2 months
//}
//}



//dose_meta_data

//type, value
//frequencey, after meal , 1
//frequencey, before meal, 2
//unit, spoon
//unit, tablet
//direction, 1-1-1
//direction, 1-0-1
//duration, 1 month
//duration, 2 month