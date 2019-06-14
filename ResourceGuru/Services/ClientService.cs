using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class ClientService
    {
        protected RequestHelper _requestHelper;
        public ClientService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public List<Client> GetClients(string subdomain, int? limit = null, int? offset = 0)
        {
            string url = string.Format("/v1/{0}/clients?limit={1}&offset={2}", subdomain,limit,offset);
            return _requestHelper.Get<List<Client>>(url);
        }

        public List<Client> GetArchivedClients(string subdomain, int? limit = null, int? offset = null)
        {
            string url = string.Format("/v1/{0}/clients/archived", subdomain);
            return _requestHelper.Get<List<Client>>(url);
        }

        public ClientDetail GetClient(string subdomain, int clientId)
        {
            string url = string.Format("/v1/{0}/clients/{1}", subdomain, clientId);
            return _requestHelper.Get<ClientDetail>(url);
        }

        public Client AddNewClient(string subdomain, string name, string notes, string color = null)
        {
            string url = string.Format("/v1/{0}/clients", subdomain);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes
            };

            return _requestHelper.Post<Client>(url, requestData);
        }

        public Client UpdateClient(string subdomain, int clientId, string name, string notes, string color = null)
        {
            string url = string.Format("/v1/{0}/clients/{1}", subdomain, clientId);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes
            };

            return _requestHelper.Put<Client>(url, requestData);
        }

        public void DeleteClient(string subdomain, int clientId)
        {
            string url = string.Format("/v1/{0}/clients/{1}", subdomain, clientId);
            _requestHelper.Delete<dynamic>(url);
        }
    }
}
