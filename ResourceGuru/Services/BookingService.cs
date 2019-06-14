﻿using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class BookingService
    {
        protected RequestHelper _requestHelper;
        public BookingService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public List<Booking> GetBookings(string subdomain, DateTime? startDate = null, DateTime? endDate = null, int? limit = null, int? offset = 0, int? bookerId = null)
        {
            var startDateString = Utilities.FormatDateForRequest(startDate);
            var endDateString = Utilities.FormatDateForRequest(endDate);

             string url = string.Format("/v1/{0}/bookings", subdomain);
            var requestData = new Dictionary<string, string>
            {
                {"start_date", startDateString},
                {"end_date", endDateString},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"booker_id", bookerId.ToString()}
            };

            return _requestHelper.Get<List<Booking>>(url, requestData);
        }

        public List<Booking> GetBookingsForProject(string subdomain, int projectId)
        {
            string url = string.Format("/v1/{0}/projects/{1}/bookings", subdomain, projectId);
            return _requestHelper.Get<List<Booking>>(url);
        }

        public List<Booking> GetBookingsForClient(string subdomain, int clientId)
        {
            string url = string.Format("/v1/{0}/clients/{1}/bookings", subdomain, clientId);
            return _requestHelper.Get<List<Booking>>(url);
        }

        public List<Booking> GetBookingsForResource(string subdomain, int resourceId)
        {
            string url = string.Format("/v1/{0}/resources/{1}/bookings", subdomain, resourceId);
            return _requestHelper.Get<List<Booking>>(url);
        }

        public Booking AddNewBooking(string subdomain, BookingRequest createBookingRequest)
        {
            string url = string.Format("/v1/{0}/bookings", subdomain);
            return _requestHelper.Post<Booking>(url, createBookingRequest);
        }

        public Booking UpdateBooking(string subdomain, int bookingId, BookingRequest createBookingRequest)
        {
            string url = string.Format("/v1/{0}/bookings/{1}", subdomain, bookingId);
            return _requestHelper.Put<Booking>(url, createBookingRequest);
        }

        public void DeleteBooking(string subdomain, int bookingId)
        {
            string url = string.Format("/v1/{0}/bookings/{1}", subdomain, bookingId);
            _requestHelper.Delete<dynamic>(url);
        }
    }
}
