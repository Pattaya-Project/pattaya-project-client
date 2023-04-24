using pattaya_project_client.Models;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pattaya_project_client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var client = new SocketIO(Config.BotServer, new SocketIOOptions
            {
                ExtraHeaders = new Dictionary<string, string>
                {
                    { "Authorization", $"{Config.BotTokenMask} {Config.BotToken}" }
                }
            });

            client.On("bot_receive_task", response =>
            {
                Console.WriteLine(response);
                string text = response.GetValue<string>();
            });

            client.OnConnected += async (sender, e) =>
            {
                var botCharacter = Utils.BotUtil.GenerateChracter();
                Console.WriteLine($"Bot has been connected to server: {Config.BotServer}");
                await client.EmitAsync("bot_checkin", botCharacter);

            };


            await client.ConnectAsync();

            Console.ReadLine();


        }
    }
}
