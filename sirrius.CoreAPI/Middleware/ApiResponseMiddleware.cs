using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using sirrius.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.CoreAPI.Middleware
{
    /// <summary>
    /// JsonApiWrapper middle-ware class
    /// It handles all responses in order to maintain consistence under api:json
    /// specification. Strictly preserves data structure into a common response
    /// wrapper in order to avoid inconsistency. 
    /// The specification and style-guide can be found at http://jsonapi.org/
    /// </summary>
    public class ApiResponseMiddleware
    {
        /// <summary>
        /// API version class member
        /// </summary>
        private String ApiVersion;

        /// <summary>
        /// API name class member
        /// </summary>
        private String ApiName;

        /// <summary>
        /// Request processing task member
        /// </summary>
        private readonly RequestDelegate Next;

        /// <summary>
        /// Initializes a new JsonApiWrapper Middle-ware instance.
        /// </summary>
        /// <param name="version">API version to be shown in all responses</param>
        /// <param name="name">API name to be shown in all responses</param>
        /// <param name="requestDelegate">A task that represents the completion of request processing</param>
        public ApiResponseMiddleware(string version, string name, RequestDelegate requestDelegate)
        {
            this.ApiVersion = version;
            this.ApiName = name;
            this.Next = requestDelegate;
        }

        /// <summary>
        /// Response post-processing handler.
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            Stream responseBody = context.Response.Body;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await this.Next(context);

                memoryStream.Position = 0;
                string responseString = new StreamReader(memoryStream).ReadToEnd();
                string wrappedResponse = this.Wrap(responseString, context);
                byte[] responseBytes = Encoding.UTF8.GetBytes(wrappedResponse);

                //context.Response.Headers["Content-type"] = "application/vnd.api+json";
                context.Response.Headers["Content-type"] = "application/json";
                context.Response.Body = responseBody;
                await context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }

        /// <summary>
        /// Wraps body content into json:api specification.
        /// </summary>
        /// <param name="originalBody">Original body content</param>
        /// <param name="context">HttpContext object</param>
        /// <returns>Wrapped JSON string</returns>
        private string Wrap(string originalBody, HttpContext context)
        {
            object response;

            if (IsJSON(originalBody))
                response = JsonConvert.DeserializeObject(originalBody);
            else
                response = originalBody;

            object wrapper;

            if (IsSuccessResponse(context.Response.StatusCode))
                wrapper = this.DataWrap(response, context);
            else
                wrapper = this.ErrorWrap(response.ToString(), context);

            string newBody = JsonConvert.SerializeObject(wrapper, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
                {
                    //NamingStrategy = new SnakeCaseNamingStrategy()
                    //{
                    //    ProcessDictionaryKeys = true
                    //}

                    NamingStrategy = null,
                }
            });

            return newBody;
        }

        /// <summary>
        /// Rewrites original body content for success wrapping.
        /// </summary>
        /// <param name="response">Response body</param>
        /// <returns>Formatted object</returns>
        private object DataWrap(object response, HttpContext context)
        {
            return new ApiResponse(ApiName, ApiVersion, context.Response.StatusCode, response);
        }

        /// <summary>
        /// Rewrites original body content for error wrapping.
        /// </summary>
        /// <param name="response">Response body</param>
        /// <returns>Formatted object</returns>
        private object ErrorWrap(string response, HttpContext context)
        {
            string reason = string.IsNullOrWhiteSpace(response) ? StatusCodeMessage(context.Response.StatusCode) : response;

            return new ApiResponse(ApiName, ApiVersion, context.Response.StatusCode, reason, true);
        }

        /// <summary>
        /// Determines whether string is a valid JSON (then, serialize) or not.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool IsJSON(string text)
        {
            var response = false;

            try
            {
                JToken.Parse(text);

                response = true;
            }
            catch (JsonReaderException ex)
            {
                response = false;
            }

            return response;
        }

        /// <summary>
        /// Determine whether a response is a success or not by its status code.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private bool IsSuccessResponse(int statusCode)
        {
            return (statusCode >= 200 && statusCode < 299);
        }

        private string StatusCodeMessage(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Bad request.";
                case 401:
                    return "Unauthorized access.";
                case 402:
                    return "Payment required.";
                case 403:
                    return "Forbidden access.";
                case 404:
                    return "Resource not found.";
                case 405:
                    return "Method not allowed.";
                case 406:
                    return "Not acceptable.";
                case 407:
                    return "Proxy authentication required.";
                case 408:
                    return "Request timeout.";
                case 409:
                    return "Conflict";
                case 410:
                    return "Resource is gone.";
                case 411:
                    return "Length is required.";
                case 500:
                    return "Internal server error.";
                case 501:
                    return "Not implemented.";
                case 502:
                    return "Bad gateway.";
                case 503:
                    return "Service unavailable.";
                case 504:
                    return "Gateway timeout.";
                case 505:
                    return "HTTP version not supported.";
            }
            return "";
        }
    }
}
