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

        public List<Project>  GetProjects(string subdomain, int? limit = null, int? offset = null)
        {
            string url = string.Format("/v1/{0}/projects", subdomain);
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

        public Project AddNewProject(string subdomain, string color, string name, string notes)
        {
            string url = string.Format("/v1/{0}/projects ", subdomain);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes
            };

            return _requestHelper.Post<Project>(url, requestData);
        }

        public Project UpdateProject(string subdomain, int projectId, string color, string name, string notes)
        {
            string url = string.Format("/v1/{0}/projects/{1}", subdomain, projectId);
            var requestData = new
            {
                color = color,
                name = name,
                notes = notes
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