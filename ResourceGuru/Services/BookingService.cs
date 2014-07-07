using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class BookingService
    {
        protected ResourceGuruClient _client;
        public BookingService(ResourceGuruClient client)
        {
            _client = client;
        }

        public List<Booking> GetBookings(string subdomain, DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? offset = null, int? bookerId = null)
        {
            string url = string.Format("/v1/{0}/bookings", subdomain);
            var requestData = new Dictionary<string, string>
            {
                {"start_date", Utilities.FormatDateForRequest(startDate)},
                {"end_date", Utilities.FormatDateForRequest(endDate)},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"booker_id", bookerId.ToString()}
            };

            return _client.requestHelper.Get<List<Booking>>(url, requestData);
        }

        public List<Booking> GetBookingsForProject(string subdomain, int projectId)
        {
            string url = string.Format("/v1/{0}/projects/{1}/bookings", subdomain, projectId);
            return _client.requestHelper.Get<List<Booking>>(url);
        }
    }
}
