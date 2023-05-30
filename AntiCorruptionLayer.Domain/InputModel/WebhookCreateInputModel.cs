using Newtonsoft.Json;

namespace AntiCorruptionLayer.Domain
{
    public class WebhookCreateInputModel : WebhookInputModel
    {
        public string Name { get; set; }
        [JsonProperty("config")]
        public WebhookConfigInputModel Config { get; set; }
    }
}
