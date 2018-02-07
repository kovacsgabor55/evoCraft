using System.Runtime.Serialization;

namespace EvoCraft2.Common
{
    [DataContract]
    public class GameDescription
    {
        [DataMember]
        public string GameName { get; set; }

        [DataMember]
        public string MapName { get; set; }
    }
}
