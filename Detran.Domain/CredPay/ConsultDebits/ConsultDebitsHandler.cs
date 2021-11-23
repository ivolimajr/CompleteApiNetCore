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

        private const string ENDPOINT = "/detran/debitos";

        public ConsultDebitsHandler(IConfiguration configuration, CredPayTokenService tokenService)
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

            try
            {
                request.Uf = "MS";
                request.Pais = "BR";
                request.Placa = "MRZ4519";
                request.Renavam = "00225622521";
                CheckSuportedUf(request.Uf);

                var content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("pais", request.Pais),
                new KeyValuePair<string, string>("uf", request.Uf),
                new KeyValuePair<string, string>("placa", request.Placa),
                new KeyValuePair<string, string>("renavam", request.Renavam),
                });

                var client = new HttpClientService<ConsultDebitsInput, ConsultDebitsResponse, CredPayTokenService, Guid>(HttpClientTokenType.XAPIToken, TokenService.Token);
                client.Init(TokenService.BaseAddress + "/" + TokenService.ApiVersion + ENDPOINT, TokenService, ignoreHeaders: true);
                var response = await client.PostFormUrlEncoded(content);

                if (response.Detran == null || response.Detran?.ValorTotalNum == 0) return response;

                response.Protocolo = Guid.NewGuid().ToString("N").Substring(0, 5).PadLeft(8, '0') + DateTime.Today.ToString("yyyy");

                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        private void CheckSuportedUf(string uf)
        {
            string[] estados = new[] { "SP", "RS", "MS", "DF", "BA", "PB" };
            //string[] estados = new[] { "RS" };

            if (!estados.Any(estado => estado.ToLower() == uf.ToLower()))
                throw new HttpClientCustomException("Somente são suportado pesquisas aos seguintes estados: 'RS'");
        }
    }
}
