using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class ResourceService
    {
        protected RequestHelper _requestHelper;
        public ResourceService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public List<Resource> GetResources(string subdomain, int? limit = null, int? offset = 0)
        {
            string url = string.Format("/v1/{0}/resources?limit={1}&offset={2}", subdomain, limit, offset);
            return _requestHelper.Get<List<Resource>>(url);
        }

        public List<Resource> GetArchivedResources(string subdomain, int? limit = null, int? offset = null)
        {
            string url = string.Format("/v1/{0}/resources/archived", subdomain);
            return _requestHelper.Get<List<Resource>>(url);
        }

        public ResourceDetail GetResource(string subdomain, int resourceId)
        {
            string url = string.Format("/v1/{0}/resources/{1}", subdomain, resourceId);
            return _requestHelper.Get<ResourceDetail>(url);
        }

        public Resource AddNewResource(string subdomain, ResourceRequest resourceRequest)
        {
            string url = string.Format("/v1/{0}/resources", subdomain);
            return _requestHelper.Post<Resource>(url, resourceRequest);
        }

        public Resource UpdateResource(string subdomain, int resourceId, ResourceRequest resourceRequest)
        {
            string url = string.Format("/v1/{0}/resources/{1}", subdomain, resourceId);
            return _requestHelper.Put<Resource>(url, resourceRequest);
        }

        public void DeleteResource(string subdomain, int resourceId)
        {
            string url = string.Format("/v1/{0}/resources/{1}", subdomain, resourceId);
            _requestHelper.Delete<dynamic>(url);
        }
    }
}
