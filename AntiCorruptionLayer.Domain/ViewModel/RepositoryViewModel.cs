using Newtonsoft.Json;

namespace AntiCorruptionLayer.Domain.ViewModel
{
    public class RepositoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        public string Description { get; set; }
        public bool Private { get; set; }
    }
}