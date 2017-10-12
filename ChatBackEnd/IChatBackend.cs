using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatBackend
{
    // Defines the set of methods supported by the backend implementation
    // SendMessage to call DisplayMessage
    [ServiceContract]
    interface IChatBackend
    {
        [OperationContract(IsOneWay = true)]
        void DisplayMessage(CompositeType composite);
        void SendMessage(string text);
    }

    // Capable to holding two sting
    // Later would be handy to send more complicated datas
    [DataContract]
    public class CompositeType
    {
        string username = "John Doe";
        string message = "";

        public CompositeType() { }
        public CompositeType(string u, string m)
        {
            username = u;
            message = m;
        }

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }

    // Separate the GUI code from application logic and communication code
    // GUI will use delegate to pass function handler to the backend
    public delegate void DisplayMessageDelegate(CompositeType data);
}
