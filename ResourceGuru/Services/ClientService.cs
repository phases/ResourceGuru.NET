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
    }
}
