using System.Runtime.Serialization;

namespace EvoCraft2.Common
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FirstName { get; set; }
    }
}
