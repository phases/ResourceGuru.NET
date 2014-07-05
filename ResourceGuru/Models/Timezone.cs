using Newtonsoft.Json;

namespace ResourceGuru.Models
{
    public class Timezone
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
