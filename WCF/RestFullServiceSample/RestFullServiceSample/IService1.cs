using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RestFullServiceSample
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "Students", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Student> GetStudentDetails();

        // TODO: Add your service operations here  
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.  
    [DataContract]
    public class Student
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
