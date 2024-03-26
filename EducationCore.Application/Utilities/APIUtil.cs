using EducationCore.Application.Contracts.Utilities;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EducationCore.Application.Utilities
{
    public class APIUtil : IAPIUtil
    {
        private readonly AsyncRetryPolicy _retryPolicy;

        public APIUtil()
        {
            _retryPolicy = Policy
                   .Handle<Exception>()
                   .WaitAndRetryAsync(1, i => TimeSpan.FromSeconds(1));
        }

        public async Task<T> GetDataAsync<T>(string requestUri, Dictionary<string, string> requestParams = null, Dictionary<string, string> headerParams = null)
        {
            if (requestParams == null)
                requestParams = new Dictionary<string, string>();

            string strRequestParam = "";
            foreach (var item in requestParams)
            {
                strRequestParam += "/" + item.Value;
            }

            requestUri += strRequestParam;

            var response = await GetAsync(requestUri, headerParams);

            var result = await CheckResponeApiAsync<T>(response);

            return result;
        }

        private async Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> headerParams = null)
        {
            if (headerParams == null) headerParams = new Dictionary<string, string>();

            HttpResponseMessage response = null;
            string strResult = string.Empty;

            await _retryPolicy.ExecuteAsync(async () =>
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var item in headerParams)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                response = await client.GetAsync(requestUri);

                strResult = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();
            });

            response.Content = new StringContent(strResult, Encoding.UTF8, "application/json");

            return response;
        }

        private async Task<T> CheckResponeApiAsync<T>(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new NotImplementedException();
            }

            string strResult = await response.Content.ReadAsStringAsync();

            T result = JsonConvert.DeserializeObject<T>(strResult.Replace(Environment.NewLine, ""));

            return result;
        }

        public async Task<T> PostDataAsync<T>(string requestUri, string json, Dictionary<string, string> headerParams = null)
        {
            var response = await PostAsync(requestUri, json, headerParams);

            var result = await CheckResponeApiAsync<T>(response);

            return result;
        }

        private async Task<HttpResponseMessage> PostAsync(string requestUri, string json, Dictionary<string, string> headerParams = null)
        {
            if (headerParams == null) headerParams = new Dictionary<string, string>();

            HttpResponseMessage response = null;
            string strResult = string.Empty;

            await _retryPolicy.ExecuteAsync(async () =>
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var item in headerParams)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }

                var data = new StringContent(json ??= "", Encoding.UTF8, "application/json");

                response = await client.PostAsync(requestUri, data);

                strResult = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();
            });

            response.Content = new StringContent(strResult, Encoding.UTF8, "application/json");

            return response;
        }
    }
}