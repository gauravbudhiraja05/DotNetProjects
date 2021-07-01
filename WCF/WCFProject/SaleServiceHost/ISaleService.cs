using System.ServiceModel;

namespace SaleServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISaleService" in both code and config file together.
    [ServiceContract]
    public interface ISaleService
    {
        [OperationContract]
        void DoWork();
    }
}
