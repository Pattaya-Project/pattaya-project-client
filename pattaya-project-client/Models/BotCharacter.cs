

using System.Text.Json.Serialization;

namespace pattaya_project_client.Models
{
    public class BotCharacter
    {
        [JsonPropertyName("wanIp")]
        public string WanIp { get; set; }
        [JsonPropertyName("lanIp")]
        public string LanIp { get; set; }
        [JsonPropertyName("os")]
        public string Os { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }
        [JsonPropertyName("processName")]
        public string ProcessName { get; set; }
        [JsonPropertyName("processId")]
        public int ProcessId { get; set; }
        [JsonPropertyName("architecture")]
        public string Architecture { get; set; }
        [JsonPropertyName("integrity")]
        public string Integrity { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("hwid")]
        public string HWID { get; set; }
    }
}
