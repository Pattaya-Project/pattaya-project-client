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

        [JsonPropertyName("file")]
        public string File { get; set; }
    }
}
