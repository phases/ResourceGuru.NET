using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourceGuru.Models;

namespace ResourceGuru.Services
{
    public class UserService
    {
        protected RequestHelper _requestHelper;
        public UserService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public CurrentUser GetCurrentUser()
        {
            string url = "/v1/me";
            return _requestHelper.Get<CurrentUser>(url);
        }

        public Users GetUser(string subdomain, int id)
        {
            string url = string.Format("/v1/{0}/users/{1}", subdomain, id);
            return _requestHelper.Get<Users>(url);
        }
    }
}
