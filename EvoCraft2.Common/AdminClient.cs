using System.ServiceModel;

namespace EvoCraft2.Common
{
    public class AdminClient : IAdminService
    {
        private EndpointAddress endpoint;
        private ChannelFactory<IAdminService> channel;
        private IAdminService service;

        public void SetServerDetails(GameDescription gameDetails)
        {
             ServiceChannel.SetServerDetails(gameDetails);
        }

        private IAdminService ServiceChannel
        {
            get
            {
                if (service == null)
                {
                    if (endpoint != null)
                    {
                        channel = new ChannelFactory<IAdminService>(ServiceHelper.GetNetTcpBinding(), endpoint);

                        service = channel.CreateChannel();
                        //(service as ICommunicationObject).Closed += ChatClient_Closed;
                        //(service as ICommunicationObject).Faulted += ChatClient_Closed;
                    }
                }
                return service;
            }
        }

        public AdminClient()
        {
            endpoint = new EndpointAddress(ServiceHelper.GetAdminServiceUri());
        }
    }
}
