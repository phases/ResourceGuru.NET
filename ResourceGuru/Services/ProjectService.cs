using ResourceGuru.Models;
using ResourceGuru.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGuru.Services
{
    public class ProjectService
    {
        protected RequestHelper _requestHelper;
        public ProjectService(RequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public List<Project> GetProjects(string subdomain, int? limit = null, int? offset = 0)
        {
            string url = string.Format("/v1/{0}/projects?limit={1}&offset={2}", subdomain, limit, offset);
            return _requestHelper.Get<List<Project>>(url);
        }

        public List<Project> GetArchivedProjects(string subdomain, int? limit = null, int? offset = null)
        {
            string url = string.Format("/v1/{0}/projects/archived", subdomain);
            return _requestHelper.Get<List<Project>>(url);
        }

        public ProjectDetail GetProject(string subdomain, int projectId)
        {
            string url = string.Format("/v1/{0}/projects/{1}", subdomain, projectId);
            return _requestHelper.Get<ProjectDetail>(url);
        }

        public Project AddNewProject(string subdomain, string name, string notes, int? clientId = null, string color = null)
        {
            string url = string.Format("/v1/{0}/projects ", subdomain);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes,
                client_id = clientId != null?clientId.Value : 0
            };

            return _requestHelper.Post<Project>(url, requestData);
        }

        public Project UpdateProject(string subdomain, int projectId, string name, string notes, int? clientId = null, string color = null)
        {
            string url = string.Format("/v1/{0}/projects/{1}", subdomain, projectId);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes,
                client_id = clientId != null ? clientId.Value : 0
            };

            return _requestHelper.Put<Project>(url, requestData);
        }

        public void DeleteClient(string subdomain, int projectId)
        {
            string url = string.Format("/v1/{0}/projects/{1}", subdomain, projectId);
            _requestHelper.Delete<dynamic>(url);
        }
    }
}