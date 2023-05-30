using Newtonsoft.Json;

namespace AntiCorruptionLayer.Domain
{
    public class RepositoryCreateInputModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }
        [JsonProperty("auto_init")]
        public bool AutoInit { get; set; }
    }
}
