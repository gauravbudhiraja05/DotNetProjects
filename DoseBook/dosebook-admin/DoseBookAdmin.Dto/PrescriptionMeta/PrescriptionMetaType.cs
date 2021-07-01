using System.Collections.Generic;

namespace DoseBookAdmin.Dto.PrescriptionMeta
{
    public class PrescriptionMetaTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class PrescriptionMetaTypeDtoList : List<PrescriptionMetaTypeDto>
    {

    }

}
