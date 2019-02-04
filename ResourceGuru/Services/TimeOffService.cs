using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class TimeOffService
    {
        protected RequestHelper _requestHelper;
        public TimeOffService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public List<TimeOff> GetTimeOffs(string subdomain, DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? offset = 0, int? bookerId = null)
        {
            var startDateString = Utilities.FormatDateForRequest(startDate);
            var endDateString = Utilities.FormatDateForRequest(endDate);

            string url = string.Format("/v1/{0}/downtimes", subdomain);
            var requestData = new Dictionary<string, string>
            {
                {"from", startDateString},
                {"to", endDateString},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"booker_id", bookerId.ToString()}
            };

            return _requestHelper.Get<List<TimeOff>>(url, requestData);
        }

        // ResourceGuruApi returns 200 Status Code when the TimeOff is not found...
        public TimeOff GetTimeOffById(string subdomain, int timeOffId)
        {
            string url = string.Format("/v1/{0}/downtimes/{1}", subdomain, timeOffId);
            return _requestHelper.Get<TimeOff>(url);
        }
        
        public TimeOff AddNewTimeOff(string subdomain, TimeOffRequest createTimeOffRequest)
        {
            string url = string.Format("/v1/{0}/downtimes", subdomain);
            return _requestHelper.Post<TimeOff>(url, createTimeOffRequest);
        }

        public void DeleteTimeOff(string subdomain, int timeOffId)
        {
            string url = string.Format("/v1/{0}/downtimes/{1}", subdomain, timeOffId);
            _requestHelper.Delete<dynamic>(url);
        }

        public List<TimeOffType> GetTimeOffTypes(string subdomain)
        {
            string url = string.Format("/v1/{0}/downtime_types", subdomain);
            return _requestHelper.Get<List<TimeOffType>>(url);
        }

        public TimeOff UpdateTimeOff(string subdomain, int timeOffId, TimeOff updateTimeOff)
        {
            string url = string.Format("/v1/{0}/downtimes/{1}", subdomain, timeOffId);
            return _requestHelper.Put<TimeOff>(url, updateTimeOff);
        }

    }
}
