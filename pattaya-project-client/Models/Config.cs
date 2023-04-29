using System;

namespace pattaya_project_client.Models
{
    public static class Config
    {
        public static string BotServer = "http://localhost:3000/";
        public static string BotToken = "zilVjQAv5ZdCk0Ybe3t5RvqhUHevwPgIVA84gRE7GRU";
        public static string BotTokenMask = "$$$$$$";

        public static string BotSendResult = "bot_send_task_result";
        public static string BotReceiveTask = "bot_receive_task";
        public static string BotCheckIn = "bot_checkin";

        public static TimeSpan BotWSTimeout = TimeSpan.FromMinutes(60);
        public static int SignalDelay = 600000;

    }
}
