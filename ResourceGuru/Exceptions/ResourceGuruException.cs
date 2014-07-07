using Newtonsoft.Json;
using System;

namespace ResourceGuru.Exceptions
{
    public class ResourceGuruException : Exception
    {
        /// <summary>
        /// Status code of the response
        /// </summary>
        public int Status { get; internal set; }
        public string Error { get; set; }
        public string ResponseBody { get; set; }
        public ResourceGuruException(int status, string error, string responseBody)
        {
            this.Status = status;
            this.Error = error;
            this.ResponseBody = responseBody;
        }
    }
}
