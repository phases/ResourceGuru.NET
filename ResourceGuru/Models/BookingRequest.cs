using Newtonsoft.Json;
using System;

namespace ResourceGuru.Models
{
    public class BookingRequest
    {
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("duration")]
        public int? Duration { get; set; }

        [JsonProperty("start_time")]
        public int? StartTime { get; set; }

        [JsonProperty("resource_id")]
        public int ResourceId { get; set; }

        [JsonProperty("allow_waiting")]
        public bool? AllowWaiting { get; set; }

        [JsonProperty("project_id")]
        public int? ProjectId { get; set; }

        [JsonProperty("client_id")]
        public int? ClientId { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }
    }
}