using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class GazetteersVM
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public IFormFile FileNameData { get; set; }
        public DateTime FileUploadDate { get; set; }
        public string CreationDateToDisplay { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public byte[] Timestamp { get; set; }

        public GazetteersData GazetteersData { get; set; }
    }

    public class GazetteersOPSLocations
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string AreaAndAddress1 { get; set; }
        public string AreaAndAddress2 { get; set; }
        public string AreaAndAddress3 { get; set; }
        public string AreaAndAddress4 { get; set; }
        public string AreaAndAddress5 { get; set; }
        public string AreaAndAddress6 { get; set; }
        public string AreaAndAddress7 { get; set; }
        public string RQM { get; set; }
        public string OpsContact { get; set; }
        public string OpsDept { get; set; }
        public string AreaManager { get; set; }
        public string CSM { get; set; }
        public string MC1 { get; set; }
        public string MC2 { get; set; }
        public string MCEmail1 { get; set; }
        public string MCEmail2 { get; set; }
        public string MCEmail3 { get; set; }
        public string Store { get; set; }
    }

    public class GazetteersPickfordsMoveCentreTerritory
    {
        public int Id { get; set; }
        public string PostDistrict { get; set; }
        public string SalesCentre { get; set; }
        public string MoveCentre { get; set; }
        public string Code { get; set; }
    }

    public class GazetteersSalesCentres
    {
        public int Id { get; set; }
        public string Branch { get; set; }
        public string CustomerNumber { get; set; }
        public string GroupSalesManager { get; set; }
        public string CustomerServiceManager { get; set; }
        public string AreaManager { get; set; }
        public string OpsDept { get; set; }
    }

    public class GazetteersDetails
    {
        public int Id { get; set; }
        public int GazetteersId { get; set; }
        public string PostalCode { get; set; }
        public string SalesCentre { get; set; }
        public string CustomerNumber { get; set; }
        public string GroupSalesManager { get; set; }
        public string CustomerServiceManager { get; set; }
        public string AreaManager { get; set; }
        public string OperationsLocation { get; set; }
        public string BookingDeptCode { get; set; }
        public string ResourceQualityManager { get; set; }
        public string OperationalContact { get; set; }
    }
}
