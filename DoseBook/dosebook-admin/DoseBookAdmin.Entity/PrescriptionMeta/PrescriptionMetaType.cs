using System.Collections.Generic;

namespace DoseBookAdmin.Entity.PrescriptionMeta
{
    public class PrescriptionMetaTypeEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class PrescriptionMetaTypeEntityList : List<PrescriptionMetaTypeEntity>
    {

    }

}
