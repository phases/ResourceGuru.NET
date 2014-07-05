using Newtonsoft.Json;
using System;

namespace ResourceGuruAPI.Exceptions
{
    public class ResourceGuruException : Exception
    {
        /// <summary>
        /// Status code of the response
        /// </summary>
        public int Status { get; internal set; }
        public string Error { get; set; }
        public ResourceGuruException(int status, string error)
        {
            this.Status = status;
        }
    }
}
