using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using ChatBackend;

namespace ChatBackend
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatBackend : IChatBackend
    {
        DisplayMessageDelegate displayMessageDelegate = null;

        private ChatBackend()
        {
        }

        public ChatBackend(DisplayMessageDelegate dmd)
        {
            displayMessageDelegate = dmd;
            StartService();
        }

        public void DisplayMessage(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (displayMessageDelegate != null)
            {
                displayMessageDelegate(composite);
            }
        }

        private string myUserName = "John Doe";
        private ServiceHost host = null;
        private ChannelFactory<IChatBackend> channelFactory = null;
        private IChatBackend channel;

        public void SendMessage(string text)
        {
            if (text.StartsWith("setname:", StringComparison.OrdinalIgnoreCase))
            {
                myUserName = text.Substring("setname:".Length).Trim();
                displayMessageDelegate(new CompositeType("Event", "Setting your name to " + myUserName));
            }
            else
            {
                channel.DisplayMessage(new CompositeType(myUserName, text));
            }
        }

        private void StartService()
        {
            host = new ServiceHost(this);
            host.Open();
            channelFactory = new ChannelFactory<IChatBackend>("ChatEndpoint");
            channel = channelFactory.CreateChannel();

            if (File.Exists("options.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);

                string username = info.UserName;

                channel.DisplayMessage(new CompositeType("Event", username + " has entered the conversation."));
            }
        }

        private void StopService()
        {
            if (host != null)
            {
                channel.DisplayMessage(new CompositeType("Event", myUserName + " is leaving the conversation."));
                if (host.State != CommunicationState.Closed)
                {
                    channelFactory.Close();
                    host.Close();
                }
            }
        }
    }
}
