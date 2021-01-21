using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ChemiClean.SharedKernel

{
    public sealed class RestSharpAPIHandler
    {
        public IRestResponse<TResponse> GetAPIResult<TResponse, TRequestBody>(string baseUrl, string apiCall, string authToken, Dictionary<string, string> headers, Dictionary<string, object> paramaters, TRequestBody requestBody)
        {
            RestRequest request = PrepareRequest(authToken, headers, paramaters, requestBody);
            RestClient client = new RestClient(baseUrl + apiCall);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return client.Execute<TResponse>(request);
        }

        public IRestResponse<TResponse> GetAPIResult<TResponse>(string baseUrl, string apiCall, string authToken, Dictionary<string, string> headers, Dictionary<string, object> paramaters)
        {
            RestRequest request = PrepareRequest(authToken, headers, paramaters, null);
            RestClient client = new RestClient(baseUrl + apiCall);
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            return client.Execute<TResponse>(request);
        }

        private RestRequest PrepareRequest(string authToken, Dictionary<string, string> headers, Dictionary<string, object> paramaters, object requestBody)
        {
            RestRequest request = new RestRequest(Method.POST);
            if (headers != null && headers.Keys.Contains("Method") && !string.IsNullOrEmpty(headers["Method"]))
            {
                switch (headers["Method"])
                {
                    case "GET":
                        request = new RestRequest(Method.GET);
                        break;

                    case "PUT":
                        request = new RestRequest(Method.PUT);
                        break;

                    default:
                        request = new RestRequest(Method.POST);
                        break;
                }
            }
            if (!string.IsNullOrWhiteSpace(authToken))
                request.AddHeader("Authorization", "Bearer " + authToken);

            if (headers != null)
                foreach (var irem in headers)
                    request.AddHeader(irem.Key, irem.Value);

            if (paramaters != null)
                foreach (var irem in paramaters)
                    request.AddQueryParameter(irem.Key, irem.Value.ToString(), true);

            if (requestBody != null)
                request.AddJsonBody(requestBody);

            request.RequestFormat = DataFormat.Json;

            return request;
        }
    }
}