﻿using System;
using System.Text.Json.Serialization;


namespace pattaya_project_client.Models
{
    public class BotTask
    {
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; }

        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }

        [JsonPropertyName("incomingFile")]
        public string IncomingFile { get; set; }

        [JsonPropertyName("incomingFilename")]
        public string IncomingFilename { get; set; }

        public byte[] FileBytes
        {
            get
            {
                if (string.IsNullOrEmpty(IncomingFile)) return new byte[0];
                return Convert.FromBase64String(IncomingFile);
            }
        }
    }
}
