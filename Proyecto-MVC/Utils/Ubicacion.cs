using System.Net;

namespace Proyecto_MVC.Utils
{
    public class Ubicacion
    {
        public static string getIpAdress(IHttpContextAccessor _httpContextAccessor)
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress != null && remoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                var ethernetIpAddress = GetEthernetIpAddress();
                var wifiIpAddress = GetWifiIpAddress();

                remoteIpAddress = ethernetIpAddress ?? wifiIpAddress;
            }

            return remoteIpAddress?.ToString();
        }

        private static IPAddress GetEthernetIpAddress()
        {
            var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            var ethernetInterface = networkInterfaces.FirstOrDefault(ni => ni.Name == "Ethernet" && ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);

            if (ethernetInterface != null && ethernetInterface.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Ethernet)
            {
                var ipProperties = ethernetInterface.GetIPProperties();
                var ipv4Address = ipProperties.UnicastAddresses
                    .FirstOrDefault(address => address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.Address;

                return ipv4Address;
            }

            return null;
        }

        private static IPAddress GetWifiIpAddress()
        {
            var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            var wifiInterface = networkInterfaces.FirstOrDefault(ni => ni.Name == "Wi-Fi" && ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);

            if (wifiInterface != null && wifiInterface.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211)
            {
                var ipProperties = wifiInterface.GetIPProperties();
                var ipv4Address = ipProperties.UnicastAddresses
                    .FirstOrDefault(address => address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.Address;

                return ipv4Address;
            }

            return null;
        }
    }
}