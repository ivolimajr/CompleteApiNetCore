using Detran.Shared.Configurations;
using Detran.Shared.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Detran.Domain.CredPay.ConsultDebits
{
    public class ConsultDebitsHandler : IRequestHandler<ConsultDebitsInput, ConsultDebitsResponse>
    {    
        public IConfiguration Configuration { get; }

        private string Url = "https://services-homolog.credpay.com.vc";
        private string Token = "26b0eeb1a4b89fcf4b1520aa2b546f6b";
        private string ApiVersion = "v2";

        private static readonly HttpClient client = new HttpClient();

        public ConsultDebitsHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<ConsultDebitsResponse> Handle(ConsultDebitsInput request, CancellationToken cancellationToken)
        {

            //var config = Configuration.GetSection("CredPayConfig").Get<CredPayConfig>();

            try
            {
                /*
                CheckSuportedUf(request.Uf);

                var request = client.PostAsync<ConsultDebitsResponse>(this.Url, request);
                //var client = GetClient<DebitosInput, DebitosResponse, Guid>($"/{TokenService.ApiVersion}/detran/debitos");
                //var response = await client.PostFormUrlEncoded(content);
                return null;
                */
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
