using System.Collections.Generic;

namespace AntiCorruptionLayer.Domain
{
    public class WebhookInputModel
    {
        public bool Active { get; set; }
        public List<string> Events { get; set; }
    }
}
