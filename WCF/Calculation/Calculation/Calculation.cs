using System.Runtime.Serialization;

namespace Calculation
{
    [DataContract]
    public class Calculation
    {
        [DataMember]
        public double n1;
        [DataMember]
        public double n2;
    }
}
