using pattaya_project_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pattaya_project_client.Commands.Interfaces
{
    public abstract class BotCommand
    {
        public abstract string TaskName { get; }
        public abstract string RunTask(BotTask task);
    }
}
