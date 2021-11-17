using Detran.Shared.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Detran.Shared.Services.HttpMethods
{
    public static class HttpClientMediaType
    {
        public const string ApplicationJson = "application/json";
    }

    public static class HttpClientTokenType
    {
        public const string XAPIToken = "x-api-key";
    }

    public class HttpClientService<TInput, TResponse, TTokenService, TKey>
        where TInput : class
        where TResponse : class, new()
        where TTokenService : AuthTokentService
    {

        static readonly HttpClient httpClient = new HttpClient();

        private bool _useBearer = true;
        public string ApiVersion { get; set; }
        public TTokenService TokenService { get; set; }
        public string Endpoint { get; set; }
        private HttpClient HttpClient { get; set; }

        public HttpClientService(string tokenType, string token)
        {
            _useBearer = false;
            InitClient();
            AddHeader(tokenType, token);
        }


        public HttpClientService()
        {
            InitClient();
        }

        private void InitClient()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression =
                    DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.All
            };

            HttpClient = new HttpClient(handler);
        }

        public void Init(string endpoint, TTokenService tokenService, Dictionary<string, string> headerOption = null, bool ignoreHeaders = false)
        {
            TokenService = tokenService;
            Endpoint = endpoint;
            httpClient.BaseAddress = new Uri(TokenService.BaseAddress, UriKind.RelativeOrAbsolute);

            HttpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(HttpClientMediaType.ApplicationJson));

            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

            if (_useBearer)
            {
                AddBearerHeaderToken();
            }

            if (!ignoreHeaders)
            {
                if (headerOption != null)
                {
                    foreach (var (key, value) in headerOption)
                    {
                        if (key == "Authorization") HttpClient.DefaultRequestHeaders.Remove(key);
                        HttpClient.DefaultRequestHeaders.Add(key, value);
                    }
                }
            }

        }

        public void AddHeader(string key, string value)
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, value);
        }

        public void AddBearerHeaderToken()
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Bearer", TokenService.Token);
        }

        public async Task<TResponse> Get()
        {
            var response = await HttpClient.GetAsync(Endpoint);
            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
            }

            return JsonConvert.DeserializeObject<TResponse>(result);
        }

        public async Task<TResponse> Get(TKey id)
        {
            var response = await HttpClient.PostAsJsonAsync<TKey>(Endpoint, id);

            if (response.StatusCode < HttpStatusCode.BadRequest)
                return await response.Content.ReadAsAsync<TResponse>();

            var result = await response.Content.ReadAsStringAsync();

            throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
        }

        public async Task<TResponse> Post(TInput entity)
        {
            var response = await HttpClient.PostAsJsonAsync<TInput>(Endpoint, entity);

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
            }

            var res = await response.Content.ReadAsAsync<TResponse>();

            return res;
        }

        public async Task<TResponse> PostFormUrlEncoded(FormUrlEncodedContent content)
        {
            var response = await HttpClient.PostAsync(Endpoint, content);

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
            }

            var res = await response.Content.ReadAsAsync<TResponse>();

            return res;
        }

        public async Task<TResponse> Put(TInput entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                "application/json-patch+json");
            var response = await HttpClient.PutAsync(Endpoint, content);

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();

                throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
            }

            var responseString = await response.Content.ReadAsStringAsync();
            TResponse res = JsonConvert.DeserializeObject<TResponse>(responseString);
            return res;
        }

        public async Task<TResponse> Patch(TInput entity)
        {
            var content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                "application/json-patch+json");
            var response = await HttpClient.PatchAsync(Endpoint, content);

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
            }
            var res = await response.Content.ReadAsAsync<TResponse>();
            return res;
        }

        public async Task<HttpResponseMessage> Delete(TKey id)
        {
            var response = await HttpClient.DeleteAsync($"{Endpoint}/{id}");

            if (response.StatusCode >= HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new HttpClientCustomException(result, Convert.ToInt32(response.StatusCode));
            }
            return response;
        }

        public HttpClient GetHttpClient()
        {
            return HttpClient;
        }
    }
}