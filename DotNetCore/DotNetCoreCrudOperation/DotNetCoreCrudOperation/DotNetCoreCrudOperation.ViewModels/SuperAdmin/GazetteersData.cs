using System.Collections.Generic;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class GazetteersData
    {
        public List<GazetteersPickfordsMoveCentreTerritory> TerritetoryList { get; set; }
        public List<GazetteersSalesCentres> SalesCentresList { get; set; }
        public List<GazetteersOPSLocations> OperationLocationsList { get; set; }
        public List<GazetteersDetails> GazetteersDetailsList { get; set; }
        public string ExcelErrorMessage { get; set; }
        //public string TerritoryErrorMessage { get; set; }
        //public string SalesCentresErrorMessage { get; set; }
        //public string OpsLocationErrorMessage { get; set; }
    }
}
