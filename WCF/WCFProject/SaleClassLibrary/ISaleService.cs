using System.Collections.Generic;
using System.ServiceModel;

namespace SaleClassLibrary
{
    [ServiceContract]
    interface ISaleService
    {
        [OperationContract]
        bool InsertCustomer(Customer obj);

        [OperationContract]
        List<Customer> GetAllCustomer();

        [OperationContract]
        bool DeleteCustomer(int Cid);

        [OperationContract]
        bool UpdateCustomer(Customer obj);
    }
}
