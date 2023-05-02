using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;

namespace pattaya_project_client.Commands.Implements
{
    public class PingBot : BotCommand
    {
        public override string TaskName => "pingbot";

        public override BotTaskResult RunTask(BotTask task)
        {

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = "Hello, Welcome to Pattaya :)"
            };
        }
    }
}
