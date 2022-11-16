using System;

namespace sirrius.Model
{
    public class ApiResponse
    {
        public string ApiName { get; set; }
        public string Version { get; set; }

        public int HttpStatusCode { get; set; }

        public object Data { get; set; }

        public bool IsError { get; set; }

        public ApiResponse(string apiName, string version, int httpStatusCode, object data, bool isError = false)
        {
            this.ApiName = apiName;
            this.Version = version;
            this.HttpStatusCode = httpStatusCode;
            this.Data = data;
            this.IsError = isError;
        }
    }
}
