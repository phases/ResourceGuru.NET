using System.Collections.Generic;
using Newtonsoft.Json;

namespace ResourceGuru.Models
{
    public class ResourceTypeDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("human")]
        public bool? Human { get; set; }

        [JsonProperty("custom_fields")]
        public CustomField[] CustomFields { get; set; }
    }

    public class CustomField
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("custom_field_options")]
        public List<CustomFieldOption> CustomFieldOptions { get; set; }
    }
}