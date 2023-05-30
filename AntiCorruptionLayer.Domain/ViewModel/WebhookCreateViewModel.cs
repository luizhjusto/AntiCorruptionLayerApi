using Newtonsoft.Json;

namespace AntiCorruptionLayer.Domain.ViewModel
{
    public class WebhookCreateViewModel
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        [JsonProperty("test_url")]
        public string TestUrl { get; set; }
    }
}
