using Newtonsoft.Json;
using PodioAPI.Models;
using ResourceGuru.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ResourceGuru.Utils
{
    public class RequestHelper
    {
        private OAuthInfo _OAuthInfo;

        public RequestHelper(OAuthInfo OAuthInfo)
        {
            _OAuthInfo = OAuthInfo;
        }

        internal T Get<T>(string url, Dictionary<string, string> requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.GET, url, requestData, options);
        }

        internal T Post<T>(string url, dynamic requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.POST, url, requestData, options);
        }

        internal T Put<T>(string url, dynamic requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.PUT, url, requestData);
        }

        internal T Delete<T>(string url, dynamic requestData = null, dynamic options = null) where T : new()
        {
            return Request<T>(RequestMethod.DELETE, url, requestData);
        }

        private T Request<T>(RequestMethod requestMethod, string url, dynamic requestData, dynamic options = null) where T : new()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            var data = new List<string>();
            string httpMethod = string.Empty;
            string originalUrl = url;

            //To use url other than api.podio.com, ex file download from files.podio.com
            if (options != null && options.ContainsKey("url"))
            {
                url = options["url"];
            }

            switch (requestMethod.ToString())
            {
                case "GET":
                    httpMethod = "GET";
                    requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    if (requestData != null)
                    {
                        string query = EncodeAttributes(requestData);
                        url = url + "?" + query;
                    }
                    requestHeaders["Content-length"] = "0";
                    break;
                case "DELETE":
                    httpMethod = "DELETE";
                    requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    if (requestData != null)
                    {
                        string query = EncodeAttributes(requestData);
                        url = url + "?" + query;
                    }
                    requestHeaders["Content-length"] = "0";
                    break;
                case "POST":
                    httpMethod = "POST";
                    if (options != null && options.ContainsKey("oauth_request") && options["oauth_request"])
                    {
                        data.Add("oauth");
                        requestHeaders["Content-type"] = "application/x-www-form-urlencoded";
                    }
                    else
                    {
                        requestHeaders["Content-type"] = "application/json";
                        data.Add("post");
                    }
                    break;
                case "PUT":
                    httpMethod = "PUT";
                    requestHeaders["Content-type"] = "application/json";
                    data.Add("put");
                    break;
            }

            if (_OAuthInfo != null && !string.IsNullOrEmpty(_OAuthInfo.access_token))
            {
                requestHeaders["Authorization"] = "OAuth2 " + _OAuthInfo.access_token;
                if (options != null && options.ContainsKey("oauth_request") && options["oauth_request"])
                {
                    requestHeaders.Remove("Authorization");
                }
            }
            else
            {
                requestHeaders.Remove("Authorization");
            }

            if (options != null && options.ContainsKey("file_download") && options["file_download"])
                requestHeaders["Accept"] = "*/*";
            else
                requestHeaders["Accept"] = "application/json";

            var request = (HttpWebRequest)WebRequest.Create(url);
            ServicePointManager.Expect100Continue = false;
            request.Proxy = null;
            request.Method = httpMethod;
            request.UserAgent = "ResourceGuru Dotnet Client";

            APIResponse apiResponse = new APIResponse();
            var responseHeaders = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            var responseObject = new T();

            if (requestHeaders.Any())
            {
                if (requestHeaders.ContainsKey("Accept"))
                    request.Accept = requestHeaders["Accept"];
                if (requestHeaders.ContainsKey("Content-type"))
                    request.ContentType = requestHeaders["Content-type"];
                if (requestHeaders.ContainsKey("Content-length"))
                    request.ContentType = requestHeaders["Content-length"];
                if (requestHeaders.ContainsKey("Authorization"))
                    request.Headers.Add("Authorization", requestHeaders["Authorization"]);
            }
            if (data.Any())
            {
                foreach (string item in data)
                {
                    if (item == "oauth")
                        WriteToRequestStream(EncodeAttributes(requestData), request);
                    else
                        WriteToRequestStream(requestData, request);
                }
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    podioResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }
                   
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        podioResponse.Body = sr.ReadToEnd();
                    }
                    podioResponse.Headers = responseHeaders;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    podioResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        podioResponse.Body = sr.ReadToEnd();
                    }
                    podioResponse.Headers = responseHeaders;
                }
            }


            //if (podioResponse.Headers.ContainsKey("Retry-After"))
             //   RateLimitRemaining = int.Parse(podioResponse.Headers["Retry-After"]);
           


            switch (podioResponse.Status)
            {
                case 200:
                case 201:
                    responseObject = JSONSerializer.Deserilaize<T>(podioResponse.Body);
                    break;
                case 204:
                    responseObject = default(T);
                    break;
                case 401:
                    throw new PodioAuthorizationException(podioResponse.Status, "Unauthorized");
                    break;
                case 403:
                    throw new PodioForbiddenException(podioResponse.Status, "Forbidden");
                case 404:
                    throw new PodioNotFoundException(podioResponse.Status, "Not Found");
                case 422:
                    throw new UnprocessableEntityException(podioResponse.Status, "Unprocessable Entity");
                case 500:
                    throw new PodioServerException(podioResponse.Status, podioError);
                case 502:
                case 503:
                case 504:
                    throw new PodioUnavailableException(podioResponse.Status, "Resource Guru is having trouble");
                default:
                    throw new PodioException(podioResponse.Status, "Uncategorized Error");
            }

            return responseObject;
        }

        /// <summary>
        /// Convert dictionay to to query string
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        internal static string EncodeAttributes(Dictionary<string, string> attributes)
        {
            var encodedString = string.Empty;
            if (attributes.Any())
            {
                var parameters = new List<string>();
                foreach (var item in attributes)
                {
                    if (item.Key != string.Empty && !string.IsNullOrEmpty(item.Value))
                    {
                        parameters.Add(HttpUtility.UrlEncode(item.Key) + "=" + (HttpUtility.UrlEncode(item.Value)));
                    }
                }
                if (parameters.Any())
                    encodedString = string.Join("&", parameters.ToArray());
            }

            return encodedString;
        }

        internal void WriteToRequestStream(object obj, HttpWebRequest request)
        {
            if (obj != null)
            {
                try
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        if (obj is string)
                            streamWriter.Write(obj);
                        else
                            streamWriter.Write(JsonConvert.SerializeObject(obj));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    public enum RequestMethod
    {
        GET, POST, PUT, DELETE
    }
}
