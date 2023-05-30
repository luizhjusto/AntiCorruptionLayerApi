using Newtonsoft.Json;
using System.Collections.Generic;

namespace AntiCorruptionLayer.Domain
{
    public class WebhookUpdateInputModel : WebhookInputModel
    {
        public List<string> RemoveEvents { get; set; }
        public List<string> AddEvents { get; set; }
        [JsonProperty("config")]
        public WebhookConfigInputModel Config { get; set; }
    }
}
