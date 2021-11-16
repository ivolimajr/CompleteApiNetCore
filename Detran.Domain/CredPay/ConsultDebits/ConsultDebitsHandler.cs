using Detran.Shared.Configurations;
using Detran.Shared.Helpers;
using Detran.Shared.Services.HttpMethods;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Detran.Domain.CredPay.ConsultDebits
{
    public class ConsultDebitsHandler : IRequestHandler<ConsultDebitsInput, ConsultDebitsResponse>
    {
        public IConfiguration Configuration { get; }
        public CredPayTokenService TokenService { get; }

        private const string ENDPOINT = "detran/debitos";

        public ConsultDebitsHandler(IConfiguration configuration, IHttpClientFactory clientFactory, CredPayTokenService tokenService)
        {
            Configuration = configuration;
            TokenService = tokenService;

            var config = Configuration.GetSection("CredPayConfig").Get<CredPayConfig>();

            TokenService.Token = config.Token;
            TokenService.BaseAddress = config.Url;
            TokenService.ApiVersion = config.ApiVersion;
        }

        public async Task<ConsultDebitsResponse> Handle(ConsultDebitsInput request, CancellationToken cancellationToken)
        {

            var config = Configuration.GetSection("CredPayConfig").Get<CredPayConfig>();

            try
            {
                CheckSuportedUf(request.Uf);

                var content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("pais", request.Pais),
                new KeyValuePair<string, string>("uf", request.Uf),
                new KeyValuePair<string, string>("placa", request.Placa),
                new KeyValuePair<string, string>("renavam", request.Renavam),
                });

                var client = GetClient<ConsultDebitsInput, ConsultDebitsResponse, Guid>($"/{TokenService.ApiVersion}/detran/debitos");
                var response = await client.PostFormUrlEncoded(content);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private HttpClientService<TInput, TResponse, TKey, CredPayTokenService> GetClient<TInput, TResponse, TKey>(string url, bool ignoreHeaders = true)
        {
            var client = new HttpClientService<TInput, TResponse, TKey, CredPayTokenService>(HttpClientTokenType.XAPIToken, TokenService.Token);
            client.Init(url, TokenService, ignoreHeaders: ignoreHeaders);
            return client;
        }

        private void CheckSuportedUf(string uf)
        {
            //string[] estados = new[] { "SP", "RS", "MS", "DF", "BA", "PB" };
            string[] estados = new[] { "RS" };

            if (!estados.Any(estado => estado.ToLower() == uf.ToLower()))
                throw new HttpClientCustomException("Somente são suportado pesquisas aos seguintes estados: 'RS'");
        }
    }
}
