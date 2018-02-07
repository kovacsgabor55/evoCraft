using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Discovery;
using System.Threading;
using System.Threading.Tasks;

namespace EvoCraft2.Common
{
    public static class ServiceFinder
    {
        public static void StartSearchAsync()
        {
            new TaskFactory().StartNew(() =>
            {
                isRunning = true;
                while (isRunning)
                {
                    FindService();
                    if (task != null)
                    {
                        Task.Delay(60 * 1000).Wait(cancellationToken);
                    }
                }
            });
        }

        public static void FindServiceAsync()
        {
            discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            discoveryClient.FindProgressChanged += new EventHandler<FindProgressChangedEventArgs>(discoveryClient_FindProgressChanged);
            discoveryClient.FindAsync(new FindCriteria(typeof(IClientService)));   
        }

        public static void discoveryClient_FindProgressChanged(object sender, FindProgressChangedEventArgs e)
        {
            lock (availableServices)
            {
                EndpointDiscoveryMetadata edm = e.EndpointDiscoveryMetadata;
                //foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in e.)
                //{

                GameServer serverToAdd = edm.ToServer();
                    // todo remove update.

                    if (availableServices.All(server => server.ServerAddress != serverToAdd.ServerAddress))
                    {
                        availableServices.Add(edm.ToServer());
                    }
                //}
                //SearchCompleted?.Invoke(availableServices.Cast<GameDescription>().ToList());
            }
        }

        internal static void CancelSearch()
        {
            isRunning = false;
            cancellationTokenSource.Cancel();
            task = null;
        }

        internal static void WaitForServers()
        {
            //task?.Wait();
        }

        //internal static EndpointAddress GetEndpointAddress(Guid id)
        //{
        //    //lock (availableServices)
        //    //{
        //    //    var server = availableServices.FirstOrDefault(item => item.ServerId == id);
        //    //    return server?.EndpointAddress;
        //    //}
        //}

        //internal static List<Server> GetAllServers()
        //{
        //    lock (availableServices)
        //    {
        //        return availableServices;
        //    }
        //}

        private static void FindService()
        {
            discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            task = discoveryClient.FindTaskAsync(new FindCriteria(typeof(IClientService)), cancellationToken).ContinueWith(DiscoveryClient_FindCompleted, TaskContinuationOptions.NotOnCanceled);
        }

        private static void DiscoveryClient_FindCompleted(Task<FindResponse> e)
        {
            if (e.Exception == null && e.Result.Endpoints.Any())
            {
                lock (availableServices)
                {
                    foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in e.Result.Endpoints)
                    {

                        GameServer serverToAdd = endpointDiscoveryMetadata.ToServer();
                        // todo remove update.

                        if (availableServices.All(server => server.ServerAddress != serverToAdd.ServerAddress))
                        {
                            availableServices.Add(endpointDiscoveryMetadata.ToServer());
                        }
                    }
                    //SearchCompleted?.Invoke(availableServices.Cast<GameDescription>().ToList());
                }
            }
        }

        public static List<GameServer> GetAviableServices()
        {
            return availableServices;
        }

        public delegate void SearchCompletedHandler(List<GameDescription> servers);

        public static event SearchCompletedHandler SearchCompleted;
        private static DiscoveryClient discoveryClient;
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken cancellationToken = cancellationTokenSource.Token;
        private static readonly List<GameServer> availableServices = new List<GameServer>();
        private static Task task;

        private static bool isRunning;
    }
}
