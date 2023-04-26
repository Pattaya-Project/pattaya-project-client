using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace pattaya_project_client
{
    public class Program
    {

        private static List<BotCommand> _commands = new List<BotCommand>();
        private static SocketIO _client;
        static async Task Main(string[] args)
        {

            LoadBotCommands();

            _client = new SocketIO(Config.BotServer, new SocketIOOptions
            {
                ExtraHeaders = new Dictionary<string, string>
                {
                    { "Authorization", $"{Config.BotTokenMask} {Config.BotToken}" }
                }
            });

            _client.On("bot_receive_task", response =>
            {
                var task = response.GetValue<BotTask>(0);
                Console.WriteLine(task.Command);
                ProcessBotTask(task);

            });

            _client.OnConnected += async (sender, e) =>
            {
                var botCharacter = Utils.BotUtil.GenerateChracter();
                Console.WriteLine($"Bot has been connected to server: {Config.BotServer}");
                await _client.EmitAsync("bot_checkin", botCharacter);

            };
            await _client.ConnectAsync();
            Console.ReadLine();

        }

        private static void ProcessBotTask(BotTask task)
        {
            var command = _commands.FirstOrDefault(c => c.TaskName.Equals(task.Command, StringComparison.OrdinalIgnoreCase));

            if (command is null)
            {
                EmitTaskResult(task.TaskId, "Command not found");
                return;
            }

            try
            {
                var result = command.RunTask(task);
                EmitTaskResult(task.TaskId, result);
            }
            catch (Exception e)
            {

                EmitTaskResult(task.TaskId, e.Message);
            }

        }

        private static void EmitTaskResult(string taskId, string result)
        {
            var taskResult = new BotTaskResult
            {
                TaskId = taskId,
                Result = result
            };
            _client.EmitAsync("bot_send_task_result", taskResult);
        }

        private static void LoadBotCommands()
        {
            var self = Assembly.GetEntryAssembly();
            foreach (var type in self.GetTypes())
            {
                if (type.IsSubclassOf(typeof(BotCommand)))
                {
                    var command = (BotCommand)Activator.CreateInstance(type);
                    _commands.Add(command);
                }
            }
        }
    }
}
