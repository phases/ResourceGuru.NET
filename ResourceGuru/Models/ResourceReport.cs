using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Models
{
    class ResourceReport
    {

        [JsonProperty("booked")]
        public int Booked { get; set; }

        [JsonProperty("unbooked")]
        public int Unbooked { get; set; }

        [JsonProperty("availability")]
        public int Availability { get; set; }

        [JsonProperty("waiting_list")]
        public int WaitingList { get; set; }

        [JsonProperty("utilization")]
        public double Utilization { get; set; }

        [JsonProperty("resources")]
        public List<IndividualResource> Resources { get; set; }
    }

    public class IndividualResource
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("booked")]
        public int Booked { get; set; }

        [JsonProperty("unbooked")]
        public int Unbooked { get; set; }

        [JsonProperty("availability")]
        public int Availability { get; set; }

        [JsonProperty("waiting_list")]
        public int WaitingList { get; set; }

        [JsonProperty("utilization")]
        public int Utilization { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
