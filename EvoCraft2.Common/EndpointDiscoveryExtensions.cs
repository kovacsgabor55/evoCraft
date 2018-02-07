using System.ServiceModel.Discovery;

namespace EvoCraft2.Common
{
    internal static class EndpointDiscoveryExtensions
    {
        internal static GameServer ToServer(this EndpointDiscoveryMetadata endpointDiscoveryMetadata)
        {
            GameServer server = new GameServer()
            {
                //ServerId = Guid.NewGuid(),
                EndpointAddress = endpointDiscoveryMetadata.Address,
                ServerAddress = endpointDiscoveryMetadata.Address.Uri.Host
                ,
                //Hostname = endpointDiscoveryMetadata.Address.Uri.Host
            };
           GameClient client = new GameClient(endpointDiscoveryMetadata.Address);
            var serverinfo = client.GetServerDetails();
            server.GameName = serverinfo.GameName;
            server.MapName = serverinfo.MapName;

            return server;
        }
    }
}
