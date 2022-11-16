using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utilities.Helper
{
    public static class RestClientHelper
    {
        private static T GetResult<T>(RestClient client, RestRequest request, object obj = null, Dictionary<string, string> headers = null)
        {
            if (headers != null) //header varsa requeste headerları ekle
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            //request.AddHeader("Content-Type", "application/json");

            if (obj != null) //post,put,delete gibi işlemler için servise gönderilecek nesne varsa requeste ekle
            {
                request.AddJsonBody(obj);
            }
            //client üzerinden requesti servise yolla ve
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        private static async Task<T> GetResultAsync<T>(RestClient client, RestRequest request, object obj = null, Dictionary<string, string> headers = null)
        {
            if (headers != null) //header varsa requeste headerları ekle
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            //request.AddHeader("Content-Type", "application/json");

            if (obj != null) //post,put,delete gibi işlemler için servise gönderilecek nesne varsa requeste ekle
            {
                request.AddJsonBody(obj);
            }
            //client üzerinden requesti servise yolla ve
            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }

        public static T PostMethod<T>(Method method, object payload, string uri, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(method) { RequestFormat = DataFormat.Json };

            var result = GetResult<T>(client, request, payload, headers);

            return result;
        }

        public static async Task<T> PostMethodAsync<T>(Method method, object payload, string uri, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(method) { RequestFormat = DataFormat.Json };

            var result = await GetResultAsync<T>(client, request, payload, headers);

            return result;
        }

        public static T GetResult<T>(object data)
        {
            return JsonConvert.DeserializeObject<T>(data.ToString());
        }
    }
}
