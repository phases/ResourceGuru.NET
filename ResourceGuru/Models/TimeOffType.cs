using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ResourceGuru.Models
{
    public class TimeOffType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }
    }
}