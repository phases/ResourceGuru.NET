using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Models
{
    public class ResourceRequest
    {
        /// <summary>
        /// For non human resources
        /// </summary>
        [JsonProperty("name")]

        // For human resources
        public string Name { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("invite")]
        public bool Invite { get; set; }

        /// <summary>
        /// Meeting rooms only
        /// </summary>
        [JsonProperty("capacity")]
        public int? Capacity { get; set; }

        // <summary>
        /// Vehicle only
        /// </summary>
        [JsonProperty("registration_number")]
        public string RegistrationNumber { get; set; }

        // Other parameters
        [JsonProperty("resource_type_id")]
        public int ResourceTypeId { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("bookable")]
        public bool Bookable { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("custom_field_option_ids")]
        public int[] CustomFieldOptionIds { get; set; }
    }
}
