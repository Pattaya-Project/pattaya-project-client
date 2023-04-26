using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;


namespace pattaya_project_client.Services
{
    public class Execute
    {
        public static string ExecuteCommand(string fileName, string arguments)
        {

            var startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            var process = Process.Start(startInfo);

            string output = "";

            using (process.StandardOutput)
            {
                output += process.StandardOutput.ReadToEnd();
            }
            using (process.StandardError)
            {
                output += process.StandardError.ReadToEnd();
            }

            return output;
        }

        public static string ExecuteAssembly(byte[] asm, string[] argumets = null)
        {

            if (argumets is null)
            {
                argumets = new string[] { };
            }


            var currentOut = Console.Out;
            var currentError = Console.Error;

            var ms = new MemoryStream();
            var sw = new StreamWriter(ms)
            {
                AutoFlush = true
            };

            Console.SetOut(sw);
            Console.SetOut(sw);

            var assembly = Assembly.Load(asm);

            assembly.EntryPoint.Invoke(null, new object[] { argumets });

            Console.Out.Flush();
            Console.Error.Flush();

            var output = Encoding.UTF8.GetString(ms.ToArray());
            Console.SetOut(currentOut);
            Console.SetError(currentError);

            ms.Dispose();
            sw.Dispose();

            return output;
        }
    }
}