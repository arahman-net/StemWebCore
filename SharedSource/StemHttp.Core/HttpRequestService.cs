using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static StemHttp.Core.HttpModuleEnums;

namespace StemHttp.Core
{
    public class HttpRequestSrvice
    {
        //private readonly IUserSession _userSession;
        public string BaseUri { get; set; }
        protected string ApiKey = "";//ConfigurationManager.AppSettings["ApiKey"];

        public async Task<T> GetAsync<T>(string uri)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                //var baseUri = _baseConfig["ApiUrl"];
                //client.BaseAddress = new System.Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(BaseUri + uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    dynamic errorResponse = JsonConvert.DeserializeObject(responseJson);
                    throw new WebException("response: " + errorResponse, WebExceptionStatus.ReceiveFailure);
                }
            }
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

        public async Task<T> GetAsync<T>(string uri, string tokenInfo)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                //var baseUri = _baseConfig["ApiUrl"];
                client.BaseAddress = new System.Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenInfo);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(BaseUri + uri);
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    dynamic errorResponse = JsonConvert.DeserializeObject(responseJson);
                    throw new WebException("response: " + errorResponse, WebExceptionStatus.ReceiveFailure );
                }
            }
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

        public async Task<T> GetAsync<T>(string uri, int Id, string tokenInfo)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                //var baseUri = _baseConfig["ApiUrl"];
                client.BaseAddress = new System.Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenInfo);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(BaseUri + uri);
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    dynamic errorResponse = JsonConvert.DeserializeObject(responseJson);
                    throw new WebException("response: " + errorResponse, WebExceptionStatus.ReceiveFailure);
                }
            }
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

        public async Task<T> PutJsonAsync<T>(string uri, object data, string tokenInfo)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", tokenInfo);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonRequest = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                var response = client.PutAsync(BaseUri + uri, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    dynamic errorResponse = JsonConvert.DeserializeObject(responseJson);
                    throw new WebException("response: " + errorResponse, WebExceptionStatus.ReceiveFailure);
                }
            }
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

        public async Task<T> PostAsJsonAsync<T>(string uri, object data, string tokenInfo)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BaseUri);

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", tokenInfo);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonRequest = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                var response = client.PostAsync(BaseUri + uri, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    dynamic errorResponse = JsonConvert.DeserializeObject(responseJson);
                    throw new WebException("response: " + errorResponse, WebExceptionStatus.ReceiveFailure);
                }
            }
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

        public async Task<T> DeleteJsonAsync<T>(string uri, string tokenInfo)
        {
            string responseJson = string.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(BaseUri);

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", tokenInfo);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync(BaseUri + uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    dynamic errorResponse = JsonConvert.DeserializeObject(responseJson);
                    throw new WebException("response: " + errorResponse, WebExceptionStatus.ReceiveFailure);
                }
            }
            return JsonConvert.DeserializeObject<T>(responseJson);
        }

    }
}
