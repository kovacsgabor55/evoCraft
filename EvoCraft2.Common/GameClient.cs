using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace EvoCraft2.Common
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class GameClient : IClientService, IServiceCallback
    {
        public static List<Command> CommandList = new List<Command>();

        public GameClient(EndpointAddress endpointAddress)
        {
            endpoint = endpointAddress;
        }

        private EndpointAddress endpoint;
        private DuplexChannelFactory<IClientService> channel;
        private IClientService service;

        public event EventHandler GameStarted;
        public event EventHandler<string> MessageReceived;
        public event EventHandler<List<Unit>> MapReceived;
        public event EventHandler<Command> CommandReceived;

        public void StartGame() {
           if (GameStarted != null)
           {
               GameStarted(this, null);
           }
        }

        public void Command()
        {
            if (MapReceived != null)
            {
                MapReceived(this, null);
            }
        }

        public GameDescription GetServerDetails()
        {
            return ServiceChannel.GetServerDetails();
        }

        private IClientService ServiceChannel
        {
            get
            {
                if (service == null)
                {
                    if (endpoint != null)
                    {
                        channel = new DuplexChannelFactory<IClientService>(this, ServiceHelper.GetNetTcpBinding(), endpoint);

                        service = channel.CreateChannel();
                        //(service as ICommunicationObject).Closed += ChatClient_Closed;
                        //(service as ICommunicationObject).Faulted += ChatClient_Closed;
                    }
                }
                return service;
            }
        }

        public void Join(string message)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, message);
            }
            //Console.WriteLine(message);
        }

        public string JoinGame(string person)
        {
            return ServiceChannel.JoinGame(person);
        }

        public void  SendMessage(string message)
        {
            ServiceChannel.SendMessage(message);
        }







        //////////////////////// 
        public void AddCommand(Command command)
        {

            ServiceChannel.AddCommand(command);

            //if (command != null)
            //{
            //    CommandList.Add(command);
            //}
        }

        public void SendMap(List<Unit> map)
        {
            throw new NotImplementedException();
        }

        public void GetMap(List<Unit> map)
        {
            throw new NotImplementedException();
        }
    }
}
