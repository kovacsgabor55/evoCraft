using System;
using System.Net;
using System.ServiceModel;

namespace EvoCraft2.Common
{
    public static class ServiceHelper
    {
        public static NetTcpBinding GetNetTcpBinding()
        {

            NetTcpBinding result = new NetTcpBinding()
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue,
                MaxBufferPoolSize = int.MaxValue,
                ReaderQuotas =
                {
                    MaxArrayLength = int.MaxValue,
                    MaxStringContentLength = int.MaxValue
                },
                ReceiveTimeout = new TimeSpan(1, 0, 0, 0),
                SendTimeout = new TimeSpan(1, 0, 0),
                PortSharingEnabled = true,
                Security =
                {
                    Mode = SecurityMode.None,
                },

            };

            return result;

        }

        public static Uri GetServiceUri()
        {
            return GetServiceUri(serviceName);
        }

        private static Uri GetServiceUri(string serviceName)
        {
            Uri uri = new UriBuilder(Uri.UriSchemeNetTcp, HOSTNAME, PORT, serviceName).Uri;
            return uri;
        }

        public static Uri GetAdminServiceUri()
        {
            string serviceName = "AdminService";
            Uri uri = new UriBuilder(Uri.UriSchemeNetTcp, HOSTNAME, PORT, serviceName).Uri;
            return uri;
        }

        private static readonly string serviceName = "Service";

        private static readonly string HOSTNAME = Dns.GetHostName();

        private static readonly int PORT = 1234;
    }
}
