using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceGuru.Models;
using ResourceGuru.Utils;

namespace ResourceGuru.Services
{
    public class HookService
    {
        protected RequestHelper _requestHelper;
        public HookService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public List<Webhook> GetWebhooks(string subdomain)
        {
            string url = string.Format("/v1/{0}/webhooks ", subdomain);
            return _requestHelper.Get<List<Webhook>>(url);
        }

        public Webhook GetWebhook(string subdomain, int webhookId)
        {
            string url = string.Format("/v1/{0}/webhooks/{1}", subdomain, webhookId);
            return _requestHelper.Get<Webhook>(url);
        }

        public Webhook AddNewWebHook(string subdomain, string name, string payloadUrl, List<string> events, string secret = null)
        {
            string url = string.Format("/v1/{0}/webhooks", subdomain);
            var requestData = new
            {
                name = name,
                payload_url = payloadUrl,
                events = events,
                secret = secret
            };
            return _requestHelper.Post<Webhook>(url, requestData);
        }

        public Webhook UpdateWebhook(string subdomain, int webhookId, string name, string payloadUrl, List<string> events, string secret = null)
        {
            string url = string.Format("/v1/{0}/webhooks/{1}", subdomain,webhookId);
            var requestData = new
            {
                name = name,
                payload_url = payloadUrl,
                events = events,
                secret = secret
            };
            return _requestHelper.Put<Webhook>(url, requestData);
        }

        public void DeleteWebHook(string subdomain, int webhookId)
        {
            string url = string.Format("/v1/{0}/webhooks/{1}", subdomain, webhookId);
            _requestHelper.Delete<dynamic>(url);
        }

        public void TestWebHook(string subdomain, int webhookId)
        {
            string url = string.Format("/v1/{0}/webhooks/{1}/test ", subdomain, webhookId);
            _requestHelper.Get<dynamic>(url);
        }
    }
}
