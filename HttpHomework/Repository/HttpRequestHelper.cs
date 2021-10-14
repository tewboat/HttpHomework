using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpHomework.Repository
{
    public class HttpRequestHelper
    {
        private HttpClient httpClient;

        public HttpRequestHelper(Uri baseAddress)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("product", "1"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string Get(string uri)
        {
            var task = httpClient.GetAsync(uri);
            return GetContent(task);
        }

        public string Post(string uri, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var task = httpClient.PostAsync(uri, content);
            return GetContent(task);
        }

        private string GetContent(Task<HttpResponseMessage> task)
        {
            var response = task.Result;
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"\n{response.StatusCode} {response.RequestMessage}");
            var result = response.Content.ReadAsStringAsync();
            return result.Result;
        }


        public void AddAuthorization(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}