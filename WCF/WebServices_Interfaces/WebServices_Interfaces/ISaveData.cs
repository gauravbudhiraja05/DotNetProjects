using System.Runtime.Serialization;
using System.ServiceModel;

namespace WebServices_Interfaces
{
    [ServiceContract]
    public interface ISaveData
    {
        [OperationContract]
        void AddData(Data s);

        [OperationContract]
        Data[] GetCurrentData();
    }

    // DataContracts for the Web Service  
    [DataContract]
    public class Data
    {
        [DataMember]
        public string DataLabel { set; get; }
    }
}
