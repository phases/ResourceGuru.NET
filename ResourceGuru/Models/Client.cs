using Newtonsoft.Json;
using System;

namespace ResourceGuru.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}