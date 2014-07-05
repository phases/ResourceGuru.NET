using Newtonsoft.Json;

namespace ResourceGuru.Models
{
    public class Resource
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("resource_type")]
        public ResourceType ResourceType { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }
    }

    public class ResourceType
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Timezone
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}