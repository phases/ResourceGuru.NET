using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ResourceGuru.Models
{
    public class TimeOffRequest
    {
        [JsonProperty("resource_ids")]
        public List<int> ResourceIds { get; set; }

        [JsonProperty("booker_id")]
        public int BookerId { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("start_time")]
        public int StartTime { get; set; }

        [JsonProperty("end_time")]
        public int EndTime { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("downtime_type_id")]
        public int? DowntimeTypeId { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}