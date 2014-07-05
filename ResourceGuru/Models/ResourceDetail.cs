using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ResourceGuru.Models
{
    class ResourceDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }

        [JsonProperty("bookable")]
        public bool? Bookable { get; set; }

        [JsonProperty("available_periods")]
        public List<AvailablePeriod> AvailablePeriods { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("job_title")]
        public string JobTitle { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("resource_type")]
        public ResourceType ResourceType { get; set; }

        [JsonProperty("selected_custom_field_options")]
        public List<CustomFieldOption> SelectedCustomFieldOptions { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }
    }

    public class AvailablePeriod
    {

        [JsonProperty("week_day")]
        public int WeekDay { get; set; }

        [JsonProperty("start_time")]
        public int StartTime { get; set; }

        [JsonProperty("end_time")]
        public int EndTime { get; set; }

        [JsonProperty("valid_from")]
        public DateTime? ValidFrom { get; set; }

        [JsonProperty("valid_until")]
        public DateTime? ValidUntil { get; set; }
    }

    public class CustomFieldOption
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}