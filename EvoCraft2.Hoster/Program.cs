using EvoCraft2.Common;
using System;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace EvoCraft2.Hoster
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            //var arguments = Environment.GetCommandLineArgs();

            EvoCraft2.Core.GameService gameService = new Core.GameService();
            //gameService.SetGameDescription(arguments[1], arguments[2]);

            ServiceHost service;

            service = new ServiceHost(gameService, ServiceHelper.GetServiceUri());
            //service.Description.Behaviors.RemoveAll<ServiceDebugBehavior>();
            //service.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
            service.AddServiceEndpoint(typeof(IClientService), ServiceHelper.GetNetTcpBinding(), ServiceHelper.GetServiceUri());
            service.AddServiceEndpoint(typeof(IAdminService), ServiceHelper.GetNetTcpBinding(), ServiceHelper.GetAdminServiceUri());
            service.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
            service.AddServiceEndpoint(new UdpDiscoveryEndpoint());
            service.Open();

            Console.ReadLine();
        }
    }
}
