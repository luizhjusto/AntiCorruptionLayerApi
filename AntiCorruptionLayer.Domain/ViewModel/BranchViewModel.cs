using Newtonsoft.Json;

namespace AntiCorruptionLayer.Domain.ViewModel
{
    public class BranchViewModel
    {
        public string Name { get; set; }
        public bool Protected { get; set; }
        [JsonProperty("protection_url")]
        public string ProtectionUrl { get; set; }
    }
}
