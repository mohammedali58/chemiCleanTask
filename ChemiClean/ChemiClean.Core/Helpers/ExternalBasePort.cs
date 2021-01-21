using ChemiClean.SharedKernel;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ChemiClean.Core
{
    public class ExternalBasePort<TRequest, TResponse>
    {
        public TResponse BaseCall(string baseUrl, string actionUrl, string message, Dictionary<string, string> headers = default, Dictionary<string, object> parameters = default)
        {
            IRestResponse<TResponse> response = new RestSharpAPIHandler().GetAPIResult<TResponse>(baseUrl, actionUrl, null, headers, parameters);
            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                return JsonConvert.DeserializeObject<TResponse>(response.Content);
            else
                throw new ValidationsException($"{message} {JsonConvert.SerializeObject(response.Content)}");
        }
         
        public TResponse BaseCall(string baseUrl, string actionUrl, string message, TRequest request, Dictionary<string, string> headers = default, Dictionary<string, object> parameters = default)
        {
            IRestResponse<TResponse> response = new RestSharpAPIHandler().GetAPIResult<TResponse, TRequest>(baseUrl, actionUrl, null, headers, parameters, request);
            if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                return JsonConvert.DeserializeObject<TResponse>(response.Content);
            else
                throw new ValidationsException($"{message} {JsonConvert.DeserializeObject<ResultBaseDto>(response.Content).Message}");
        }
    }
}