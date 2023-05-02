using pattaya_project_client.Models;

namespace pattaya_project_client.Commands.Interfaces
{
    public abstract class BotCommand
    {
        public abstract string TaskName { get; }
        public abstract BotTaskResult RunTask(BotTask task);
    }
}
