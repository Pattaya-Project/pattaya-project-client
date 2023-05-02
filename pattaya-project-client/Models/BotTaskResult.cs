using System.Text.Json.Serialization;


namespace pattaya_project_client.Models
{
    public class BotTaskResult
    {
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("respondingFile")]
        public string RespondingFile { get; set; }

        [JsonPropertyName("respondingFilename")]
        public string RespondingFilename { get; set; }
    }
}

