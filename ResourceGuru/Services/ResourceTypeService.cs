using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class ResourceTypeService
    {
         protected RequestHelper _requestHelper;
         public ResourceTypeService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

         public List<ResourceTypeDetail> GetResourceTypes(string subdomain)
         {
             string url = string.Format("/v1/{0}/resource_types", subdomain);
             return _requestHelper.Get<List<ResourceTypeDetail>>(url);
         }
    }
}
