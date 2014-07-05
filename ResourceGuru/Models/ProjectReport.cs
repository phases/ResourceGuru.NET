using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Models
{
    public class ProjectReport
    {
        [JsonProperty("booked")]
        public int Booked { get; set; }

        [JsonProperty("waiting_list")]
        public int WaitingList { get; set; }

        [JsonProperty("projects")]
        public List<ProjectDetail> Projects { get; set; }
    }

}
