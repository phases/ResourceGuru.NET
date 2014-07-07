using ResourceGuru.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class ClientService
    {
        protected ResourceGuruClient _client;
        public ClientService(ResourceGuruClient client)
        {
            _client = client;
        }

        public List<Client> GetClients(string subdomain, int? limit, int? offset)
        {
            string url = string.Format("/v1/{0}/clients", subdomain);
            return _client.requestHelper.Get<List<Client>>(url);
        }

        public List<Client> GetArchivedClients(string subdomain, int? limit, int? offset)
        {
            string url = string.Format("/v1/{0}/clients/archived", subdomain);
            return _client.requestHelper.Get<List<Client>>(url);
        }

        public ClientDetail GetClient(string subdomain, int clientId)
        {
            string url = string.Format("/v1/{0}/clients/{1}", subdomain, clientId);
            return _client.requestHelper.Get<ClientDetail>(url);
        }

        public Client AddNewClient(string subdomain, string color, string name, string notes)
        {
            string url = string.Format("/v1/{0}/clients", subdomain);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes
            };

            return _client.requestHelper.Post<Client>(url, requestData);
        }

        public Client UpdateClient(string subdomain, int clientId, string color, string name, string notes)
        {
            string url = string.Format("/v1/{0}/clients/{1}", subdomain, clientId);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes
            };

            return _client.requestHelper.Put<Client>(url, requestData);
        }

        public void DeleteClient(string subdomain, int clientId)
        {
            string url = string.Format("/v1/{0}/clients/{1}", subdomain, clientId);
            _client.requestHelper.Delete<dynamic>(url);
        }
    }
}
