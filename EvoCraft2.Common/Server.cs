using System.Runtime.Serialization;
using System.ServiceModel;

namespace EvoCraft2.Common
{
    [DataContract]
    public class GameServer : GameDescription
    {
        [DataMember]
        internal string ServerAddress { get; set; }

        [DataMember]
        public EndpointAddress EndpointAddress { get; set; }

        public override string ToString()
        {
            return ServerAddress + " " + EndpointAddress + " " + GameName + " " + MapName;
        }
    }
}