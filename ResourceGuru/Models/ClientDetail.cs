using Newtonsoft.Json;

namespace ResourceGuru.Models
{
    public class ClientDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}