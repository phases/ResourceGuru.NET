using Newtonsoft.Json;
using PodioAPI.Models;
using ResourceGuru.Authentication;
using ResourceGuru.Exceptions;
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
        private ResourceGuruClient _ResourceGuruClient;
        private WebProxy _Proxy { get; set; }
        public RequestHelper(OAuthInfo OAuthInfo, ResourceGuruClient resourceGuruClient, WebProxy proxy)
        {
            _OAuthInfo = OAuthInfo;
            _ResourceGuruClient = resourceGuruClient;
            _Proxy = proxy;
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
            url = "https://api.resourceguruapp.com" + url;
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
                if (!string.IsNullOrEmpty(_OAuthInfo.expires_in))
                {
                    if (options == null || (options != null && !options.ContainsKey("oauth_request") && !options["oauth_request"]))
                    {
                        int expirationStamp;
                        var dtToCheck = new DateTime();
                        if (Int32.TryParse(_OAuthInfo.expires_in, out expirationStamp))
                        {
                            dtToCheck = Convert.ToDateTime(String.Format("{0:d/M/yyyy HH:mm:ss}", new System.DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(expirationStamp))).AddDays(6);
                        }
                        else
                        {
                            dtToCheck = Convert.ToDateTime(_OAuthInfo.expires_in);
                        }

                        if (dtToCheck < DateTime.Now)
                        {
                            _OAuthInfo = _ResourceGuruClient.RefreshAccessToken(_OAuthInfo);
                            _OAuthInfo.expires_in = DateTime.Now.ToString();
                            _ResourceGuruClient.OAuthInfo = _OAuthInfo;
                        }
                    }
                }
                requestHeaders["Authorization"] = "Bearer " + _OAuthInfo.access_token;
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            request.Proxy = this._Proxy;
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
                    request.ContentLength = int.Parse(requestHeaders["Content-length"]);
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
                using (
                    WebResponse response = request.GetResponse())
                {
                    apiResponse.Status = (int)((HttpWebResponse)response).StatusCode;

                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        apiResponse.Body = sr.ReadToEnd();
                    }
                    apiResponse.Headers = responseHeaders;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    apiResponse.Status = (int)((HttpWebResponse)response).StatusCode;
                    foreach (string key in response.Headers.AllKeys)
                    {
                        responseHeaders.Add(key, response.Headers.Get(key));
                    }

                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        apiResponse.Body = sr.ReadToEnd();
                    }
                    apiResponse.Headers = responseHeaders;
                }
            }


            //if (apiResponse.Headers.ContainsKey("Retry-After"))
            //   RateLimitRemaining = int.Parse(apiResponse.Headers["Retry-After"]);



            switch (apiResponse.Status)
            {
                case 200:
                case 201:
                    responseObject = JsonConvert.DeserializeObject<T>(apiResponse.Body);
                    break;
                case 204:
                    responseObject = default(T);
                    break;
                case 401:
                    //if (!string.IsNullOrEmpty(_OAuthInfo.refresh_token))
                    //{
                    //    //Refresh access token
                    //    var authInfo = _ResourceGuruClient.RefreshAccessToken(_OAuthInfo);
                    //    if (authInfo != null && !string.IsNullOrEmpty(authInfo.access_token))
                    //        responseObject = Request<T>(requestMethod, originalUrl, requestData, options);
                    //}
                    //else
                    //{

                    //}
                    throw new ResourceGuruException(apiResponse.Status, "Unauthorized", apiResponse.Body);
                    break;
                case 403:
                    if (apiResponse.Headers.ContainsKey("Retry-After"))
                    {
                        int retryAfter = 0;
                        int.TryParse(apiResponse.Headers["Retry-After"], out retryAfter);
                        //   RateLimitRemaining = int.Parse(apiResponse.Headers["Retry-After"]);
                        throw new ResourceGuruException(apiResponse.Status, "RateLimitExceed", apiResponse.Body, retryAfter);
                    }

                    throw new ResourceGuruException(apiResponse.Status, "Forbidden", apiResponse.Body);
                case 404:
                    throw new ResourceGuruException(apiResponse.Status, "Not Found", apiResponse.Body);
                case 422:
                    throw new ResourceGuruException(apiResponse.Status, "Unprocessable Entity", apiResponse.Body);
                case 500:
                case 502:
                case 503:
                case 504:
                    throw new ResourceGuruException(apiResponse.Status, "Resource Guru is having trouble", apiResponse.Body);
                default:
                    throw new ResourceGuruException(apiResponse.Status, "Uncategorized Error", apiResponse.Body);
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
                        {
                            streamWriter.Write(obj);
                        }
                        else
                        {
                            var settings = new JsonSerializerSettings();
                            settings.NullValueHandling = NullValueHandling.Ignore;

                            streamWriter.Write(JsonConvert.SerializeObject(obj, settings));
                        }
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